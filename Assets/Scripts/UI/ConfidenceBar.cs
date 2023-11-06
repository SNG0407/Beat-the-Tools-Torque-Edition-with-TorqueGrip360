using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfidenceBar : MonoBehaviour
{
    [SerializeField] private GameObject confiImage;
    [SerializeField] private GameObject SliderImage1;
    [SerializeField] private GameObject SliderImage2;

    private Slider _confidenceSlider;
    // Start is called before the first frame update

    public void Awake()
    {
        _confidenceSlider = GetComponentInChildren<Slider>();
    }

    public IEnumerator Start()
    {
        GetComponent<Transform>().localScale.Set(1.5f, 1.5f, 1.0f); 
        confiImage.GetComponent<UIFade>().StartFadeIn();
        SliderImage1.GetComponent<UIFade>().StartFadeIn();
        SliderImage2.GetComponent<UIFade>().StartFadeIn();
        float start = PlayerPrefs.GetFloat("Confidence");
        _confidenceSlider.value = start;
        yield return new WaitForSeconds(3);
        float confiIncrease = GameManager.Instance.confidence;
        float time = 0;
        while (!(_confidenceSlider.value >= start + confiIncrease))
        {
            time += Time.deltaTime / 3f;
            
            _confidenceSlider.value = Mathf.Lerp(start, start+confiIncrease, time);
            yield return null;
        }
        PlayerPrefs.SetFloat("Confidence", _confidenceSlider.value);
        
        yield return new WaitForSeconds(2);
        confiImage.GetComponent<UIFade>().StartFadeOut();
        SliderImage1.GetComponent<UIFade>().StartFadeOut();
        SliderImage2.GetComponent<UIFade>().StartFadeOut();
        yield break;

    }
}
