using System.Collections.Generic;
using UnityEngine;

namespace finished3
{
    /// <summary>
    /// Giao diện bảng Túi đồ tổng quát.
    /// Nó sẽ lắng nghe sự kiện từ InventoryManager và yêu cầu các InventorySlotUI cập nhật hình ảnh.
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        [Header("Tham chiếu Hệ thống")]
        public InventoryManager inventory;

        [Header("Tham chiếu Giao diện (UI)")]
        [Tooltip("Kéo Object cha chứa các ô (Slot) vào đây. Khuyến khích dùng GridLayoutGroup.")]
        public Transform itemsParent;

        private InventorySlotUI[] slots;

        private void Start()
        {
            if (inventory == null)
            {
                inventory = InventoryManager.Instance;
            }

            // Đăng ký nghe kênh sóng "Túi đồ bị thay đổi"
            if (inventory != null)
            {
                inventory.OnInventoryChanged += UpdateUI;
            }

            // Lấy danh sách toàn bộ các Script Ô vuông con
            if (itemsParent != null)
            {
                slots = itemsParent.GetComponentsInChildren<InventorySlotUI>();
            }

            UpdateUI(); // Lần đầu bật lên thì load luôn
        }

        private void OnDestroy()
        {
            if (inventory != null)
            {
                inventory.OnInventoryChanged -= UpdateUI;
            }
        }

        /// <summary>
        /// Đồng bộ giao diện UI cho khớp với dữ liệu bên trong biến "slots" của InventoryManager
        /// </summary>
        public void UpdateUI()
        {
            if (slots == null || inventory == null) return;

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventory.slots.Count)
                {
                    // Lấy cục dữ liệu nhét vào cái vỏ UI
                    slots[i].UpdateSlot(inventory.slots[i]);
                }
                else
                {
                    // Nếu số lượng UI Slot sinh ra bị dư so với MaxSlots của code -> Xóa đi
                    slots[i].ClearSlot();
                }
            }
        }
    }
}
