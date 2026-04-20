using UnityEngine;
using UnityEngine.SceneManagement;

namespace finished3
{
    /// <summary>
    /// Quản lý logic cơ bản cho cảnh Menu, bao gồm phát nhạc nền (BGM) và chuyển cảnh.
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        [Header("Scene Settings")]
        [Tooltip("Tên cảnh muốn chuyển đến khi người chơi Click.")]
        public string startSceneName = "Chapter_1";

        [Header("Audio Settings (Layered)")]
        [Tooltip("Danh sách các lớp nhạc nền. Index 0 là Nhạc chính (Music), Index 1+ là các lớp đệm (Ambience - Loop).")]
        public AudioActionSettings[] layeredPlaylists;

        [Tooltip("Âm thanh vang lên khi người chơi click để bắt đầu game.")]
        public AudioActionSettings clickSfx;

        private bool isStarting = false;

        private void Start()
        {
            PlayMenuMusic();
        }

        private void Update()
        {
            // Nếu người chơi click chuột trái và chưa ở trong trạng thái đang chuyển cảnh
            if (Input.GetMouseButtonDown(0) && !isStarting)
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            isStarting = true;
            GameLogger.Log($"MenuController: Người chơi đã click. Đang bắt đầu chuyển sang cảnh {startSceneName}...");

            // 🎵 Phát âm thanh click (SFX)
            clickSfx.PlayRandom();

            // 🔇 Dừng toàn bộ âm thanh của Menu trước khi chuyển cảnh
            Hellmade.Sound.EazySoundManager.StopAll();

            // 🚀 Chuyển cảnh
            SceneManager.LoadScene(startSceneName);
        }

        private void PlayMenuMusic()
        {
            if (layeredPlaylists == null || layeredPlaylists.Length == 0) return;

            for (int i = 0; i < layeredPlaylists.Length; i++)
            {
                if (i == 0)
                {
                    // Lớp 0: Nhạc chính (Kênh Music)
                    layeredPlaylists[i].PlayRandomMusic(1f);
                }
                else
                {
                    // Lớp 1 trở đi: Nhạc môi trường/ Drone (Kênh Sound - Loop)
                    layeredPlaylists[i].PlayRandom(1f, true);
                }
            }

            GameLogger.Log($"MenuController: Đã kích hoạt {layeredPlaylists.Length} lớp nhạc nền song song.");
        }
    }
}
