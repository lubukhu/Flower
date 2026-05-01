using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace finished3
{
    /// <summary>
    /// Hiển thị Năng lượng (Mana) dưới dạng các Mặt Trăng (Moons) giống phong cách Zelda.
    /// Tối đa 10 icon, 1 icon = 2 Mana.
    /// </summary>
    public class ManaIconHUD : MonoBehaviour
    {
        [Header("UI References")]
        public Transform iconContainer; 
        public GameObject iconPrefab; 

        [Header("Mana Sprites")]
        public Sprite fullMoonSprite;   // 2 MP
        public Sprite halfMoonSprite;   // 1 MP
        public Sprite emptyMoonSprite;  // 0 MP

        private List<Image> icons = new List<Image>();
        private CharacterStats currentPlayerStats;
        private const int MAX_ICONS_UI = 10;

        private void OnEnable()
        {
            PlayerController.OnPlayerStep += HandlePlayerSpawned;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerStep -= HandlePlayerSpawned;
            if (currentPlayerStats != null) currentPlayerStats.OnManaChanged -= UpdateManaIcons;
        }

        private void HandlePlayerSpawned(OverlayTile tile)
        {
            if (currentPlayerStats == null && PlayerController.Instance.IsPlayerSpawned)
            {
                currentPlayerStats = PlayerController.Instance.character.GetComponent<CharacterStats>();
                currentPlayerStats.OnManaChanged += UpdateManaIcons;
                UpdateManaIcons(currentPlayerStats.currentMana, currentPlayerStats.maxMana);
            }
        }

        private void UpdateManaIcons(int currentMP, int maxMP)
        {
            if (icons.Count == 0)
            {
                for (int i = 0; i < MAX_ICONS_UI; i++)
                {
                    GameObject newIcon = Instantiate(iconPrefab, iconContainer);
                    icons.Add(newIcon.GetComponent<Image>());
                }
            }

            int totalIconsToShow = Mathf.CeilToInt(maxMP / 2f);

            for (int i = 0; i < icons.Count; i++)
            {
                if (i < totalIconsToShow && i < MAX_ICONS_UI)
                {
                    icons[i].gameObject.SetActive(true);
                    
                    if (currentMP >= (i * 2 + 2))
                    {
                        icons[i].sprite = fullMoonSprite;
                    }
                    else if (currentMP == (i * 2 + 1))
                    {
                        icons[i].sprite = halfMoonSprite;
                    }
                    else
                    {
                        icons[i].sprite = emptyMoonSprite;
                    }
                }
                else
                {
                    icons[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
