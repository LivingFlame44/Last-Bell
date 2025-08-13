using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private string volumeParameter = "music";

    private void Start()
    {
        if (myMixer == null)
        {
            Debug.LogError("AudioMixer not assigned in VolumeSettings!");
            return;
        }

        if (musicSlider == null)
        {
            Debug.LogError("Music Slider not assigned in VolumeSettings!");
            return;
        }

        // Set slider range (do this in inspector if possible)
        musicSlider.minValue = 0.0001f;
        musicSlider.maxValue = 1f;

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }

    public void SetMusicVolume()
    {
        if (myMixer == null) return;

        float volume = Mathf.Clamp(musicSlider.value, 0.0001f, 1f);

        // Convert to decibel scale (-80dB to 0dB)
        float dB = volume > 0.0001f ? Mathf.Log10(volume) * 20 : -80f;

        myMixer.SetFloat(volumeParameter, dB);
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();

        Debug.Log($"Set volume: {volume} (dB: {dB})");
    }

    private void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        musicSlider.value = savedVolume;
        SetMusicVolume();
    }
}