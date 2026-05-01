using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace finished3
{
    /// <summary>
    /// Quản lý Popup chọn nâng cấp khi nhân vật lên cấp.
    /// Gắn script này vào 1 Panel UI mặc định ẩn.
    /// </summary>
    public class LevelUpUI : MonoBehaviour
    {
        [Header("Giao diện UI")]
        public GameObject levelUpPanel;
        public TextMeshProUGUI levelTitleText;

        private CharacterStats playerStats;
        private int pendingLevels = 0; // Số lần lên cấp đang chờ xử lý

        private void Start()
        {
            if (levelUpPanel != null) levelUpPanel.SetActive(false);
            TryFindPlayer();
        }

        private void Update()
        {
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
                    playerStats.OnLevelUp += HandleLevelUp;
                }
            }
        }

        private void OnDestroy()
        {
            if (playerStats != null)
            {
                playerStats.OnLevelUp -= HandleLevelUp;
            }
        }

        private void HandleLevelUp(int newLevel)
        {
            pendingLevels++;
            
            // Nếu bảng chưa hiện, thì hiện lên
            if (!levelUpPanel.activeSelf)
            {
                ShowLevelUpPanel();
            }
        }

        private void ShowLevelUpPanel()
        {
            if (levelUpPanel != null)
            {
                levelUpPanel.SetActive(true);
                if (levelTitleText != null && playerStats != null)
                {
                    levelTitleText.text = $"LÊN CẤP {playerStats.currentLevel}!";
                }

                // Chặn di chuyển của Player
                if (PlayerController.Instance != null)
                {
                    PlayerController.Instance.isLevelingUp = true;
                }
            }
        }

        /// <summary>
        /// Gọi từ Button "Nâng HP"
        /// </summary>
        public void OnClickUpgradeHP()
        {
            ApplyUpgrade(UpgradeType.MaxHP);
        }

        /// <summary>
        /// Gọi từ Button "Nâng Mana"
        /// </summary>
        public void OnClickUpgradeMana()
        {
            ApplyUpgrade(UpgradeType.MaxMana);
        }

        /// <summary>
        /// Gọi từ Button "Nâng Steps"
        /// </summary>
        public void OnClickUpgradeSteps()
        {
            ApplyUpgrade(UpgradeType.MaxSteps);
        }

        private void ApplyUpgrade(UpgradeType type)
        {
            if (playerStats != null)
            {
                playerStats.ApplyUpgrade(type);
            }

            pendingLevels--;

            if (pendingLevels > 0)
            {
                // Nếu vẫn còn lượt lên cấp chờ, cập nhật text và giữ bảng hiện
                if (levelTitleText != null && playerStats != null)
                {
                    levelTitleText.text = $"LÊN CẤP {playerStats.currentLevel - pendingLevels + 1}!";
                }
            }
            else
            {
                // Hết lượt lên cấp, đóng bảng và mở khóa di chuyển
                levelUpPanel.SetActive(false);
                if (PlayerController.Instance != null)
                {
                    PlayerController.Instance.isLevelingUp = false;
                }
            }
        }
    }
}
