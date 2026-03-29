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

            // 🔥 IMPACT FRAME (đỉnh của sin = lúc chạm = t đạt 50% thời gian)
            // Sửa lại thành xét theo t >= 0.5f. Chứ quét theo moveT >= 0.95f thì máy nào hơi Lag/Tụt FPS nó sẽ trượt vòng lặp làm đánh không ra Damge!
            if (!hasHit && t >= 0.5f)
            {
                hasHit = true;
                onComplete?.Invoke(); // 🔥 gọi damage an toàn và CHỈ GỌI 1 LẦN
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

                // 🔥 Xóa lệnh onComplete?.Invoke() ở đây để triệt tiêu lỗi bị đánh 2 lần (Double Hit) chèn Log
            }
        }
    }
}