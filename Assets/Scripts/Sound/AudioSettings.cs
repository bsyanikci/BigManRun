using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    public bool EnableMusic;
    public bool EnableSfx;
    public float MasterVolume;
    public AudioSettings()
    {
        EnableMusic = true;
        EnableSfx = true;
        MasterVolume = 0.25f;
    }
}