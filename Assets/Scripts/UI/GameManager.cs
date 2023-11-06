using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Platform.Samples.VrHoops;
using SpaceGraphicsToolkit.Cloudsphere;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class GameManager : MonoBehaviour
{
    public bool Pilot = false;
    
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();
            if (_instance == null)
            {
                GameObject container = new GameObject("GameManager");
                _instance = container.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    
    [SerializeField] private GameObject confidenceBar;
    private Slider _confidenceSlider;
    public float confidence;
    private float time;
    public void Awake()
    {
        _confidenceSlider = confidenceBar.GetComponentInChildren<Slider>();
        
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "Earth_plane")
        {
            PlayerPrefs.SetFloat("Confidence", 0.0f);
            PlayerPrefs.SetString("Dino_Reward", "");
            PlayerPrefs.SetString("Desert_Reward", "");
            PlayerPrefs.SetFloat("Confidence", 0.0f);
        }
        else if (SceneManager.GetActiveScene().name == "FinalScene")
        {
            GameObject reward1 = GameObject.Find("Dinosour");
            GameObject reward2 = GameObject.Find("Meteor");
            GameObject reward3 = GameObject.Find("Aurora");
            GameObject reward4 = GameObject.Find("Oasis");
            GameObject reward5 = GameObject.Find("Plane");
            if (PlayerPrefs.GetString("Dino_Reward", "") == "Reward1")
            {
                reward2.SetActive(false);
            }
            else if (PlayerPrefs.GetString("Dino_Reward", "") == "Reward2")
            {
                reward1.SetActive(false);
            }

            if (PlayerPrefs.GetString("Desert_Reward", "") == "Reward1")
            {
                reward4.SetActive(false);
                reward5.SetActive(false);
            }
            else if (PlayerPrefs.GetString("Desert_Reward", "") == "Reward2")
            {
                reward3.SetActive(false);
                reward5.SetActive(false);
            }
            else if (PlayerPrefs.GetString("Desert_Reward", "") == "Reward3")
            {
                reward3.SetActive(false);
                reward4.SetActive(false);
            }
        }
        _confidenceSlider.value = PlayerPrefs.GetFloat("Confidence", 0.0f);
        

    }
    // Start is called before the first frame update

    public void Confidence(float value)
    {
        confidence = value;
        confidenceBar.SetActive(true);
    }
    

    public void RewardSelect()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        Debug.Log(clickObject.name);
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Dino_Sample")
        {
            PlayerPrefs.SetString("Dino_Reward", clickObject.name);
            Debug.Log("Dino Reward Saved");
        }
        else if (scene.name == "PathInDesert_Sample")
        {
            PlayerPrefs.SetString("Desert_Reward", clickObject.name);
            Debug.Log("Desert Reward Saved");

        }
    }
}
