using UnityEngine;
using System.Collections.Generic;
using BrewedInk.CRT;

[System.Serializable]
public class HorrorSoundData
{
    public AudioClip clip;

    [Header("Volume")]
    public Vector2 volumeRange = new Vector2(0.3f, 0.8f);

    [Header("Delay before play")]
    public Vector2 delayRange = new Vector2(1f, 3f);

    [Header("Glitch Presets")]
    public List<CRTGlitchDataObject> glitchPresets;

    [Header("Glitch Duration")]
    public Vector2 glitchDurationRange = new Vector2(0.1f, 0.3f);
}