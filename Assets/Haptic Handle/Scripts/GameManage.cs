using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    public static GameManage instance;

    public WeaponType CurrentStage;
    public WeaponType HoldingWeapon;
    public GameLevel CurrentLevel;

    //Game Objects
    public GameObject[] Sword_Obstacles;
    public GameObject[] Gun_Obstacles;
    public GameObject[] Shield_Obstacles;
    public GameObject[] Hammer_Obstacles;
    public Transform[] Obstacle_points;


    //Game UI
    public Slider TorqueGauge;
    public Slider RemainTime;
    public TextMeshProUGUI GameLevel_Text;
    public GameObject WarningUI;

    //Game Beat
    public float Music_Beat = (60 / 95) * 2; //105
    private float Current_Music_Beat = (60 / 95) * 2;

    private float Total_Timer;
    private float Beat_timer;

    //Badge system
    public bool Sword_Master = false;
    public bool Gun_Master = false;
    public bool Shield_Master = false;
    public bool Hammer_Master = false;
    public bool Is_Boss_Possible = false;
    public GameObject Sword_Badge;
    public GameObject Gun_Badge;
    public GameObject Shield_Badge;
    public GameObject Hammer_Badge;
    public GameObject ToolMaster_Badge; 
    public GameObject ToolMaster_UI; 
    public TextMeshProUGUI HitMessage;

    // Start is called before the first frame update


    public enum WeaponType
    {
        Sword,
        Gun,
        Shield,
        Hammer,
        Boss,
        Main,
        // Add more weapon types as needed
    }

    public enum GameLevel
    {
        Easy,
        Normal,
        Hard,
        // Add more weapon types as needed
    }

    private void Awake()
    {
        instance = this;
        TorqueGauge.maxValue = 30;
    }
    void Start()
    {
        
        HitMessage.enabled = false;

        //CurrentWeapon = WeaponType.Sword;
        RemainTime.value = 180;

        // Make sure there's only one instance of this object
        //if (FindObjectsOfType<GameManage>().Length > 1)
        //{
        //    Destroy(gameObject);
        //}
        //// Keep this object alive across scene changes
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Obstacle_Play();
        Update_GameLevel();
        Total_Timer += Time.deltaTime;
        //Debug.Log(Total_Timer+ ": 경과");
        GameOverCheck();
        BadgeCheck();
        
    }
    public void LoadTargetScene(string targetSceneName)
    {
        if (targetSceneName == "BeatDevil")
        {
            if (Is_Boss_Possible == true)
            {
                SceneManager.LoadScene(targetSceneName);
            }
            else
            {
                Debug.Log("The Beat Devil is too strong. You need to master all Tools first!");
                StartCoroutine(ShowWarning());
            }
        }
        else
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
    IEnumerator ShowWarning()
    {
        WarningUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        WarningUI.SetActive(false);
    }
    public void BadgeImageOn()
    {
        if (Sword_Master == true)
        {
            Sword_Badge.SetActive(true);
            Debug.Log("Sword master!!!!!!!!");

        }
        if (Gun_Master == true)
        {
            Gun_Badge.SetActive(true);
        }
        if (Shield_Master == true)
        {
            Shield_Badge.SetActive(true);
        }
        if (Hammer_Master == true)
        {
            Hammer_Badge.SetActive(true);
        }
        if(Sword_Master == true && Gun_Master == true && Shield_Master == true && Hammer_Master == true)
        {
            if (!Is_Boss_Possible)
            {
                Is_Boss_Possible = true;
                StartCoroutine(BecameToolMaster());
            }
        }
    }
    public IEnumerator BecameToolMaster()
    {
        ToolMaster_UI.SetActive(true);

        yield return new WaitForSeconds(3f);
        ToolMaster_UI.SetActive(false);

        ToolMaster_Badge.SetActive(true);

    }
    public void BadgeCheck()
    {
        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            if (TorqueGauge.value == TorqueGauge.maxValue)
            {
                switch (CurrentStage)
                {
                    case WeaponType.Sword:
                        Debug.Log("You Mastered Sword!");
                        Sword_Master = true;

                        break;
                    case WeaponType.Gun:
                        Gun_Master = true;
                        Debug.Log("You Mastered Gun!");
                        break;
                    case WeaponType.Shield:
                        Shield_Master = true;
                        Debug.Log("You Mastered Shield!");
                        break;
                    case WeaponType.Hammer:
                        Hammer_Master = true;
                        Debug.Log("You Mastered Hammer!");
                        break;
                    default:
                        break;
                }
                DestroyObjectsWithTag("BeatCube");
                DestroyObjectsWithTag("Blue");
                DestroyObjectsWithTag("Red");
                BadgeImageOn();
            }
        }
        else
        {
            BadgeImageOn();
        }
    }
    public void GameOverCheck()
    {
        if (SceneManager.GetActiveScene().name != "MainScene")
        { 
            RemainTime.value = 180 - Total_Timer;
            if (RemainTime.value <= 0)
            {
                Debug.Log("Game Over!");
                CurrentStage = WeaponType.Main;
            }
        }
    }

    public void Update_GameLevel()
    {
        if (TorqueGauge.value < TorqueGauge.maxValue / 3)
        {
            CurrentLevel = GameLevel.Easy;
        }
        else if (TorqueGauge.value >= TorqueGauge.maxValue * 1 / 3 &&TorqueGauge.value < TorqueGauge.maxValue * 2 / 3)
        {
            CurrentLevel = GameLevel.Normal;
        }
        else if (TorqueGauge.value >= TorqueGauge.maxValue * 2 / 3 && TorqueGauge.value < TorqueGauge.maxValue)
        {
            CurrentLevel = GameLevel.Hard;
        }
        switch (CurrentLevel)
        {
            case GameLevel.Easy:
                //Debug.Log("Easy");
                Current_Music_Beat = Music_Beat;
                GameLevel_Text.text = "Easy";
                break;
            case GameLevel.Normal:
                //Debug.Log("Normal");
                Current_Music_Beat = Music_Beat/2f;
                GameLevel_Text.text = "Normal";
                break;
            case GameLevel.Hard:
                //Debug.Log("Hard");
                int randomN = Random.Range(0, 10);
                if (randomN >= 8)
                {
                    Current_Music_Beat = Music_Beat / 3;
                }
                else
                {
                    Current_Music_Beat = Music_Beat / 2;
                }
                GameLevel_Text.text = "Hard";
                break;
            default:
                Current_Music_Beat = Music_Beat;
                break;
        }
    }
    public void Obstacle_Play()
    {
        switch (CurrentStage)
        {
            case WeaponType.Sword:
                //Debug.Log("Sword");
                Sword_Obstacle();
                break;
            case WeaponType.Gun:
                //Debug.Log("Gun");
                Gun_Obstacle();
                break;
            case WeaponType.Shield:
                //Debug.Log("Shield");
                Shield_Obstacle();
                break;
            case WeaponType.Hammer:
                //Debug.Log("Hammer");
                Hammer_Obstacle();
                break;
            case WeaponType.Boss:
                //Debug.Log("Hammer");
                Hammer_Obstacle();
                break;
            case WeaponType.Main:
                //Debug.Log("Hammer");
                //Hammer_Obstacle();
                break;
            default:
                //Sword_Obstacle();
                break;
        }
    }
    void DestroyObjectsWithTag(string tag)
    {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }
    public void Sword_Obstacle()
    {
        if (Beat_timer > Current_Music_Beat)
        {
            GameObject cube = Instantiate(Sword_Obstacles[Random.Range(0, 2)], Obstacle_points[Random.Range(0, 6)]);
            cube.transform.localPosition = Vector3.zero;
            cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            Beat_timer -= Current_Music_Beat;
        }
        Beat_timer += Time.deltaTime;
    }
    public void Gun_Obstacle()
    {
        if (Beat_timer > Current_Music_Beat)
        {
            GameObject cube = Instantiate(Gun_Obstacles[Random.Range(0, 2)], Obstacle_points[Random.Range(0, 6)]);
            cube.transform.localPosition = Vector3.zero;
            cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            Beat_timer -= Current_Music_Beat;
        }
        Beat_timer += Time.deltaTime;
    }
    public void Shield_Obstacle()
    {
        if (Beat_timer > Current_Music_Beat)
        {
            GameObject cube = Instantiate(Shield_Obstacles[Random.Range(0, 2)], Obstacle_points[Random.Range(0, 6)]);
            cube.transform.localPosition = Vector3.zero;
            //cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            Beat_timer -= Current_Music_Beat;
        }
        Beat_timer += Time.deltaTime;
    }
    public void Hammer_Obstacle()
    {
        if (Beat_timer > Current_Music_Beat)
        {
            GameObject cube = Instantiate(Hammer_Obstacles[Random.Range(0, 2)], Obstacle_points[Random.Range(0, 6)]);
            cube.transform.localPosition = Vector3.zero;
            //cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            Beat_timer -= Current_Music_Beat;
        }
        Beat_timer += Time.deltaTime;
    }

    public IEnumerator ShowHitMessage(string msg, Color textColor)
    {
        // Display hit message
        HitMessage.enabled = true;
        HitMessage.text = msg;
        HitMessage.color = textColor;
        // Wait for the specified duration
        yield return new WaitForSeconds(1f);

        // Hide hit message after the specified duration
        HitMessage.enabled = false;
    }
}
