using System;
using UnityEngine;

namespace finished3
{
    public enum UpgradeType
    {
        MaxHP,
        MaxMana,
        MaxSteps
    }

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
        public int currentSteps;
        public int currentMana;

        [Header("🌟 Hệ Thống Cấp Độ (EXP)")]
        public int currentLevel = 1;
        public int currentExp = 0;
        public int expToNextLevel = 100;

        [Header("💪 Chỉ Số Thưởng Nâng Cấp (Bonus)")]
        public int bonusMaxHP = 0;
        public int bonusMaxMana = 0;
        public int bonusMaxSteps = 0;

        // 🔗 Chuyển tiếp (Getter) các chỉ số sang thẻ Data + Chỉ số thưởng.
        public int maxHP => (characterData != null ? characterData.maxHP : 10) + bonusMaxHP;
        public int maxSteps => (characterData != null ? characterData.maxSteps : 30) + bonusMaxSteps;
        public int maxMana => (characterData != null ? characterData.maxMana : 10) + bonusMaxMana;
        public int attack => characterData != null ? characterData.attack : 3;
        public int defense => characterData != null ? characterData.defense : 1;
        public int moveRange => characterData != null ? characterData.moveRange : 3;
        public int attackRange => characterData != null ? characterData.attackRange : 1;

        #region KÊNH SÓNG SỰ KIỆN (Events)
        /// <summary>
        /// Kênh báo động khi Máu bị rút cục bộ (Máu Hiện Tại, Máu Tối Đa). Gửi cho giao diện (UI Thanh Máu) tự cập nhật.
        /// </summary>
        public event Action<int, int> OnHealthChanged;
        public event Action<int, int> OnStepsChanged;
        public event Action<int, int> OnManaChanged;
        
        /// <summary>
        /// Kênh thông báo thay đổi EXP (EXP Hiện tại, EXP Cần lên cấp, Level hiện tại)
        /// </summary>
        public event Action<int, int, int> OnExpChanged;
        
        /// <summary>
        /// Kênh thông báo Lên cấp để gọi UI Nâng cấp
        /// </summary>
        public event Action<int> OnLevelUp;

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
            // Nếu là Player, ưu tiên lấy dữ liệu từ GameManager (đã chọn ở Menu)
            if (gameObject.CompareTag("Player") && GameManager.Instance != null && GameManager.Instance.selectedCharacterData != null)
            {
                characterData = GameManager.Instance.selectedCharacterData;
            }

            ResetHealth();
        }

        /// <summary>
        /// Bơm đầy máu lại (VD: Khi hồi sinh từ Object Pool)
        /// </summary>
        public void ResetHealth()
        {
            if (characterData != null)
            {
                currentHP = maxHP; // Sử dụng maxHP thay vì characterData.maxHP để tính cả bonus
                currentSteps = maxSteps;
                currentMana = maxMana;

                OnHealthChanged?.Invoke(currentHP, maxHP);
                OnStepsChanged?.Invoke(currentSteps, maxSteps);
                OnManaChanged?.Invoke(currentMana, maxMana);
                OnExpChanged?.Invoke(currentExp, expToNextLevel, currentLevel);
            }
        }

        /// <summary>
        /// Cộng dồn EXP và xử lý Lên cấp (Level Up)
        /// </summary>
        public void AddExp(int amount)
        {
            if (currentHP <= 0) return;

            currentExp += amount;
            GameLogger.Log($"{gameObject.name} nhận được {amount} EXP. Tổng: {currentExp}/{expToNextLevel}");

            while (currentExp >= expToNextLevel)
            {
                currentExp -= expToNextLevel;
                currentLevel++;
                
                // Công thức tính EXP cấp tiếp theo (Tăng thêm 50%)
                expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.5f);

                GameLogger.Log($"{gameObject.name} LÊN CẤP {currentLevel}!");
                OnLevelUp?.Invoke(currentLevel);
            }

            OnExpChanged?.Invoke(currentExp, expToNextLevel, currentLevel);
        }

        /// <summary>
        /// Áp dụng Nâng cấp khi người chơi chọn Thẻ Bài
        /// </summary>
        public void ApplyUpgrade(UpgradeType upgradeType)
        {
            switch (upgradeType)
            {
                case UpgradeType.MaxHP:
                    bonusMaxHP += 2; // +1 Tim = +2 HP
                    currentHP += 2;
                    OnHealthChanged?.Invoke(currentHP, maxHP);
                    break;
                case UpgradeType.MaxMana:
                    bonusMaxMana += 2; // +1 Mana = +2 MP
                    currentMana += 2;
                    OnManaChanged?.Invoke(currentMana, maxMana);
                    break;
                case UpgradeType.MaxSteps:
                    bonusMaxSteps += 10;
                    currentSteps += 10;
                    OnStepsChanged?.Invoke(currentSteps, maxSteps);
                    break;
            }
            GameLogger.Log($"Đã áp dụng nâng cấp: {upgradeType}");
        }

        /// <summary>
        /// Xử lý mỗi bước di chuyển của nhân vật. Hết bước sẽ trừ 1 máu.
        /// </summary>
        public void UseStep()
        {
            if (currentSteps > 0)
            {
                currentSteps--;
                OnStepsChanged?.Invoke(currentSteps, maxSteps);
            }
            else
            {
                // Hình phạt khi đi lỡ bước (Hết Lượt Đi) -> Bị trừ 1 Máu theo yêu cầu
                GameLogger.LogWarning("Nhân vật đã Kiệt Sức! Phải dùng Máu (HP) để bước tiếp!");
                TakeDamage(1); 
            }
        }

        public void UseMana(int amount)
        {
            if (currentMana >= amount)
            {
                currentMana -= amount;
                OnManaChanged?.Invoke(currentMana, maxMana);
            }
        }

        /// <summary>
        /// Bơm lại năng lượng (Mana) từ bình hoặc kỹ năng.
        /// </summary>
        public void RestoreMana(int amount)
        {
            if (currentMana >= maxMana) return;

            currentMana += amount;
            if (currentMana > maxMana) currentMana = maxMana;

            GameLogger.Log($"{gameObject.name} hồi {amount} Mana. Mana hiện tại: {currentMana}/{maxMana}");
            OnManaChanged?.Invoke(currentMana, maxMana);
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