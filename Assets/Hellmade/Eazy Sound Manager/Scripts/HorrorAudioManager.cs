using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using BrewedInk.CRT;
using UnityEngine.SceneManagement;

public class HorrorAudioManager : MonoBehaviour
{
    [Header("Sound Data")]
    public List<HorrorSoundData> sounds;

    [Header("CRT Camera")]
    public CRTCameraBehaviour crtCamera;

    [Header("Global Timing")]
    public Vector2 globalDelayRange = new Vector2(1f, 3f);

    [Header("Settings")]
    public bool autoFindCamera = true;

    private int lastPlayedIndex = -1;

    void Start()
    {
        EnsureCamera();
        StartCoroutine(RandomSoundLoop());
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (autoFindCamera)
        {
            crtCamera = FindObjectOfType<CRTCameraBehaviour>();
        }
    }

    void EnsureCamera()
    {
        if (!autoFindCamera) return;

        if (crtCamera == null || crtCamera.Equals(null))
        {
            crtCamera = FindObjectOfType<CRTCameraBehaviour>();
        }
    }

    IEnumerator RandomSoundLoop()
    {
        while (true)
        {
            if (sounds == null || sounds.Count == 0)
                yield break;

            // ⏱ delay 1–3s
            float waitTime = Random.Range(globalDelayRange.x, globalDelayRange.y);
            yield return new WaitForSeconds(waitTime);

            // 🎲 random KHÔNG trùng
            int newIndex;

            if (sounds.Count == 1)
            {
                newIndex = 0;
            }
            else
            {
                do
                {
                    newIndex = Random.Range(0, sounds.Count);
                }
                while (newIndex == lastPlayedIndex);
            }

            lastPlayedIndex = newIndex;

            HorrorSoundData soundData = sounds[newIndex];

            if (soundData.clip == null)
                continue;

            float volume = Random.Range(soundData.volumeRange.x, soundData.volumeRange.y);

            // 🔊 play sound
            EazySoundManager.PlaySound(soundData.clip, volume);

            // 💀 glitch (nếu có)
            if (soundData.glitchPresets != null && soundData.glitchPresets.Count > 0)
            {
                StartCoroutine(GlitchEffect(soundData, volume));
            }
        }
    }

    IEnumerator GlitchEffect(HorrorSoundData soundData, float intensity)
    {
        EnsureCamera();

        if (crtCamera == null || crtCamera.Equals(null))
            yield break;

        if (crtCamera.data == null)
            yield break;

        if (soundData.glitchPresets == null || soundData.glitchPresets.Count == 0)
            yield break;

        CRTData original = crtCamera.data.Clone();

        CRTGlitchDataObject preset = soundData.glitchPresets[
            Random.Range(0, soundData.glitchPresets.Count)
        ];

        float duration = Random.Range(
            soundData.glitchDurationRange.x,
            soundData.glitchDurationRange.y
        );

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            crtCamera.data.dithering4 = preset.dithering4 * intensity;
            crtCamera.data.dithering8 = preset.dithering8 * intensity;

            crtCamera.data.pixelationAmount = preset.pixelation;

            crtCamera.data.colorScans.redBlueChannelMultiplier =
                Random.Range(-preset.redBlueShift, preset.redBlueShift);

            crtCamera.data.colorScans.sizeMultiplier = preset.scanSize;

            crtCamera.data.vignette = preset.vignette;
            crtCamera.data.maxColorChannels.greyScale = preset.greyscale;
            // 🔥 ADD THIS
            crtCamera.data.tint = preset.tint;
            if (preset.flicker && Random.value > 0.7f)
            {
                crtCamera.data.pixelationAmount = Random.Range(10, 80);
            }

            yield return null;
        }

        if (crtCamera != null && !crtCamera.Equals(null))
        {
            crtCamera.data = original;
        }
    }
}