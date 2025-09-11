using UnityEngine.Audio;
using UnityEngine;

public class AudioVolumeLoader : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
            audioMixer.SetFloat("master", Mathf.Log10(PlayerPrefs.GetFloat("masterVolume")) * 20);
        if (PlayerPrefs.HasKey("musicVolume"))
            audioMixer.SetFloat("music", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        if (PlayerPrefs.HasKey("fxVolume"))
            audioMixer.SetFloat("fx", Mathf.Log10(PlayerPrefs.GetFloat("fxVolume")) * 20);
    }
}