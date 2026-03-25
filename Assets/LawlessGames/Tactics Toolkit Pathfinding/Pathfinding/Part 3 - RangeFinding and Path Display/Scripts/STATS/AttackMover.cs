using UnityEngine;
using System;

namespace finished3
{
    public class AttackMover : MonoBehaviour
    {
        public float attackDuration = 0.2f;
        public float lungeDistance = 0.4f;

        private bool isAttacking = false;
        private float timer = 0f;

        private Vector3 startPos;
        private Vector3 targetPos;

        private Action onComplete;

        public bool IsAttacking => isAttacking;

        public void StartAttack(Vector3 enemyPos, Action onFinish = null)
        {
            startPos = transform.position;

            // hướng từ player tới enemy
            Vector3 dir = (enemyPos - startPos).normalized;

            // chỉ lao tới một đoạn, không tới hẳn tile enemy
            targetPos = startPos + dir * lungeDistance;

            timer = 0f;
            isAttacking = true;
            onComplete = onFinish;
        }

        private void Update()
        {
            if (!isAttacking) return;

            timer += Time.deltaTime;
            float t = timer / attackDuration;

            if (t >= 1f)
            {
                t = 1f;
                isAttacking = false;
            }

            // chia làm 2 phase: đi tới (0→0.5), quay lại (0.5→1)
            float phase;

            if (t < 0.5f)
            {
                phase = t * 2f;
                transform.position = Vector3.Lerp(startPos, targetPos, phase);
            }
            else
            {
                phase = (t - 0.5f) * 2f;
                transform.position = Vector3.Lerp(targetPos, startPos, phase);
            }

            if (!isAttacking)
            {
                transform.position = startPos;
                onComplete?.Invoke();
            }
        }
    }
}