using UnityEngine;
using TMPro;

namespace finished3
{
    /// <summary>
    /// Hiển thị thông tin "Tầng hiện tại" ở góc trên bên phải màn hình.
    /// Lắng nghe sự kiện từ DungeonManager.
    /// </summary>
    public class FloorHUD : MonoBehaviour
    {
        [Header("Giao diện")]
        [Tooltip("Text Mesh Pro hiển thị số Tầng (VD: Tầng 1)")]
        public TextMeshProUGUI floorText;

        private void Start()
        {
            if (DungeonManager.Instance != null)
            {
                DungeonManager.Instance.OnFloorChanged += UpdateFloorText;
                
                // Lần đầu bật game
                UpdateFloorText(DungeonManager.Instance.currentFloor);
            }
        }

        private void OnDestroy()
        {
            if (DungeonManager.Instance != null)
            {
                DungeonManager.Instance.OnFloorChanged -= UpdateFloorText;
            }
        }

        private void UpdateFloorText(int floorIndex)
        {
            if (floorText != null)
            {
                floorText.text = $"Tầng {floorIndex}";
            }
        }
    }
}
