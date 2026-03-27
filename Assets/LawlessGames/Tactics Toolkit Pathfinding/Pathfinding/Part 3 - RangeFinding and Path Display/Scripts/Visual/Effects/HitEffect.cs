using UnityEngine;

namespace finished3
{
    public class HitEffect : MonoBehaviour
    {
        [Header("Shake")]
        public float shakeDuration = 0.15f;
        public float shakeAmount = 0.05f;

        [Header("Squash")]
        public float squashAmount = 0.1f;

        [Header("Flash")]
        public Color flashColor = Color.white;

        private float shakeTime = 0f;
        private bool isShaking = false;

        private Vector3 originalPos;
        private Vector3 originalScale;
        private Color originalColor;

        private SpriteRenderer sr;

        // =====================
        // 🔹 INIT
        // =====================
        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            originalScale = transform.localScale;

            if (sr != null)
                originalColor = sr.color;
        }

        // =====================
        // 🔹 PLAY HIT
        // =====================
        public void PlayHit()
        {
            originalPos = transform.position;
            originalScale = transform.localScale;

            if (sr != null)
                originalColor = sr.color;

            shakeTime = 0f;
            isShaking = true;
        }

        // =====================
        // 🔹 UPDATE
        // =====================
        private void Update()
        {
            if (!isShaking) return;

            shakeTime += Time.deltaTime;
            float t = shakeTime / shakeDuration;

            if (t >= 1f)
            {
                isShaking = false;

                // reset
                transform.position = originalPos;
                transform.localScale = originalScale;

                if (sr != null)
                    sr.color = originalColor;

                return;
            }

            // =====================
            // 🔹 SHAKE (X only)
            // =====================
            float offsetX = Random.Range(-shakeAmount, shakeAmount);

            transform.position = new Vector3(
                originalPos.x + offsetX,
                originalPos.y,
                originalPos.z
            );

            // =====================
            // 🔹 SQUASH (co giãn)
            // =====================
            float squash = Mathf.Sin(t * Mathf.PI) * squashAmount;

            transform.localScale = new Vector3(
                1 + squash,
                1 - squash,
                1
            );

            // =====================
            // 🔹 FLASH (nháy trắng)
            // =====================
            if (sr != null)
            {
                float flashT = Mathf.Sin(t * Mathf.PI);
                sr.color = Color.Lerp(originalColor, flashColor, flashT);
            }
        }
    }
}