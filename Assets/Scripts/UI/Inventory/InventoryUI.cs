using UnityEngine;

namespace finished3
{
    /// <summary>
    /// Giao diện (Mặt Cười) của túi ba-lô trên màn hình hông.
    /// Hoạt động theo quy luật Tai Nghe Điện Thoại (Observer lắng nghe Event OnInventoryChanged).
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        private void Start()
        {
            // Bắt đài phát sóng ngay khi UI Mở (Start)
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.OnInventoryChanged += UpdateUI;
            }
        }

        private void OnDestroy()
        {
            // Trả Micro (Tránh lỗi Rò Rỉ Biến Trí Nhớ Kênh khi Màn bị tắt / Delete)
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.OnInventoryChanged -= UpdateUI;
            }
        }

        /// <summary>
        /// Mỗi khi Nhặt Đồ, Quăng Đồ, Cắn Bình -> Hàm rọi tranh này sẽ được Tự Động Rửa / Bấm Chạy
        /// </summary>
        private void UpdateUI()
        {
            GameLogger.Log("[UI] Thu sóng mới...! Đang kéo Khổ Lưới Vẽ lại Hòm Hình Ảnh cho Túi Đồ của bạn.");
            
            // Cách làm bằng Mắt Thường (Bạn cần tạo Hình Chữ Nhật trên Canvas):
            // var slots = InventoryManager.Instance.slots;
            // for (int i = 0; i < uiSlots.Length; i++) {
            //     if (i < slots.Count && !slots[i].IsEmpty) {
            //         uiSlots[i].iconImage.sprite = slots[i].item.icon;
            //         uiSlots[i].iconImage.enabled = true;
            //         uiSlots[i].amountText.text = slots[i].amount.ToString();
            //     } else {
            //         uiSlots[i].iconImage.enabled = false;
            //         uiSlots[i].amountText.text = "";
            //     }
            // }
        }
    }
}
