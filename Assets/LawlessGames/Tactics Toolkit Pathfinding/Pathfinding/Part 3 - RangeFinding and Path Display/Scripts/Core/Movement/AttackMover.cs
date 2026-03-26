using UnityEngine;

namespace finished3
{
    public class AttackMover : MonoBehaviour
    {
        public float attackDuration = 0.2f;
        public float lungeDistance = 0.15f;
        public float tiltAmount = 20f;

        private bool isAttacking = false;
        private float attackTime = 0f;

        private Vector3 startPos;
        private Vector3 targetPos;

        private System.Action onComplete;

        public bool IsAttacking => isAttacking;

        public void StartAttack(Vector3 enemyPos, System.Action onFinish = null)
        {
            startPos = transform.position;

            Vector3 dir = (enemyPos - startPos).normalized;
            targetPos = startPos + dir * lungeDistance;

            attackTime = 0f;
            isAttacking = true;
            onComplete = onFinish;
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

            // 🔥 forward → backward (ping pong)
            float moveT = Mathf.Sin(easedT * Mathf.PI);

            // position
            transform.position = Vector3.Lerp(startPos, targetPos, moveT);

            // 🔥 tilt theo hướng
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