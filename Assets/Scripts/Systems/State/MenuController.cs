using UnityEngine;

namespace finished3
{
    /// <summary>
    /// Quản lý logic cơ bản cho cảnh Menu, bao gồm phát nhạc nền (BGM).
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        [Header("Audio Settings (Layered)")]
        [Tooltip("Danh sách các lớp nhạc nền. Index 0 là Nhạc chính (Music), Index 1+ là các lớp đệm (Ambience - Loop).")]
        public AudioActionSettings[] layeredPlaylists;

        private void Start()
        {
            PlayMenuMusic();
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
