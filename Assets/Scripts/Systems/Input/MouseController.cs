using UnityEngine;

namespace finished3
{
    /// <summary>
    /// MouseController 2.0 - Chuẩn SRP.
    /// Chỉ đảm nhận nhiệm vụ đọc trạng thái chuột, bắn lưới Raycast (Zero-Allocation), vẽ Cursor chỉ điểm, và giao việc lại cho Bộ Máy PlayerController.
    /// </summary>
    public class MouseController : MonoBehaviour
    {
        #region Inspector Variables
        [Header("Cursor Settings")]
        public GameObject cursor;
        public float speed;
        #endregion

        #region Events
        public static event System.Action<OverlayTile> OnTileHovered;
        #endregion

        #region Internal State
        private static RaycastHit2D[] _hitsBuffer = new RaycastHit2D[50];
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            UnityEngine.Assertions.Assert.IsNotNull(cursor, "FATAL ERROR: Bạn chưa kéo `cursor` GameObject vào hệ thống MouseController!");
        }

        private void LateUpdate()
        {
            // 1. Raycast dò tìm Tile
            RaycastHit2D? hit = GetFocusedOnTile();

            // 2. Không trúng Tile nào
            if (!hit.HasValue)
            {
                // Nếu User click ra ngoài -> Hủy lệnh thông qua PlayerController
                if (Input.GetMouseButtonDown(0))
                {
                    if (PlayerController.Instance != null)
                    {
                        PlayerController.Instance.CancelAction();
                    }
                }
                return;
            }

            // 3. Trúng một OverlayTile
            OverlayTile tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
            if (tile == null) return;

            // Cập nhật vị trí hiển thị Cursor
            cursor.transform.position = tile.transform.position;
            cursor.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;

            // Kiểm tra PlayerController đã vào trận chưa
            if (PlayerController.Instance == null)
            {
                GameLogger.LogWarning("PlayerController.Instance is missing! Make sure to attach PlayerController to a GameObject in the Scene and assign PlayerPrefab to it.");
                return;
            }

            // 4. Hover Tile: Bắn Event cho UI xử lý (HoverTooltipUI sẽ lắng nghe Event này)
            OnTileHovered?.Invoke(tile);
            PlayerController.Instance.HoverTile(tile); // Vẫn gọi cho logic (nếu cần vẽ outline)

            // 5. Chuột Trái Click: Di chuyển / Mở sương mù
            if (Input.GetMouseButtonDown(0))
            {
                PlayerController.Instance.TapOnTile(tile);
            }

            // 6. Chuột Phải Click: Cắm cờ
            if (Input.GetMouseButtonDown(1))
            {
                tile.ToggleFlag();
                GameLogger.Log($"Đã {(tile.isFlagged ? "cắm" : "rút")} cờ tại ô {tile.gridLocation}");
            }
        }
        #endregion

        #region Internal Raycast Logic
        /// <summary>
        /// Tìm cấu trúc vật lý OverlayTile mà chuột đang chiếu, khai thác RaycastNonAlloc chặn hoàn toàn rác Ram.
        /// </summary>
        private static RaycastHit2D? GetFocusedOnTile()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            int hitCount = Physics2D.Raycast(mousePos2D, Vector2.zero, new ContactFilter2D(), _hitsBuffer);

            if (hitCount > 0)
            {
                RaycastHit2D highestHit = _hitsBuffer[0];
                float maxZ = highestHit.collider.transform.position.z;

                for (int i = 1; i < hitCount; i++)
                {
                    float currentZ = _hitsBuffer[i].collider.transform.position.z;
                    if (currentZ > maxZ)
                    {
                        maxZ = currentZ;
                        highestHit = _hitsBuffer[i];
                    }
                }
                return highestHit;
            }
            return null;
        }
        #endregion
    }
}
