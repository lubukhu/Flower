using UnityEngine;

namespace finished3
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void Attack(CharacterStats attacker, CharacterStats target)
        {
            if (attacker == null || target == null) return;

            // 🎵 Phát âm thanh tấn công
            if (attacker.characterData != null) 
            {
                attacker.characterData.PlayRandomAttack();
            }

            // Thực hiện Animation lao tới (nếu có)
            var attackMover = attacker.GetComponent<AttackMover>();
            if (attackMover != null)
            {
                attackMover.StartAttack(target.transform.position, () => 
                {
                    GameLogger.Log($"{attacker.name} attack {target.name}");
                    target.TakeDamage(attacker.attack);
                });
            }
            else
            {
                // Fallback nếu không có Script diễn hoạt
                GameLogger.Log($"{attacker.name} attack {target.name}");
                target.TakeDamage(attacker.attack);
            }
        }
    }
}