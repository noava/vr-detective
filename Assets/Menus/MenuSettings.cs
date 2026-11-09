using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer myMixer;

    [Header("UI Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // Master Volume
        if (PlayerPrefs.HasKey("masterVolume")) 
            masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        else 
            masterSlider.value = 1f;

        // Music Volume
        if (PlayerPrefs.HasKey("musicVolume")) 
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        else 
            musicSlider.value = 1f;

        // SFX Volume
        if (PlayerPrefs.HasKey("sfxVolume")) 
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        else 
            sfxSlider.value = 1f;

        // Force-apply the volume based on the set slider values
        SetMasterVolume(masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }

    // Master Slider
    public void SetMasterVolume(float volume)
    {
        myMixer.SetFloat("MasterVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    // Music Slider
    public void SetMusicVolume(float volume)
    {
        myMixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    // SFX Slider
    public void SetSFXVolume(float volume)
    {
        myMixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
}
