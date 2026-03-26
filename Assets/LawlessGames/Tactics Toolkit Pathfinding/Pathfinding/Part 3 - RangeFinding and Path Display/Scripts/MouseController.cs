using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static finished3.ArrowTranslator;

namespace finished3
{
    public class MouseController : MonoBehaviour
    {
        
        public GameObject cursor;
        public float speed;
        public GameObject characterPrefab;
        public int movementRange = 3;
        private CharacterInfo character;
        private JumpMover jumpMover;
        private bool isRangeVisible = true;
        private CharacterStats playerStats;
        private RangeFinder rangeFinder;
        private ArrowTranslator arrowTranslator;
        private List<OverlayTile> rangeFinderTiles;
        private bool isMoving;
        private AttackController attackController;
        private List<OverlayTile> attackTiles;
        private MovementController movementController;
        private List<OverlayTile> path = new List<OverlayTile>();
        private RangeSystem rangeSystem;
        private TileHighlighter tileHighlighter;

        private void Start()
        {
            tileHighlighter = new TileHighlighter();
            rangeSystem = new RangeSystem();
            movementController = new MovementController();
            attackController = new AttackController();
            rangeFinder = new RangeFinder();
            arrowTranslator = new ArrowTranslator();

            path = new List<OverlayTile>();
            isMoving = false;
            rangeFinderTiles = new List<OverlayTile>();
        }

        void LateUpdate()
        {
            RaycastHit2D? hit = GetFocusedOnTile();

            if (hit.HasValue)
            {
                OverlayTile tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
                cursor.transform.position = tile.transform.position;
                cursor.gameObject.GetComponent<SpriteRenderer>().sortingOrder = tile.transform.GetComponent<SpriteRenderer>().sortingOrder;

                if (isRangeVisible && rangeFinderTiles.Contains(tile) && !isMoving)
                {
                    path = movementController.GetPath(character, tile, rangeFinderTiles);
                    foreach (var item in rangeFinderTiles)
                    {
                        MapManager.Instance.map[item.grid2DLocation].SetSprite(ArrowDirection.None);
                    }

                    for (int i = 0; i < path.Count; i++)
                    {
                        var previousTile = i > 0 ? path[i - 1] : character.standingOnTile;
                        var futureTile = i < path.Count - 1 ? path[i + 1] : null;

                        var arrow = arrowTranslator.TranslateDirection(previousTile, path[i], futureTile);
                        path[i].SetSprite(arrow);
                    }
                }
                else
                {
                    ClearArrows();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (HandleAttack(tile)) return;
                    if (HandleSpawn(tile)) return;
                    if (HandleMovement(tile)) return;
                }
            }
            else
            {
                ClearArrows();

                if (!isRangeVisible)
                    return;
            }
            if (path.Count > 0 && isMoving)
            {
                movementController.MoveAlongPath(
                    character,
                    jumpMover,
                    path,
                    () =>
                    {
                        GetInRangeTiles();
                        isMoving = false;
                    }
                );
            }
            
        }
        bool HandleAttack(OverlayTile tile)
        {
            if (character == null) return false;

            if (tile.unitOnTile != null && tile.unitOnTile != character)
            {
                if (attackTiles.Contains(tile))
                {
                    attackController.TryAttack(tile, playerStats);
                }

                return true; // 🔥 chặn các hành động khác
            }

            return false;
        }
        bool HandleMovement(OverlayTile tile)
        {
            if (character == null) return false;

            if (!rangeFinderTiles.Contains(tile))
            {
                HideRange();
                isRangeVisible = false;
                return true;
            }

            if (tile == character.standingOnTile)
            {
                ShowRange();
                isRangeVisible = true;
                return true;
            }

            // bắt đầu di chuyển
            isMoving = true;
            tile.HideTile();

            return true;
        }
        bool HandleSpawn(OverlayTile tile)
        {
            if (character != null) return false;

            character = Instantiate(characterPrefab).GetComponent<CharacterInfo>();
            playerStats = character.GetComponent<CharacterStats>();
            jumpMover = character.GetComponent<JumpMover>();

            // đặt vào tile (đoạn bạn vừa fix)
            character.transform.position = new Vector3(
                tile.transform.position.x,
                tile.transform.position.y + 0.0001f,
                tile.transform.position.z
            );

            character.GetComponent<SpriteRenderer>().sortingOrder =
                tile.GetComponent<SpriteRenderer>().sortingOrder;

            character.standingOnTile = tile;
            tile.unitOnTile = character;

            GetInRangeTiles();

            return true;
        }
        void ClearArrows()
        {
            if (rangeFinderTiles == null) return;

            foreach (var tile in rangeFinderTiles)
            {
                tileHighlighter.ClearArrows(rangeFinderTiles);
            }
        }

        void HideRange()
        {
            foreach (var item in rangeFinderTiles)
            {
                item.HideTile();
                item.SetSprite(ArrowDirection.None);
            }
        }

        void ShowRange()
        {
            foreach (var item in rangeFinderTiles)
            {
                item.ShowTile();
            }
        }

        private static RaycastHit2D? GetFocusedOnTile()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

            if (hits.Length > 0)
            {
                return hits.OrderByDescending(i => i.collider.transform.position.z).First();
            }

            return null;
        }

        private void GetInRangeTiles()
        {
            rangeFinderTiles = rangeSystem.GetMoveRange(character, movementRange);
            tileHighlighter.ShowMoveRange(rangeFinderTiles);
            attackTiles = rangeSystem.GetAttackRange(character, playerStats.attackRange);
            tileHighlighter.ShowAttackRange(attackTiles);
        }
    }
}
