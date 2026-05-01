using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace finished3
{
    public class MapManager : MonoBehaviour
    {
        private static MapManager _instance;
        public static MapManager Instance { get { return _instance; } }

        public GameObject overlayPrefab;
        public GameObject overlayContainer;

        [Header("Dungeon Generation Settings")]
        [Tooltip("Số lượng quái vật trên bản đồ")]
        public int monsterCount = 15;
        [Tooltip("Số lượng rương báu")]
        public int chestCount = 3;
        [Tooltip("Số lượng cầu thang")]
        public int stairCount = 1;

        public Dictionary<Vector2Int, OverlayTile> map;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else
            {
                _instance = this;
            }
        }

        void Start()
        {
            var tileMaps = gameObject.transform.GetComponentsInChildren<Tilemap>().OrderByDescending(x => x.GetComponent<TilemapRenderer>().sortingOrder);
            map = new Dictionary<Vector2Int, OverlayTile>();

            foreach (var tm in tileMaps)
            {
                BoundsInt bounds = tm.cellBounds;

                for (int z = bounds.max.z; z > bounds.min.z; z--)
                {
                    for (int y = bounds.min.y; y < bounds.max.y; y++)
                    {
                        for (int x = bounds.min.x; x < bounds.max.x; x++)
                        {
                            if (tm.HasTile(new Vector3Int(x, y, z)))
                            {
                                if (!map.ContainsKey(new Vector2Int(x, y)))
                                {
                                    var overlayTile = Instantiate(overlayPrefab, overlayContainer.transform);
                                    var cellWorldPosition = tm.GetCellCenterWorld(new Vector3Int(x, y, z));
                                    overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tm.GetComponent<TilemapRenderer>().sortingOrder;
                                    var overlayComponent = overlayTile.gameObject.GetComponent<OverlayTile>();
                                    overlayComponent.gridLocation = new Vector3Int(x, y, z);
                                    overlayComponent.baseTilemap = tm;
    
                                    map.Add(new Vector2Int(x, y), overlayComponent);
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<OverlayTile> GetSurroundingTiles(Vector2Int originTile)
        {
            var surroundingTiles = new List<OverlayTile>();


            Vector2Int TileToCheck = new Vector2Int(originTile.x + 1, originTile.y);
            if (map.ContainsKey(TileToCheck))
            {
                if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(map[TileToCheck]);
            }

            TileToCheck = new Vector2Int(originTile.x - 1, originTile.y);
            if (map.ContainsKey(TileToCheck))
            {
                if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(map[TileToCheck]);
            }

            TileToCheck = new Vector2Int(originTile.x, originTile.y + 1);
            if (map.ContainsKey(TileToCheck))
            {
                if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(map[TileToCheck]);
            }

            TileToCheck = new Vector2Int(originTile.x, originTile.y - 1);
            if (map.ContainsKey(TileToCheck))
            {
                if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(map[TileToCheck]);
            }

            return surroundingTiles;
        }

        /// <summary>
        /// Lấy 8 ô xung quanh (kể cả chéo) phục vụ cho logic dò mìn.
        /// </summary>
        public List<OverlayTile> Get8SurroundingTiles(Vector2Int originTile)
        {
            var surroundingTiles = new List<OverlayTile>();
            
            // 8 hướng (N, S, E, W, NE, NW, SE, SW)
            Vector2Int[] directions = new Vector2Int[]
            {
                new Vector2Int(1, 0), new Vector2Int(-1, 0), 
                new Vector2Int(0, 1), new Vector2Int(0, -1),
                new Vector2Int(1, 1), new Vector2Int(1, -1),
                new Vector2Int(-1, 1), new Vector2Int(-1, -1)
            };

            foreach (var dir in directions)
            {
                Vector2Int tileToCheck = originTile + dir;
                if (map.ContainsKey(tileToCheck))
                {
                    // Đảm bảo không chênh lệch độ cao quá lớn
                    if (Mathf.Abs(map[tileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                    {
                        surroundingTiles.Add(map[tileToCheck]);
                    }
                }
            }
            return surroundingTiles;
        }

        /// <summary>
        /// Rải ngẫu nhiên Quái vật, Rương, Cầu thang vào bản đồ. Đảm bảo khu vực xuất phát an toàn.
        /// </summary>
        public void GenerateDungeon(Vector2Int startPos)
        {
            // Xóa sạch trạng thái rác nếu người dùng vô tình lưu trong Prefab
            foreach (var kvp in map)
            {
                kvp.Value.isMonster = false;
                kvp.Value.isChest = false;
                kvp.Value.isStair = false;
            }

            List<OverlayTile> availableTiles = new List<OverlayTile>(map.Values);
            
            // Xóa điểm bắt đầu và 8 ô xung quanh nó khỏi danh sách có thể rải mìn (vùng an toàn)
            OverlayTile startTile = map.ContainsKey(startPos) ? map[startPos] : null;
            if (startTile != null)
            {
                availableTiles.Remove(startTile);
                List<OverlayTile> safeNeighbors = Get8SurroundingTiles(startPos);
                foreach (var safe in safeNeighbors)
                {
                    availableTiles.Remove(safe);
                }
            }

            // Shuffle list
            System.Random rng = new System.Random();
            availableTiles = availableTiles.OrderBy(a => rng.Next()).ToList();

            int currentIndex = 0;

            // Rải cầu thang
            for (int i = 0; i < stairCount && currentIndex < availableTiles.Count; i++, currentIndex++)
                availableTiles[currentIndex].isStair = true;

            // Rải rương
            for (int i = 0; i < chestCount && currentIndex < availableTiles.Count; i++, currentIndex++)
                availableTiles[currentIndex].isChest = true;

            // Rải quái vật
            for (int i = 0; i < monsterCount && currentIndex < availableTiles.Count; i++, currentIndex++)
                availableTiles[currentIndex].isMonster = true;

            // Tính toán số liệu cho TẤT CẢ các ô
            foreach (var kvp in map)
            {
                OverlayTile tile = kvp.Value;
                
                // Reset (cho chắc chắn)
                tile.neighborMonsterCount = 0;
                tile.neighborChestCount = 0;
                tile.neighborStairCount = 0;

                // Nếu ô này là quái thì không cần đếm quái xung quanh cũng được, nhưng cứ đếm chuẩn Minesweeper
                List<OverlayTile> neighbors = Get8SurroundingTiles(kvp.Key);
                foreach (var neighbor in neighbors)
                {
                    if (neighbor.isMonster) tile.neighborMonsterCount++;
                    if (neighbor.isChest) tile.neighborChestCount++;
                    if (neighbor.isStair) tile.neighborStairCount++;
                }

                // Cài đặt màu ban đầu (Fog of War)
                tile.SetFogColor();
            }

            GameLogger.Log($"Dungeon Generated! Monsters: {monsterCount}, Chests: {chestCount}, Stairs: {stairCount}");
        }
    }
}
