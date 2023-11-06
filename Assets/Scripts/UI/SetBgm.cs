using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetBgm : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("bgmVol", 0.75f);
        SetLevel(slider.value);
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("bgmVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("bgmVol", sliderValue);
    }
}
