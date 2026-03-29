using UnityEngine;

namespace finished3
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "Flower/Item/Consumable Item", order = 2)]
    public class ConsumableData : ItemData
    {
        [Header("Chỉ số Phục Hồi (Healing & Buffs)")]
        [Tooltip("Lượng máu bơm lên lập tức")]
        public int healHP;
        
        [Tooltip("Lượng Năng lượng (Mana) bơm lên - Chừa sẵn cho chức năng đánh Skills sau này")]
        public int healMP;

        private void OnEnable()
        {
            // Tự động neo cứng nhãn Tiêu Hao ngay khi tạo thẻ bài này
            itemType = ItemType.Consumable;
        }

        /// <summary>
        /// Logic sử dụng vật phẩm lên sinh mệnh (Máu thịt) của mục tiêu
        /// Trả về TRUE nếu DÙNG ĐƯỢC MÁU (máu chưa đầy).
        /// Trả về FALSE nếu mục tiêu đã full máu (Không cho lãng phí bình)
        /// </summary>
        public bool UseItem(CharacterStats target)
        {
            if (target == null) return false;

            bool itemConsumed = false;

            // Tiêm Máu
            if (healHP > 0)
            {
                // Gọi hàm Bơm Máu bên trong CharacterStats. 
                // Hàm này sẽ tự kiểm tra Máu Đầy (MaxHP) để chặn lãng phí máu.
                bool healed = target.Heal(healHP);
                if (healed) itemConsumed = true;
            }

            // Tiêm MP (Thiết kế phòng hờ Tương Lai)
            // if (healMP > 0) { target.RestoreMana(healMP); itemConsumed = true; }

            // Thêm các thuật toán Buff Sát thương, Buff Giáp ở khúc này sau...

            return itemConsumed; 
        }
    }
}
