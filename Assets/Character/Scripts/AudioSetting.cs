using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXslider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else {
            SetMusicVolume();
            SetSFXvolume();
        }
    }

    public void SetSFXvolume()
    {
        float volume = SFXslider.value;
        audioMixer.SetFloat("sfx", volume); //audioMixer mengambil mengambil nilai SetFloat dari parameter "sfx" di window audio mixer unity
        PlayerPrefs.SetFloat("sfxVolume", volume); // "sfxVolume" is Key Name not variable or parameter (like "sfx" or "music" in method SetMusicVolume())
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume)*20); //Rumus konversi standar fisika akustik untuk mengubah rasio kekuatan audio linier menjadi satuan Desibel (dB).
        PlayerPrefs.SetFloat("musicVolume", volume); //Teks "musicVolume" Key Name buatan sendiri untuk sistem penyimpanan data bawaan Unity bernama PlayerPrefs
    }

    void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        SFXslider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetMusicVolume();
        SetSFXvolume();
    }
}
