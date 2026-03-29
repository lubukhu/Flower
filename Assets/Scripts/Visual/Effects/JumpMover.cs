using UnityEngine;

namespace finished3
{
    public class JumpMover : MonoBehaviour
    {
        [Header("Jump Settings")]
        public float jumpHeight = 0.3f;
        public float sideOffsetAmount = 0.05f;
        public float jumpDuration = 0.25f;

        [Header("Tilt Settings")]
        public float rotationAmount = 15f;
        [Range(0f, 1f)]
        public float tiltChance = 0.7f; // % có lắc

        private bool isJumping = false;
        private float jumpTime = 0f;

        private Vector3 startPos;
        private Vector3 endPos;

        private System.Action onComplete;

        private bool useTilt;

        public bool IsJumping => isJumping;

        // 🔥 GỌI HÀM NÀY ĐỂ NHẢY
        public void StartJump(Vector3 targetPos, System.Action onFinish = null)
        {
            startPos = transform.position;
            endPos = targetPos;

            jumpTime = 0f;
            isJumping = true;
            onComplete = onFinish;

            // 🎯 Random có lắc hay không
            useTilt = Random.value < tiltChance;
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

            // =====================
            // 🔹 BASE MOVE
            // =====================
            Vector3 pos = Vector3.Lerp(startPos, endPos, easedT);

            // =====================
            // 🔹 TILT (rotation Z)
            // =====================
            if (useTilt)
            {
                float dx = endPos.x - startPos.x;
                float direction = Mathf.Abs(dx) > 0.01f ? Mathf.Sign(dx) : 1f;

                float tilt = Mathf.Sin(easedT * Mathf.PI) * rotationAmount * direction;

                transform.rotation = Quaternion.Euler(0, 0, tilt);
            }

            // =====================
            // 🔹 JUMP (parabola)
            // =====================
            float height = Mathf.Sin(easedT * Mathf.PI) * jumpHeight;

            // =====================
            // 🔹 SWAY (lắc ngang)
            // =====================
            Vector3 dir = (endPos - startPos).normalized;
            Vector3 perpendicular = new Vector3(-dir.y, dir.x, 0);

            float sway = Mathf.Sin(easedT * Mathf.PI * 2f) * sideOffsetAmount;
            float swayFade = Mathf.Sin(easedT * Mathf.PI);

            pos += perpendicular * sway * swayFade;
            pos.y += height;

            // =====================
            // 🔹 APPLY POSITION
            // =====================
            transform.position = pos;

            // =====================
            // 🔹 FINISH
            // =====================
            if (!isJumping)
            {
                // reset rotation
                transform.rotation = Quaternion.Euler(0, 0, 0);

                // 🔥 nhẹ landing feel (optional)
                transform.localScale = new Vector3(1.05f, 0.95f, 1f);

                onComplete?.Invoke();
            }
        }
    }
}