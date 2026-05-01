using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static finished3.ArrowTranslator;

namespace finished3
{
    public class OverlayTile : MonoBehaviour
    {
        // ---------------- A* PATHFINDING & TILE DATA ----------------
        public int G;
        public int H;
        public int F { get { return G + H; } }
        public bool isBlocked = false;
        public OverlayTile Previous;
        public Vector3Int gridLocation;
        public Vector2Int grid2DLocation {get { return new Vector2Int(gridLocation.x, gridLocation.y); } }
        public CharacterInfo unitOnTile;
        public Chest chestOnTile;
        public List<Sprite> arrows;
        
        [HideInInspector]
        public Tilemap baseTilemap; // Lưu tham chiếu đến Tilemap gốc chứa nền đất
        [HideInInspector]
        public TileBase originalTileBase; // Lưu lại nền đất gốc để phục hồi khi chuyển tầng

        // ---------------- DUNGEON SEEKER MINESWEEPER DATA ----------------
        [Header("Minesweeper States")]
        public bool isRevealed = false;
        public bool isFlagged = false;
        
        public bool isMonster = false;
        public bool isChest = false;
        public bool isStair = false;

        [Header("Visual Settings")]
        [Tooltip("Gạch (Tile) Cầu Thang thực tế trên Palette để thay thế nền đất")]
        public TileBase stairTileBase;
        
        [Tooltip("Sprite của Cầu thang sẽ được đổi khi lật ô (Fallback nếu không dùng TileBase)")]
        public Sprite stairSprite;

        [Tooltip("Màu hiển thị khi ô chưa được lật (Sương mù)")]
        public Color fogColor = new Color(0.5f, 0.5f, 0.5f, 1f); // Mặc định là Xám
        [Tooltip("Màu hiển thị khi ô đã lật VÀ CÓ TEXTBOX (Có quái, rương, thang)")]
        public Color numberedColor = new Color(1f, 0.9f, 0.6f, 1f); // Màu do user tùy chọn (mặc định hơi vàng nhạt)
        [Tooltip("Màu hiển thị khi ô đã lật VÀ TRỐNG (Không có textbox)")]
        public Color emptyColor = new Color(1f, 1f, 1f, 1f); // Trắng (không màu)

        [Header("Neighbor Counts")]
        public int neighborMonsterCount = 0;
        public int neighborChestCount = 0;
        public int neighborStairCount = 0;

        // ---------------- METHODS ----------------

        /// <summary>
        /// Thực hiện lật ô khi người chơi bước lên
        /// </summary>
        public void RevealTile()
        {
            if (isRevealed || isFlagged) return;

            isRevealed = true;
            
            // Nếu là ô cầu thang
            if (isStair)
            {
                if (stairTileBase != null && baseTilemap != null)
                {
                    // Lưu lại TileBase cũ trước khi ghi đè nếu chưa lưu
                    if (originalTileBase == null)
                    {
                        originalTileBase = baseTilemap.GetTile(gridLocation);
                    }

                    // Xóa đất và thay bằng gạch Cầu thang thật sự trên Grid
                    baseTilemap.SetTile(gridLocation, stairTileBase);
                    
                    // Ẩn lớp sương mù (Overlay) đi hoàn toàn để lộ cầu thang
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    return;
                }
                else if (stairSprite != null)
                {
                    // Fallback: Nếu user chưa gắn TileBase mà chỉ gắn Sprite
                    gameObject.GetComponent<SpriteRenderer>().sprite = stairSprite;
                    gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    return;
                }
            }

            // Xử lý hiệu ứng đồ họa (nhuốm màu) khi lật ô (tan sương mù)
            int totalNeighbors = neighborMonsterCount + neighborChestCount + neighborStairCount;
            if (totalNeighbors > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = numberedColor;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = emptyColor;
            }
        }

        /// <summary>
        /// Toggle cắm cờ (Dùng chuột phải)
        /// </summary>
        public void ToggleFlag()
        {
            if (isRevealed) return; // Đã lật thì không cắm cờ

            isFlagged = !isFlagged;
            // TODO: Bật/Tắt Sprite cái cờ trên mặt gạch
        }

        // Các hàm cũ phục vụ việc vẽ đường đi (Có thể tái sử dụng)
        public void HidePath()
        {
            GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 1, 0);
        }

        // ---- BACKWARD COMPATIBILITY CHO CODE CŨ (Chapter1Controller, TileHighlighter) ----
        public bool isShowing { get { return isRevealed; } set { isRevealed = value; } }
        public void HideTile() 
        { 
            isRevealed = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
        public void ShowTile() 
        { 
            isRevealed = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        // -----------------------------------------------------------------------------------

        public void SetSprite(ArrowDirection d)
        {
            if (d == ArrowDirection.None)
                HidePath();
            else
            {
                GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 1, 1);
                GetComponentsInChildren<SpriteRenderer>()[1].sprite = arrows[(int)d];
                GetComponentsInChildren<SpriteRenderer>()[1].sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
            }
        }

        public void SetAttackColor()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.3f, 0.3f, 1); // Đỏ
        }

        /// <summary>
        /// Reset trạng thái màu sắc về Sương mù và phục hồi lại Tile gốc
        /// </summary>
        public void SetFogColor()
        {
            isRevealed = false;
            gameObject.GetComponent<SpriteRenderer>().color = fogColor; 

            // Phục hồi lại nền đất gốc nếu từng bị thay bằng Cầu Thang
            if (originalTileBase != null && baseTilemap != null)
            {
                baseTilemap.SetTile(gridLocation, originalTileBase);
                originalTileBase = null; // Xóa cache
            }

            // Nếu Fallback dùng Sprite thì cũng xóa
            if (stairSprite != null && gameObject.GetComponent<SpriteRenderer>().sprite == stairSprite)
            {
                // Set lại sprite cũ (Trắng tinh)
                gameObject.GetComponent<SpriteRenderer>().sprite = null; 
            }
        }
    }
}
