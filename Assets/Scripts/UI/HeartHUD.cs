using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace finished3
{
    /// <summary>
    /// Hiển thị thanh máu (HP) của người chơi dưới dạng các Trái Tim (Hearts) giống phong cách Zelda.
    /// Tự động lắng nghe sự thay đổi máu từ Player và cập nhật giao diện.
    /// </summary>
    public class HeartHUD : MonoBehaviour
    {
        [Header("UI References")]
        [Tooltip("Cái bảng (Panel) chứa các trái tim, nên dùng HorizontalLayoutGroup")]
        public Transform heartContainer; 
        [Tooltip("Prefab của 1 trái tim (Một GameObject có component Image)")]
        public GameObject heartPrefab; 

        [Header("Heart Sprites")]
        public Sprite fullHeartSprite;   // Hình trái tim đầy (2 HP)
        public Sprite halfHeartSprite;   // Hình trái tim nửa (1 HP)
        public Sprite emptyHeartSprite;  // Hình trái tim rỗng (0 HP)

        private List<Image> hearts = new List<Image>();
        private CharacterStats currentPlayerStats;
        private const int MAX_HEARTS_UI = 10; // Giới hạn không gian UI

        private void OnEnable()
        {
            PlayerController.OnPlayerStep += HandlePlayerSpawned;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerStep -= HandlePlayerSpawned;
            if (currentPlayerStats != null) currentPlayerStats.OnHealthChanged -= UpdateHearts;
        }

        private void HandlePlayerSpawned(OverlayTile tile)
        {
            if (currentPlayerStats == null && PlayerController.Instance.IsPlayerSpawned)
            {
                currentPlayerStats = PlayerController.Instance.character.GetComponent<CharacterStats>();
                currentPlayerStats.OnHealthChanged += UpdateHearts;
                UpdateHearts(currentPlayerStats.currentHP, currentPlayerStats.maxHP);
            }
        }

        private void UpdateHearts(int currentHP, int maxHP)
        {
            // 1. Khởi tạo danh sách 10 trái tim nếu chưa có
            if (hearts.Count == 0)
            {
                for (int i = 0; i < MAX_HEARTS_UI; i++)
                {
                    GameObject newHeart = Instantiate(heartPrefab, heartContainer);
                    hearts.Add(newHeart.GetComponent<Image>());
                }
            }

            // 2. Cập nhật trạng thái từng trái tim (1 Tim = 2 HP)
            int totalHeartsToShow = Mathf.CeilToInt(maxHP / 2f);

            for (int i = 0; i < hearts.Count; i++)
            {
                if (i < totalHeartsToShow && i < MAX_HEARTS_UI)
                {
                    hearts[i].gameObject.SetActive(true);
                    
                    // Logic phân loại Zelda-style
                    if (currentHP >= (i * 2 + 2))
                    {
                        hearts[i].sprite = fullHeartSprite; // Đầy 2 HP
                    }
                    else if (currentHP == (i * 2 + 1))
                    {
                        hearts[i].sprite = halfHeartSprite; // Còn 1 HP lẻ
                    }
                    else
                    {
                        hearts[i].sprite = emptyHeartSprite; // Hết máu
                    }
                }
                else
                {
                    hearts[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
