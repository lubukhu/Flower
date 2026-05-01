using UnityEngine;
using TMPro;

namespace finished3
{
    /// <summary>
    /// Quản lý các nút bấm ở Màn hình Menu chính.
    /// </summary>
    public class MainMenuUI : MonoBehaviour
    {
        public void OnClickPlay()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GoToCharacterSelection();
            }
        }

        public void OnClickQuit()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.QuitGame(); // Chú ý: Đã sửa tên hàm cho khớp logic
            }
        }
    }

    /// <summary>
    /// Quản lý giao diện chọn Class nhân vật.
    /// </summary>
    public class CharacterSelectionUI : MonoBehaviour
    {
        [Header("Class Data (Gán SO vào đây)")]
        public CharacterData guardianData;
        public CharacterData scoutData;
        public CharacterData scholarData;

        [Header("UI Preview")]
        public TextMeshProUGUI classNameText;
        public TextMeshProUGUI statsText;
        public TextMeshProUGUI descriptionText;

        private CharacterData currentSelected;

        private void Start()
        {
            // Mặc định chọn Hộ Vệ khi vừa vào
            SelectGuardian();
        }

        public void SelectGuardian()
        {
            currentSelected = guardianData;
            UpdatePreview("HỘ VỆ (GUARDIAN)", "HP: 6 (3 Tim)\nMP: 4 (2 Trăng)\nSteps: 30", "Sức chống chịu cao, phù hợp cho người mới bắt đầu.");
        }

        public void SelectScout()
        {
            currentSelected = scoutData;
            UpdatePreview("TRINH SÁT (SCOUT)", "HP: 4 (2 Tim)\nMP: 4 (2 Trăng)\nSteps: 50", "Di chuyển cực xa, giúp khám phá hầm ngục nhanh chóng.");
        }

        public void SelectScholar()
        {
            currentSelected = scholarData;
            UpdatePreview("HỌC GIẢ (SCHOLAR)", "HP: 4 (2 Tim)\nMP: 6 (3 Trăng)\nSteps: 25", "Năng lượng dồi dào, chuẩn bị cho việc thi triển nhiều phép thuật.");
        }

        private void UpdatePreview(string name, string stats, string desc)
        {
            if (classNameText) classNameText.text = name;
            if (statsText) statsText.text = stats;
            if (descriptionText) descriptionText.text = desc;
        }

        public void OnClickStartGame()
        {
            if (currentSelected != null && GameManager.Instance != null)
            {
                GameManager.Instance.StartGame(currentSelected);
            }
        }

        public void OnClickBack()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GoToMainMenu();
            }
        }
    }
}
