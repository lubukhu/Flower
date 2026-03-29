using UnityEngine;

namespace finished3
{
    /// <summary>
    /// Thẻ dữ liệu (Data Card) chứa thông số gốc của một Nhân Vật hay Quái Vật.
    /// Giúp tách bạch Data ra khỏi Prefab, tiện lợi cho việc Cân Bằng Game (Game Balance) từ xa.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCharacterData", menuName = "Flower/Character Data", order = 1)]
    public class CharacterData : ScriptableObject
    {
        [Header("Base Stats")]
        [Tooltip("Lượng máu gốc tối đa")]
        public int maxHP = 10;
        
        [Tooltip("Sát thương cơ bản")]
        public int attack = 3;
        
        [Tooltip("Giáp phòng ngự")]
        public int defense = 1;

        [Header("Movement & Range")]
        [Tooltip("Tầm di chuyển trên Grid")]
        public int moveRange = 3;
        
        [Tooltip("Tầm bay của vũ khí / kĩ năng bám sát mục tiêu")]
        public int attackRange = 1;

        [Header("Audio Settings")]
        [Tooltip("Âm thanh dậm chân khi di chuyển")]
        public AudioClip moveSound;
        
        [Tooltip("Âm thanh vung vũ khí hoặc tung đòn tấn công")]
        public AudioClip attackSound;
        
        [Tooltip("Âm thanh khi bị dính đòn (Kêu la)")]
        public AudioClip hurtSound;

        [Tooltip("Âm thanh khi tử vong đứt gánh")]
        public AudioClip deathSound;
    }
}
