using UnityEngine;

namespace finished3
{
    public class HitEffect : MonoBehaviour
    {
        public float shakeDuration = 0.15f;
        public float shakeStrength = 0.1f;

        private float timer = 0f;
        private Vector3 originalPos;
        private bool isShaking = false;

        public void PlayHit()
        {
            originalPos = transform.position;
            timer = 0f;
            isShaking = true;
        }

        private void Update()
        {
            if (!isShaking) return;

            timer += Time.deltaTime;

            if (timer >= shakeDuration)
            {
                isShaking = false;
                transform.position = originalPos;
                return;
            }

            Vector3 offset = Random.insideUnitCircle * shakeStrength;
            transform.position = originalPos + offset;
        }
    }
}