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
        
        [Tooltip("Lượt đi (Steps) giới hạn để khám phá hầm ngục")]
        public int maxSteps = 30;

        [Tooltip("Năng lượng ma thuật (Mana) để dùng skill")]
        public int maxMana = 10;

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
        public AudioActionSettings moveAudio;
        public AudioActionSettings attackAudio;
        public AudioActionSettings hurtAudio;
        public AudioActionSettings deathAudio;
        public AudioActionSettings cannotMoveAudio; // Âm thanh cảnh báo khi bị chặn di chuyển

        #region Helper functions to play sound
        public void PlayRandomMove() => moveAudio.PlayRandom();
        public void PlayRandomAttack() => attackAudio.PlayRandom();
        public void PlayRandomHurt() => hurtAudio.PlayRandom();
        public void PlayRandomDeath() => deathAudio.PlayRandom();
        public void PlayRandomCannotMove() => cannotMoveAudio.PlayRandom();
        #endregion
    }
}
