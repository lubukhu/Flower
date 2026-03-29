using System;
using System.Collections.Generic;
using UnityEngine;

namespace finished3
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        [Header("Giới hạn Túi Đồ")]
        [Tooltip("Số ô vuông tối đa có trong túi (Ba-lô)")]
        public int maxSlots = 20;

        [SerializeField]
        public List<InventorySlot> slots = new List<InventorySlot>();

        // 📡 Kênh sóng Độc Quyền thông báo "Trạng thái Túi bị thay đổi: Có đồ mới lọt vào hoặc Vừa xài hết 1 bình"
        public event Action OnInventoryChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            // Khởi tạo các ô trống theo ranh giới MaxSlots
            for (int i = 0; i < maxSlots; i++)
            {
                slots.Add(new InventorySlot(null, 0));
            }
        }

        /// <summary>
        /// Logic Cực mạnh: Bỏ đồ vào túi, Tự động tìm ô nào chứa sẵn Thẻ giống hệt để Xếp Chồng (Stack).
        /// Hết ô để xếp mà lượng đồ lượm về vẫn thừa -> Bật ô Gỗ trống mới để nhét.
        /// </summary>
        public bool AddItem(ItemData itemData, int amountToAdd = 1)
        {
            if (itemData == null || amountToAdd <= 0) return false;

            // Bước 1: Quét tìm ô ĐÃ CÓ MÓN ĐÓ sẵn, và VẪN CHƯA bị Đầy (chưa đạt sức chứa MaxStack)
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].item == itemData && slots[i].amount < itemData.maxStack)
                {
                    int spaceLeft = itemData.maxStack - slots[i].amount; // VD: Max 99, đang có 50 -> Còn nhét được thêm 49
                    if (amountToAdd <= spaceLeft)
                    {
                        slots[i].AddAmount(amountToAdd);
                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                    else // Nhặt quá nhiều, lấp đầy ô này rồi đem hàng thừa vứt sang Ô trống mới kề bên
                    {
                        slots[i].AddAmount(spaceLeft);
                        amountToAdd -= spaceLeft;
                    }
                }
            }

            // Bước 2: Đi dạo tìm xem còn Ô rương Gỗ Đóng Bụi nào trống chưa để đồ gì Không.
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].IsEmpty)
                {
                    int toAdd = Mathf.Min(amountToAdd, itemData.maxStack);
                    slots[i].item = itemData;
                    slots[i].amount = toAdd;
                    amountToAdd -= toAdd;

                    if (amountToAdd <= 0)
                    {
                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                }
            }

            // Kết cục Bi đát: Hết ô, báo lỗi Ra màn hình/Log.
            GameLogger.Log("Túi đồ đã quá tải! Không thể nhét thêm Rác vào túi thần kỳ được nữa.");
            OnInventoryChanged?.Invoke();
            return amountToAdd == 0;
        }

        /// <summary>
        /// Xóa bỏ/Tiêu hủy Item
        /// Quét tự ngược từ ô xa nhất (Cuối túi) về đầu túi để xóa đồ lẻ trước.
        /// </summary>
        public void RemoveItem(ItemData itemData, int amountToRemove = 1)
        {
            for (int i = slots.Count - 1; i >= 0; i--)
            {
                if (slots[i].item == itemData && !slots[i].IsEmpty)
                {
                    if (slots[i].amount >= amountToRemove)
                    {
                        slots[i].RemoveAmount(amountToRemove);
                        if (slots[i].IsEmpty) slots[i].item = null;
                        
                        OnInventoryChanged?.Invoke();
                        return;
                    }
                    else // Ô này đồ mót được ít hơn lượng cần lấy nẹp? Lột sạch ô này và trừ rấn sang ô khác.
                    {
                        amountToRemove -= slots[i].amount;
                        slots[i].RemoveAmount(slots[i].amount);
                        slots[i].item = null;
                    }
                }
            }
            OnInventoryChanged?.Invoke();
        }

        /// <summary>
        /// Mở nắp bình uống nước -> Nhúng sự kiện thẳng vào Máu nhân vật được chỉ định.
        /// Thỏa điều kiện Tách Rời SRP: Lớp Túi đồ Không tự ôm đồm việc "Cộng Trở Sinh Lực"
        /// </summary>
        public bool Consume(ItemData itemData, CharacterStats target)
        {
            // Kiểm chứng là Vật Nhai Được (Consumable) mới cho nhai
            if (itemData is ConsumableData consumable)
            {
                if (HasItem(itemData, 1))
                {
                    bool isSuccess = consumable.UseItem(target); // Bắn Cầu vượt Về Base Cốt Lõi `CharacterStats`
                    if (isSuccess)
                    {
                        RemoveItem(itemData, 1);
                        GameLogger.Log($"Tuyệt vời! Bạn vừa xài mất 1 thẻ {itemData.itemName}");
                        return true;
                    }
                    else
                    {
                        GameLogger.Log("Từ chối Sử dụng! Hình như máu anh da đen này đang căng tràn quá mức rồi.");
                    }
                }
            }
            return false;
        }

        public bool HasItem(ItemData itemData, int requiredAmount)
        {
            int total = 0;
            foreach (var slot in slots)
            {
                if (slot.item == itemData)
                    total += slot.amount;
            }
            return total >= requiredAmount;
        }
    }
}
