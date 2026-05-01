using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace finished3
{
    /// <summary>
    /// Hiển thị Textbox chứa các ô vuông màu khi rê chuột vào một ô ĐÃ LẬT.
    /// Màu: Xám (Quái), Vàng (Rương), Xanh Dương (Cầu thang).
    /// </summary>
    public class HoverTooltipUI : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject tooltipPanel; // Panel chứa tooltip, sẽ ẩn/hiện
        public Transform row1Container; // Hàng trên
        public Transform row2Container; // Hàng dưới

        [Header("Position Settings")]
        [Tooltip("Căn chỉnh độ cao/lệch của Textbox so với ô gạch")]
        public Vector3 tooltipOffset = new Vector3(0, 0.8f, 0);

        [Header("Prefabs")]
        public GameObject monsterBlockPrefab; // Image ô vuông màu xám
        public GameObject chestBlockPrefab;   // Image ô vuông màu vàng
        public GameObject stairBlockPrefab;   // Image ô vuông màu xanh dương

        private List<GameObject> activeBlocks = new List<GameObject>();

        private void OnEnable()
        {
            MouseController.OnTileHovered += HandleTileHovered;
        }

        private void OnDisable()
        {
            MouseController.OnTileHovered -= HandleTileHovered;
        }

        private void HandleTileHovered(OverlayTile tile)
        {
            if (tooltipPanel == null)
            {
                GameLogger.LogWarning("⚠️ Bạn quên kéo TooltipPanel vào Script HoverTooltipUI ở trong Inspector rồi kìa!");
                return;
            }

            // Chỉ hiển thị Tooltip nếu trỏ vào ô đã được lật (và không bị cắm cờ)
            if (tile == null || !tile.isRevealed)
            {
                tooltipPanel.SetActive(false);
                return;
            }

            // Nếu người chơi đang đứng đúng ô này thì không cần hiện Tooltip vì đã có MainHUD
            if (PlayerController.Instance != null && tile == PlayerController.Instance.character?.standingOnTile)
            {
                tooltipPanel.SetActive(false);
                return;
            }

            // Ẩn Tooltip nếu ô này đang chứa Rương, Cầu thang hoặc Quái vật (unitOnTile)
            if (tile.isChest || tile.isStair || (tile.unitOnTile != null && tile.unitOnTile != PlayerController.Instance?.character))
            {
                tooltipPanel.SetActive(false);
                return;
            }

            tooltipPanel.SetActive(true);
            
            // Xóa các block cũ
            foreach (var block in activeBlocks)
            {
                Destroy(block);
            }
            activeBlocks.Clear();
            // Tập hợp tất cả các khối cần sinh ra theo thứ tự Quái -> Rương -> Thang
            List<GameObject> blocksToSpawn = new List<GameObject>();
            for(int i = 0; i < tile.neighborMonsterCount; i++) blocksToSpawn.Add(monsterBlockPrefab);
            for(int i = 0; i < tile.neighborChestCount; i++) blocksToSpawn.Add(chestBlockPrefab);
            for(int i = 0; i < tile.neighborStairCount; i++) blocksToSpawn.Add(stairBlockPrefab);

            int total = blocksToSpawn.Count;
            
            // Ẩn Tooltip nếu ô trống (không có quái, rương, thang)
            if (total == 0)
            {
                tooltipPanel.SetActive(false);
                return;
            }

            // THUẬT TOÁN CHIA HÀNG ĐẶC BIỆT THEO Ý USER
            int topRowCount = total; // Mặc định <= 4 thì xếp hết ở hàng trên
            if (total == 5) topRowCount = 3; // 3 trên, 2 dưới
            else if (total > 5) topRowCount = 4; // 6: 4 trên 2 dưới, 7: 4 trên 3 dưới, 8: 4 trên 4 dưới

            // Sinh các block và nhét vào đúng hàng
            for (int i = 0; i < total; i++)
            {
                Transform targetRow = (i < topRowCount) ? row1Container : row2Container;
                if (targetRow != null && blocksToSpawn[i] != null)
                {
                    GameObject block = Instantiate(blocksToSpawn[i], targetRow);
                    activeBlocks.Add(block);
                }
            }

            // Lấy chính xác tọa độ gốc của ô gạch (Ví dụ: x=1.5, y=1.25, z=2)
            // Cộng thêm khoảng lệch do người dùng chỉnh trên Inspector
            Vector3 tileWorldPos = tile.transform.position + tooltipOffset; 
            
            if (Camera.main != null)
            {
                Canvas canvas = tooltipPanel.GetComponentInParent<Canvas>();
                if (canvas != null)
                {
                    // Lấy tọa độ pixel màn hình thực tế
                    Vector2 screenPoint = Camera.main.WorldToScreenPoint(tileWorldPos);
                    
                    // Chuyển đổi tọa độ pixel đó vào không gian cục bộ (đã bị Scale) của Canvas
                    RectTransform canvasRect = canvas.GetComponent<RectTransform>();
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        canvasRect, 
                        screenPoint, 
                        canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera, 
                        out Vector2 localPoint);

                    // Đưa Mỏ neo về giữa để tọa độ localPoint tính từ trung tâm Canvas
                    RectTransform tooltipRect = tooltipPanel.GetComponent<RectTransform>();
                    tooltipRect.anchorMin = new Vector2(0.5f, 0.5f);
                    tooltipRect.anchorMax = new Vector2(0.5f, 0.5f);
                    
                    // Gán vị trí
                    tooltipRect.localPosition = localPoint;
                }
            }
        }

    }
}
