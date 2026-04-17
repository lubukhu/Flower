using System;
using UnityEngine;

namespace finished3
{
    /// <summary>
    /// Bộ máy điều hành thông số chiến đấu trên cơ thể Char/Mob.
    /// Áp dụng mẫu Observer Event-Driven và đọc dữ liệu từ Data Card ScriptableObject.
    /// </summary>
    public class CharacterStats : MonoBehaviour
    {
        [Header("🎮 Nguồn Dữ Liệu Sức Mạnh")]
        [Tooltip("Kéo thẻ Bài Dữ Liệu (ScriptableObject) của Lính này vào đây thay vì gõ tay.")]
        public CharacterData characterData;

        [Header("🔥 Trạng Thái Nội Bộ")]
        public int currentHP;

        // 🔗 Chuyển tiếp (Getter) các chỉ số sang thẻ Data. Không làm lỗi Code cũ đang gọi .attackRange
        public int maxHP => characterData != null ? characterData.maxHP : 10;
        public int attack => characterData != null ? characterData.attack : 3;
        public int defense => characterData != null ? characterData.defense : 1;
        public int moveRange => characterData != null ? characterData.moveRange : 3;
        public int attackRange => characterData != null ? characterData.attackRange : 1;

        #region KÊNH SÓNG SỰ KIỆN (Events)
        /// <summary>
        /// Kênh báo động khi Máu bị rút cục bộ (Máu Hiện Tại, Máu Tối Đa). Gửi cho giao diện (UI Thanh Máu) tự cập nhật.
        /// </summary>
        public event Action<int, int> OnHealthChanged;

        /// <summary>
        /// Đài phát loa thông báo Lính tử trận. Hệ thống Nhiệm vụ, Drop Đồ lắng nghe ở đây.
        /// </summary>
        public event Action OnDied;
        #endregion

        private void Awake()
        {
            UnityEngine.Assertions.Assert.IsNotNull(characterData, $"FATAL ERROR: Bạn vương vãi lính {gameObject.name} nhưng chưa vứt CharacterData vào nó!");
        }

        private void Start()
        {
            ResetHealth();
        }

        /// <summary>
        /// Bơm đầy máu lại (VD: Khi hồi sinh từ Object Pool)
        /// </summary>
        public void ResetHealth()
        {
            if (characterData != null)
            {
                currentHP = characterData.maxHP;
                OnHealthChanged?.Invoke(currentHP, maxHP);
            }
        }

        /// <summary>
        /// Điểm giao tiếp duy nhất (API) để bơm máu từ Bình Máu hoặc Kỹ Năng Bơm Máu.
        /// Chặn Bơm máu ảo (máu đã đầy).
        /// </summary>
        public bool Heal(int amount)
        {
            if (currentHP <= 0 || currentHP >= maxHP || characterData == null) return false;

            currentHP += amount;
            if (currentHP > maxHP) currentHP = maxHP;

            GameLogger.Log($"{gameObject.name} nốc bình/được Buff, hồi {amount} máu. Máu hiện tại: {currentHP}/{maxHP}");

            // Kênh Event UI lắng nghe
            OnHealthChanged?.Invoke(currentHP, maxHP);
            return true;
        }

        /// <summary>
        /// Tiếp nhận trừ máu và phát sóng ra UI.
        /// </summary>
        public void TakeDamage(int damage)
        {
            if (currentHP <= 0 || characterData == null) return;

            int finalDamage = Mathf.Max(damage - defense, 1);
            currentHP -= finalDamage;

            GameLogger.Log($"{gameObject.name} ăn {finalDamage} sát thương. Máu tồn: {currentHP}/{maxHP}");

            // 🎵 [SFX] Bị thương (Random + Pitch/Pan)
            if (characterData != null) characterData.PlayRandomHurt();

            // Kéo mỏ neo Thanh Máu UI (Observer Protocol)
            OnHealthChanged?.Invoke(currentHP, maxHP);

            if (currentHP <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// ♻️ MỘT THẾ GIỚI KHÔNG RÁC (Object Pooling Basics)
        /// Chết lâm sàng -> Biến mất thay vì Phá hủy vật lý Object.
        /// </summary>
        private void Die()
        {
            GameLogger.Log($"{gameObject.name} đứt gánh - Tử Ngang.");

            // 🎵 [SFX] Chết (Random + Pitch/Pan)
            if (characterData != null) characterData.PlayRandomDeath();

            // Triggers (System Lọt Đồ, Cộng Điểm EXP) sẽ rình rập ở kênh này
            OnDied?.Invoke();

            // 🔥 [BUG FIX] Giải phóng Ô gạch (Tile) đang đứng để lấy chỗ trống cho Player đi vào
            var info = GetComponent<CharacterInfo>();
            if (info != null && info.standingOnTile != null)
            {
                info.standingOnTile.unitOnTile = null;
            }

            // Trả xác về Object Pool / Biến mất
            gameObject.SetActive(false);
        }
    }
}