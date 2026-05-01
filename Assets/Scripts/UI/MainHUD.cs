using UnityEngine;
using TMPro;

namespace finished3
{
    /// <summary>
    /// Hiển thị thông số (Quái, Rương, Cầu thang) của ô gạch mà người chơi ĐANG ĐỨNG.
    /// Góc dưới bên trái màn hình.
    /// </summary>
    public class MainHUD : MonoBehaviour
    {
        [Header("UI References")]
        public TextMeshProUGUI monsterCountText;
        public TextMeshProUGUI chestCountText;
        public TextMeshProUGUI stairCountText;

        private void OnEnable()
        {
            PlayerController.OnPlayerStep += UpdateHUD;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerStep -= UpdateHUD;
        }

        private void UpdateHUD(OverlayTile currentTile)
        {
            if (currentTile == null) return;

            if (monsterCountText != null)
                monsterCountText.text = $"x{currentTile.neighborMonsterCount}";
                
            if (chestCountText != null)
                chestCountText.text = $"x{currentTile.neighborChestCount}";
                
            if (stairCountText != null)
                stairCountText.text = $"x{currentTile.neighborStairCount}";
        }
    }
}
