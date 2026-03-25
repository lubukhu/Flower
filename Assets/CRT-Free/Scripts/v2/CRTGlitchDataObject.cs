using UnityEngine;

namespace BrewedInk.CRT
{
    [CreateAssetMenu(menuName = "BrewedInk/CRT-GlitchData")]
    public class CRTGlitchDataObject : ScriptableObject
    {
        [Range(0f, 1f)] public float intensity = 1f;

        [Header("Noise")]
        [Range(0f, 1f)] public float dithering4 = 1f;
        [Range(0f, 1f)] public float dithering8 = 1f;

        [Header("Pixelation")]
        [Range(1, 100)] public int pixelation = 30;

        [Header("Color Shift")]
        [Range(-0.5f, 0.5f)] public float redBlueShift = 0.2f;
        [Range(0f, 10f)] public float scanSize = 5f;

        [Header("Vignette")]
        [Range(0f, 1f)] public float vignette = 0.5f;

        [Header("Greyscale")]
        [Range(0f, 1f)] public float greyscale = 0.2f;

        [Header("Flicker")]
        public bool flicker = true;

        // 🔥 ADD THIS
        [Header("Color")]
        public Color tint = Color.white;
    }
}