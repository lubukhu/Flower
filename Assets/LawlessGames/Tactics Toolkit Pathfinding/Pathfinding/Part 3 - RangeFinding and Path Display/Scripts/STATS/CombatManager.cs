using UnityEngine;

namespace finished3
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void Attack(CharacterStats attacker, CharacterStats target)
        {
            if (attacker == null || target == null) return;

            Debug.Log(attacker.name + " attack " + target.name);

            target.TakeDamage(attacker.attack);
        }
    }
}