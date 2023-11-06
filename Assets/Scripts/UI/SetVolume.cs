using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider MasterVolSlider;
    public Slider sfxVolSlider;
    public Slider bgmVolSlider;
    public Slider TTSVolSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        MasterVolSlider.value = PlayerPrefs.GetFloat("masterVol", 0.75f);
        SetMasterLevel(MasterVolSlider.value);
        sfxVolSlider.value = PlayerPrefs.GetFloat("sfxVol", 0.75f);
        SetSFXLevel(sfxVolSlider.value);
        bgmVolSlider.value = PlayerPrefs.GetFloat("bgmVol", 0.75f);
        SetBGMLevel(bgmVolSlider.value);
        TTSVolSlider.value = PlayerPrefs.GetFloat("TTSVol", 0.75f);
        SetTTSLevel(TTSVolSlider.value);
    }

    public void SetMasterLevel(float sliderValue)
    {
        mixer.SetFloat("masterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("masterVol", sliderValue);
    }
    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("sfxVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("sfxVol", sliderValue);
    }
    public void SetBGMLevel(float sliderValue)
    {
        mixer.SetFloat("bgmVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("bgmVol", sliderValue);
    }
    public void SetTTSLevel(float sliderValue)
    {
        mixer.SetFloat("TTSVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("TTSVol", sliderValue);
    }
}
