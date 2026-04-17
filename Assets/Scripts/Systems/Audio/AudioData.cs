using UnityEngine;

namespace finished3
{
    /// <summary>
    /// Chứa thông tin cấu hình cho một biến thể âm thanh đơn lẻ.
    /// Cho phép tinh chỉnh Volume, Pitch và Stereo Pan độc lập.
    /// </summary>
    [System.Serializable]
    public struct AudioVariant
    {
        public AudioClip clip;
        [Range(0f, 5f)] public float volume;
        [Range(0.1f, 3f)] public float pitch;
        [Range(-1f, 1f)] public float stereoPan;

        /// <summary>
        /// Phát biến thể âm thanh này. Hỗ trợ hệ số nhân âm lượng và chế độ lặp.
        /// Trả về Sound ID để có thể quản lý sau này (dừng nhạc, thay đổi âm lượng động).
        /// </summary>
        public int Play(float volumeMultiplier = 1f, bool loop = false)
        {
            if (clip == null) return -1;

            // Tính toán Volume (Mặc định 1 nếu để 0)
            float finalVol = (volume > 0 ? volume : 1f) * volumeMultiplier;

            // Phát âm thanh
            int id = Hellmade.Sound.EazySoundManager.PlaySound(clip, finalVol, loop, null);
            var audio = Hellmade.Sound.EazySoundManager.GetAudio(id);

            if (audio != null)
            {
                // Áp dụng Pitch (Mặc định 1 nếu để 0)
                audio.Pitch = pitch > 0 ? pitch : 1f;
                // Áp dụng Stereo Pan
                audio.StereoPan = stereoPan;
            }

            return id;
        }
    }

    /// <summary>
    /// Bộ cài đặt âm thanh cho một hành động (Ví dụ: Tấn công, Di chuyển).
    /// Hỗ trợ danh sách nhiều biến thể để phát ngẫu nhiên.
    /// </summary>
    [System.Serializable]
    public struct AudioActionSettings
    {
        public AudioVariant[] audioVariants;

        /// <summary>
        /// Bốc ngẫu nhiên một biến thể và phát nó.
        /// </summary>
        public void PlayRandom(float volumeMultiplier = 1f)
        {
            if (audioVariants == null || audioVariants.Length == 0) return;

            // Bốc ngẫu nhiên 1 biến thể
            int randomIndex = UnityEngine.Random.Range(0, audioVariants.Length);
            audioVariants[randomIndex].Play(volumeMultiplier);
        }
    }
}
