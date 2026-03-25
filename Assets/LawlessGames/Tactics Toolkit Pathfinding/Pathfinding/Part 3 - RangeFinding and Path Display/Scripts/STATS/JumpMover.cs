using UnityEngine;

namespace finished3
{
    public class JumpMover : MonoBehaviour
    {
        public float jumpHeight = 0.3f;
        public float sideOffsetAmount = 0.05f;
        public float jumpDuration = 0.25f;

        private bool isJumping = false;
        private float jumpTime = 0f;

        private Vector3 startPos;
        private Vector3 endPos;

        private System.Action onComplete;

        public bool IsJumping => isJumping;

        // 🔥 GỌI HÀM NÀY ĐỂ NHẢY
        public void StartJump(Vector3 targetPos, System.Action onFinish = null)
        {
            startPos = transform.position;
            endPos = targetPos;

            jumpTime = 0f;
            isJumping = true;
            onComplete = onFinish;
        }

        private void Update()
        {
            if (!isJumping) return;

            jumpTime += Time.deltaTime;
            float t = jumpTime / jumpDuration;

            if (t >= 1f)
            {
                t = 1f;
                isJumping = false;
            }

            // 🔥 easing cho mượt
            float easedT = Mathf.SmoothStep(0f, 1f, t);

            // base move
            Vector3 pos = Vector3.Lerp(startPos, endPos, easedT);

            // 🔥 NHẢY LÊN (parabola)
            float height = Mathf.Sin(easedT * Mathf.PI) * jumpHeight;

            // 🔥 HƯỚNG DI CHUYỂN
            Vector3 dir = (endPos - startPos).normalized;

            // vector vuông góc để lắc ngang
            Vector3 perpendicular = new Vector3(-dir.y, dir.x, 0);

            // 🔥 LẮC NHẸ (giảm dần ở đầu và cuối)
            float sway = Mathf.Sin(easedT * Mathf.PI * 2f) * sideOffsetAmount;
            float swayFade = Mathf.Sin(easedT * Mathf.PI); // fade in/out

            pos += perpendicular * sway * swayFade;
            pos.y += height;

            transform.position = pos;

            // kết thúc
            if (!isJumping)
            {
                onComplete?.Invoke();
            }
        }
    }
}