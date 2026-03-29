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

            GameLogger.Log($"{attacker.name} attack {target.name}");

            target.TakeDamage(attacker.attack);
        }
    }
}