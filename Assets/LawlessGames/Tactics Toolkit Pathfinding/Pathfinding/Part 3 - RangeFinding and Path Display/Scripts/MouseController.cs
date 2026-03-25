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

        private bool isRangeVisible = true;
        private CharacterStats playerStats;
        private PathFinder pathFinder;
        private RangeFinder rangeFinder;
        private ArrowTranslator arrowTranslator;
        private List<OverlayTile> path;
        private List<OverlayTile> rangeFinderTiles;
        private bool isMoving;

        private void Start()
        {
            pathFinder = new PathFinder();
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
                    path = pathFinder.FindPath(character.standingOnTile, tile, rangeFinderTiles);

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
                    // 🔥 CHẶN CLICK NGOÀI RANGE
                    // CLICK Ô KHÔNG ĐI ĐƯỢC → TẮT RANGE
                    if (character != null && !rangeFinderTiles.Contains(tile))
                    {
                        HideRange();
                        isRangeVisible = false;
                        return;
                    }
                    // CLICK LẠI CHÍNH PLAYER → BẬT LẠI RANGE
                    if (character != null && tile == character.standingOnTile)
                    {
                        ShowRange();
                        isRangeVisible = true;
                        return;
                    }
                    if (character != null && tile.unitOnTile != null && tile.unitOnTile != character)
                    {
                        var enemyStats = tile.unitOnTile.GetComponent<CharacterStats>();

                        float distance = Vector2.Distance(character.transform.position, tile.transform.position);

                        if (distance <= playerStats.attackRange + 0.1f)
                        {
                            CombatManager.Instance.Attack(playerStats, enemyStats);
                        }

                        return;
                    }

                    tile.ShowTile();

                    if (character == null)
                    {
                        character = Instantiate(characterPrefab).GetComponent<CharacterInfo>();
                        playerStats = character.GetComponent<CharacterStats>();

                        PositionCharacterOnLine(tile);
                        GetInRangeTiles();
                    }
                    else
                    {
                        isMoving = true;
                        tile.gameObject.GetComponent<OverlayTile>().HideTile();
                    }
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
                MoveAlongPath();
            }
            
        }
        void ClearArrows()
        {
            if (rangeFinderTiles == null) return;

            foreach (var tile in rangeFinderTiles)
            {
                tile.SetSprite(ArrowDirection.None);
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
        private void MoveAlongPath()
        {
            var step = speed * Time.deltaTime;

            float zIndex = path[0].transform.position.z;
            character.transform.position = Vector2.MoveTowards(character.transform.position, path[0].transform.position, step);
            character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, zIndex);

            if (Vector2.Distance(character.transform.position, path[0].transform.position) < 0.00001f)
            {
                PositionCharacterOnLine(path[0]);
                path.RemoveAt(0);
            }

            if (path.Count == 0)
            {
                GetInRangeTiles();
                isMoving = false;
            }

        }

        private void PositionCharacterOnLine(OverlayTile tile)
        {
            character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
            character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
            character.standingOnTile = tile;
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
            rangeFinderTiles = rangeFinder.GetTilesInRange(new Vector2Int(character.standingOnTile.gridLocation.x, character.standingOnTile.gridLocation.y), movementRange);

            foreach (var item in rangeFinderTiles)
            {
                item.ShowTile();
            }
        }
    }
}
