using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static finished3.ArrowTranslator;

namespace finished3
{
    public class MouseController : MonoBehaviour
    {
        
        // =====================
        // 🔹 Inspector (Unity)
        // =====================
        public GameObject cursor;
        public float speed;
        public GameObject characterPrefab;
        public int movementRange = 3;

        // =====================
        // 🔹 Core References
        // =====================
        private CharacterInfo character;
        private CharacterStats playerStats;
        private JumpMover jumpMover;
        private ClimbMover climbMover;

        // =====================
        // 🔹 Systems
        // =====================
        private MovementController movementController;
        private AttackController attackController;
        private RangeSystem rangeSystem;
        private TileHighlighter tileHighlighter;
        private ArrowTranslator arrowTranslator;
        private MovementSystem movementSystem;

        // =====================
        // 🔹 Runtime State
        // =====================
        private bool isMoving;
        private bool isRangeVisible = true;

        // =====================
        // 🔹 Data / Cache
        // =====================
        private List<OverlayTile> rangeFinderTiles;
        private List<OverlayTile> attackTiles;
        private List<OverlayTile> path = new List<OverlayTile>();

        private void Start()
        {
            // =====================
            // 🔹 Systems
            // =====================
            movementController = new MovementController();
            attackController = new AttackController();
            rangeSystem = new RangeSystem();
            movementSystem = new MovementSystem();

            // =====================
            // 🔹 Visual / Helpers
            // =====================
            tileHighlighter = new TileHighlighter();
            arrowTranslator = new ArrowTranslator();

            // =====================
            // 🔹 Runtime State
            // =====================
            isMoving = false;

            // =====================
            // 🔹 Data / Cache
            // =====================
            path = new List<OverlayTile>();
            rangeFinderTiles = new List<OverlayTile>();
        }

        void LateUpdate()
        {
            // =====================
            // 🔹 1. Raycast
            // =====================
            RaycastHit2D? hit = GetFocusedOnTile();

            // =====================
            // 🔹 2. Không hit tile
            // =====================
            if (!hit.HasValue)
            {
                ClearArrows();

                if (Input.GetMouseButtonDown(0))
                {
                    HideRange();
                    isRangeVisible = false;

                    path.Clear();
                    isMoving = false;
                }

                return; // ✅ chỉ return ở đây là OK
            }
            else
            {
                OverlayTile tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();

                // =====================
                // 🔹 3. Cursor Visual
                // =====================
                cursor.transform.position = tile.transform.position;
                cursor.GetComponent<SpriteRenderer>().sortingOrder =
                    tile.GetComponent<SpriteRenderer>().sortingOrder;

                // =====================
                // 🔹 4. Path Preview
                // =====================
                if (isRangeVisible && rangeFinderTiles.Contains(tile) && !isMoving)
                {
                    path = movementController.GetPath(character, tile, rangeFinderTiles);

                    tileHighlighter.ClearArrows(rangeFinderTiles);
                    tileHighlighter.ShowPath(path, arrowTranslator, character.standingOnTile);
                }
                else
                {
                    ClearArrows();
                }

                // =====================
                // 🔹 5. Input Handling
                // =====================
                if (Input.GetMouseButtonDown(0))
                {
                    if (HandleAttack(tile)) return;
                    if (HandleSpawn(tile)) return;
                    if (HandleMovement(tile)) return;
                }
            }

            // =====================
            // 🔹 6. Movement Execute
            // =====================
            if (path.Count > 0 && isMoving && isRangeVisible)
            {
                movementController.MoveAlongPath(
                character,
                jumpMover,
                climbMover,        
                movementSystem,    
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
            if (!isRangeVisible)
            {
                // chỉ cho phép bật lại range khi click vào chính mình
                if (tile == character.standingOnTile)
                {
                    ShowRange();
                    isRangeVisible = true;
                }

                return false;
            }
    
            if (!rangeFinderTiles.Contains(tile))
            {
                HideRange();
                isRangeVisible = false;

                path.Clear();
                isMoving = false;

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
            climbMover = character.GetComponent<ClimbMover>();

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

            tileHighlighter.ClearArrows(rangeFinderTiles);
        }

        void HideRange()
        {
            tileHighlighter.ClearTiles(rangeFinderTiles);
        }

        void ShowRange()
        {
            tileHighlighter.ShowMoveRange(rangeFinderTiles);
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
