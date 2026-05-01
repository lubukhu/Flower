using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace finished3
{
    /// <summary>
    /// Giao diện cho một ô (Slot) duy nhất trong túi đồ.
    /// Chịu trách nhiệm hiển thị Hình ảnh, Số lượng và xử lý khi người chơi Click vào ô.
    /// </summary>
    public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
    {
        [Header("UI Components")]
        public Image iconImage;
        public TextMeshProUGUI amountText;

        private ItemData currentItem;
        private int currentAmount;

        /// <summary>
        /// Được gọi bởi InventoryUI để đổ dữ liệu vào ô
        /// </summary>
        public void UpdateSlot(InventorySlot slot)
        {
            if (slot == null || slot.IsEmpty)
            {
                ClearSlot();
                return;
            }

            currentItem = slot.item;
            currentAmount = slot.amount;

            iconImage.sprite = currentItem.icon;
            iconImage.enabled = true; // Bật icon lên

            // Hiển thị số lượng nếu lớn hơn 1
            if (currentAmount > 1)
            {
                amountText.text = currentAmount.ToString();
                amountText.enabled = true;
            }
            else
            {
                amountText.enabled = false;
            }
        }

        public void ClearSlot()
        {
            currentItem = null;
            currentAmount = 0;

            iconImage.sprite = null;
            iconImage.enabled = false;
            amountText.text = "";
            amountText.enabled = false;
        }

        /// <summary>
        /// Sự kiện khi người chơi Click chuột vào ô vuông này
        /// </summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            // Chuột trái = Sử dụng (Uống)
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (currentItem != null)
                {
                    GameLogger.Log($"Đang cố gắng sử dụng vật phẩm: {currentItem.itemName}...");
                    
                    // Lấy ra chỉ số của nhân vật hiện tại để bơm máu/mana
                    CharacterStats playerStats = PlayerController.Instance.character.GetComponent<CharacterStats>();
                    
                    if (playerStats != null)
                    {
                        InventoryManager.Instance.Consume(currentItem, playerStats);
                    }
                }
            }
        }
    }
}
