using UnityEngine;

namespace finished3
{
    public class CharacterStats : MonoBehaviour
    {
        [Header("Base Stats")]
        public int maxHP = 10;
        public int currentHP;

        public int attack = 3;
        public int defense = 1;

        public int moveRange = 3;
        public int attackRange = 1;

        private void Awake()
        {
            currentHP = maxHP;
        }

        public void TakeDamage(int damage)
        {
            int finalDamage = Mathf.Max(damage - defense, 1);
            currentHP -= finalDamage;

            Debug.Log(gameObject.name + " take damage: " + finalDamage);

            if (currentHP <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log(gameObject.name + " died");
            Destroy(gameObject);
        }
    }
}