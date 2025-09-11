using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgSlider;
    [SerializeField] private Slider fxSlider;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("masterVolume") && PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("fxVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetFxVolume();
        }
    }
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }
    public void SetMusicVolume()
    {
        float volume = bgSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetFxVolume()
    {
        float volume = fxSlider.value;
        audioMixer.SetFloat("fx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("fxVolume", volume);
    }
    void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        bgSlider.value = PlayerPrefs.GetFloat("musicVolume");
        fxSlider.value = PlayerPrefs.GetFloat("fxVolume");
        SetMasterVolume();
        SetMusicVolume();
        SetFxVolume();
    }
}