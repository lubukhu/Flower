using UnityEngine;

namespace finished3
{
    public class HitEffect : MonoBehaviour
    {
        public float shakeDuration = 0.15f;
        public float shakeAmount = 0.05f;

        private float shakeTime = 0f;
        private bool isShaking = false;
        private Vector3 originalPos;

        public void PlayHit()
        {
            originalPos = transform.position;
            shakeTime = 0f;
            isShaking = true;
        }

        private void Update()
        {
            if (!isShaking) return;

            shakeTime += Time.deltaTime;
            float t = shakeTime / shakeDuration;

            if (t >= 1f)
            {
                isShaking = false;
                transform.position = originalPos;
                return;
            }

            Vector2 randomOffset = Random.insideUnitCircle * shakeAmount;
            transform.position = originalPos + (Vector3)randomOffset;
        }
    }
}