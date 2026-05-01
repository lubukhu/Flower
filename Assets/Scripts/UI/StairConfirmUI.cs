using UnityEngine;
using UnityEngine.UI;

namespace finished3
{
    /// <summary>
    /// Quản lý giao diện Popup xác nhận có muốn bước xuống Cầu Thang hay không.
    /// Gắn script này vào 1 cái UI Panel nằm giữa màn hình.
    /// </summary>
    public class StairConfirmUI : MonoBehaviour
    {
        public static StairConfirmUI Instance { get; private set; }

        [Header("Giao diện UI")]
        public GameObject confirmPanel;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            // Mặc định ẩn Panel
            if (confirmPanel != null)
            {
                confirmPanel.SetActive(false);
            }
        }

        /// <summary>
        /// Bật cửa sổ Xác nhận
        /// </summary>
        public void ShowConfirmPanel()
        {
            if (confirmPanel != null)
            {
                confirmPanel.SetActive(true);
            }
            else
            {
                // Nếu chưa có UI, tự động chuyển tầng luôn (dành cho Test)
                GameLogger.LogWarning("Chưa thiết lập UI Xác nhận Cầu thang. Tự động chuyển tầng!");
                OnConfirmYes();
            }
        }

        /// <summary>
        /// Nối hàm này vào Event OnClick của nút "Đồng Ý" trên UI
        /// </summary>
        public void OnConfirmYes()
        {
            if (confirmPanel != null) confirmPanel.SetActive(false);

            if (DungeonManager.Instance != null)
            {
                DungeonManager.Instance.NextFloor();
            }
            else
            {
                GameLogger.LogError("FATAL: Không tìm thấy DungeonManager để chuyển tầng!");
            }
        }

        /// <summary>
        /// Nối hàm này vào Event OnClick của nút "Từ Chối" trên UI
        /// </summary>
        public void OnConfirmNo()
        {
            if (confirmPanel != null) confirmPanel.SetActive(false);
            GameLogger.Log("Đã hủy xuống tầng.");
        }
    }
}
