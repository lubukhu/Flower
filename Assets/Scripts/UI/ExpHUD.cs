using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace finished3
{
    /// <summary>
    /// Hiển thị thanh Kinh nghiệm (Slider) và Cấp độ (Level) của người chơi.
    /// Lắng nghe sự kiện từ CharacterStats của Player.
    /// </summary>
    public class ExpHUD : MonoBehaviour
    {
        [Header("Giao diện UI")]
        public Slider expSlider;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI expNumberText;

        private CharacterStats playerStats;

        private void Start()
        {
            // Thử tìm Player ngay khi Start
            TryFindPlayer();
        }

        private void Update()
        {
            // Nếu chưa tìm thấy Player (do chưa Spawn), thử tìm liên tục
            if (playerStats == null)
            {
                TryFindPlayer();
            }
        }

        private void TryFindPlayer()
        {
            if (PlayerController.Instance != null && PlayerController.Instance.character != null)
            {
                playerStats = PlayerController.Instance.character.GetComponent<CharacterStats>();
                if (playerStats != null)
                {
                    playerStats.OnExpChanged += UpdateExpUI;
                    // Cập nhật lần đầu
                    UpdateExpUI(playerStats.currentExp, playerStats.expToNextLevel, playerStats.currentLevel);
                }
            }
        }

        private void OnDestroy()
        {
            if (playerStats != null)
            {
                playerStats.OnExpChanged -= UpdateExpUI;
            }
        }

        private void UpdateExpUI(int currentExp, int maxExp, int level)
        {
            if (expSlider != null)
            {
                expSlider.maxValue = maxExp;
                expSlider.value = currentExp;
            }

            if (levelText != null)
            {
                levelText.text = $"Lv.{level}";
            }

            if (expNumberText != null)
            {
                expNumberText.text = $"{currentExp}/{maxExp}";
            }
        }
    }
}
