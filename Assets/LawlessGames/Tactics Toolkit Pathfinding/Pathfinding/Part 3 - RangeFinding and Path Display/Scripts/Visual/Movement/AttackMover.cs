using UnityEngine;

namespace finished3
{
    public class AttackMover : MonoBehaviour
    {
        public float attackDuration = 0.25f;
        public float stopDistance = 0.2f; // khoảng cách dừng trước enemy
        public float tiltAmount = 25f;

        private bool isAttacking = false;
        private bool hasHit = false;

        private float attackTime = 0f;

        private Vector3 startPos;
        private Vector3 targetPos;

        private System.Action onComplete;

        public bool IsAttacking => isAttacking;

        public void StartAttack(Vector3 enemyPos, System.Action onFinish = null)
        {
            startPos = transform.position;

            Vector3 dir = (enemyPos - startPos).normalized;

            // 🔥 lao tới gần enemy (không chồng lên)
            targetPos = enemyPos - dir * stopDistance;

            attackTime = 0f;
            isAttacking = true;
            onComplete = onFinish;
            hasHit = false;
        }

        private void Update()
        {
            if (!isAttacking) return;

            attackTime += Time.deltaTime;
            float t = attackTime / attackDuration;

            if (t >= 1f)
            {
                t = 1f;
                isAttacking = false;
            }

            float easedT = Mathf.SmoothStep(0f, 1f, t);

            // 🔥 tiến → lùi (attack motion)
            float moveT = Mathf.Sin(easedT * Mathf.PI);
            // 🔥 IMPACT FRAME (đỉnh của sin = lúc chạm)
            if (!hasHit && moveT >= 0.95f)
            {
                hasHit = true;
                onComplete?.Invoke(); // 🔥 gọi damage ở đây
            }
            // position
            transform.position = Vector3.Lerp(startPos, targetPos, moveT);

            // 🔥 tilt theo hướng enemy
            float dx = targetPos.x - startPos.x;
            float direction = Mathf.Abs(dx) > 0.01f ? Mathf.Sign(dx) : 1f;

            float tilt = Mathf.Sin(easedT * Mathf.PI) * tiltAmount * direction;
            transform.rotation = Quaternion.Euler(0, 0, tilt);

            // finish
            if (!isAttacking)
            {
                transform.position = startPos;
                transform.rotation = Quaternion.Euler(0, 0, 0);

                onComplete?.Invoke();
            }
        }
    }
}