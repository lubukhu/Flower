using UnityEngine;
using TMPro;

namespace finished3
{
    /// <summary>
    /// Giao diện hiển thị các chỉ số Tài nguyên (Chỉ còn Lượt đi - Steps).
    /// Tự động lắng nghe sự kiện từ CharacterStats và kích hoạt Animation đồng hồ cát.
    /// </summary>
    public class ResourceHUD : MonoBehaviour
    {
        [Header("UI References")]
        [Tooltip("Kéo TextMeshPro hiển thị số Lượt đi vào đây")]
        public TextMeshProUGUI stepsText;

        [Header("Animation")]
        [Tooltip("Kéo đối tượng Đồng hồ cát có Animator vào đây")]
        public Animator hourglassAnimator;
        [Tooltip("Tên Trigger để kích hoạt animation khi bước đi")]
        public string stepTriggerName = "OnStep";

        private CharacterStats currentPlayerStats;

        private void OnEnable()
        {
            PlayerController.OnPlayerStep += HandlePlayerSpawned;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerStep -= HandlePlayerSpawned;
            
            if (currentPlayerStats != null)
            {
                currentPlayerStats.OnStepsChanged -= UpdateStepsUI;
            }
        }

        private void HandlePlayerSpawned(OverlayTile tile)
        {
            if (currentPlayerStats == null && PlayerController.Instance.IsPlayerSpawned)
            {
                currentPlayerStats = PlayerController.Instance.character.GetComponent<CharacterStats>();
                
                // Đăng ký nghe sự kiện
                currentPlayerStats.OnStepsChanged += UpdateStepsUI;
                
                // Cập nhật lần đầu
                UpdateStepsUI(currentPlayerStats.currentSteps, currentPlayerStats.maxSteps);
            }
        }

        private void UpdateStepsUI(int current, int max)
        {
            if (stepsText != null)
            {
                // Chỉ hiển thị số bước còn lại (Ví dụ: 30)
                stepsText.text = current.ToString();
                
                // Đổi màu cảnh báo nếu sắp hết Lượt đi (dưới 30%)
                if ((float)current / max <= 0.3f)
                {
                    stepsText.color = Color.red;
                }
                else
                {
                    stepsText.color = Color.white;
                }

                // Kích hoạt animation đồng hồ cát
                if (hourglassAnimator != null)
                {
                    hourglassAnimator.SetTrigger(stepTriggerName);
                }
            }
        }
    }
}
