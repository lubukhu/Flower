using UnityEngine;

namespace finished3
{
    public class ClimbMover : MonoBehaviour
    {
        [Header("Climb Settings")]
        public float climbDuration = 0.3f;
        public float stepHeightAmount = 0.08f;

        [Header("Feel Settings")]
        public float startDelayThreshold = 0.1f; // 🔥 delay nhẹ lúc bắt đầu

        private bool isClimbing = false;
        private float time = 0f;

        private Vector3 startPos;
        private Vector3 endPos;

        private System.Action onComplete;

        public bool IsClimbing => isClimbing;

        // =====================
        // 🔹 START CLIMB
        // =====================
        public void StartClimb(Vector3 targetPos, System.Action onFinish = null)
        {
            startPos = transform.position;
            endPos = targetPos;

            time = 0f;
            isClimbing = true;
            onComplete = onFinish;
        }

        // =====================
        // 🔹 UPDATE
        // =====================
        private void Update()
        {
            if (!isClimbing) return;

            time += Time.deltaTime;
            float t = time / climbDuration;

            if (t >= 1f)
            {
                t = 1f;
                isClimbing = false;
            }

            // =====================
            // 🔹 BONUS: delay đầu (chống lướt)
            // =====================
            if (t < startDelayThreshold)
            {
                return;
            }

            // =====================
            // 🔹 EASING (mượt + đỡ trượt)
            // =====================
            float easedT = t * t * (3f - 2f * t);

            // =====================
            // 🔹 BASE MOVE
            // =====================
            Vector3 pos = Vector3.Lerp(startPos, endPos, easedT);

            // =====================
            // 🔹 STEP HEIGHT (🔥 tạo cảm giác leo)
            // =====================
            float stepHeight = Mathf.Sin(easedT * Mathf.PI) * stepHeightAmount;

            if (endPos.z > startPos.z) // chỉ áp dụng khi leo lên
            {
                pos.y += stepHeight;
            }

            // =====================
            // 🔹 APPLY POSITION
            // =====================
            transform.position = pos;

            // =====================
            // 🔹 FINISH
            // =====================
            if (!isClimbing)
            {
                transform.position = endPos;
                onComplete?.Invoke();
            }
        }
    }
}