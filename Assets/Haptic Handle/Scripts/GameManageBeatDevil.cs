using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManageBeatDevil : MonoBehaviour
{
    public static GameManageBeatDevil instance;

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
    public Slider HPGauge;
    public Slider RemainTime;
    public TextMeshProUGUI GameLevel_Text;

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

    // Start is called before the first frame update
    public TextMeshProUGUI HitMessage;

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
    }
    void Start()
    {
        HitMessage.enabled = false;

        //CurrentWeapon = WeaponType.Sword;
        RemainTime.value = 180;

        // Make sure there's only one instance of this object
        if (FindObjectsOfType<GameManage>().Length > 1)
        {
            Destroy(gameObject);
        }
        // Keep this object alive across scene changes
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Obstacle_Play();
        Update_GameLevel();
        Total_Timer += Time.deltaTime;
        //Debug.Log(Total_Timer+ ": 경과");
        GameOverCheck();
        //BadgeCheck();

    }
    public void LoadTargetScene(string targetSceneName)
    {
        SceneManager.LoadScene(targetSceneName);
    }


    public void BadgeCheck()
    {
        if(TorqueGauge.value == TorqueGauge.maxValue)
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
        }
    }
    public void GameOverCheck()
    {
        RemainTime.value = 180 - Total_Timer;
        if (RemainTime.value <= 0)
        {
            Debug.Log("Game Over!");
        }
    }

    public void Update_GameLevel()
    {
        if(HPGauge.value>70.0 && HPGauge.value <= 100.0)
        {
            CurrentLevel = GameLevel.Easy;
        }else if (HPGauge.value > 40.0 && HPGauge.value <= 70.0)
        {
            CurrentLevel = GameLevel.Normal;
        }
        if (HPGauge.value > 0.0 && HPGauge.value <= 40.0)
        {
            CurrentLevel = GameLevel.Hard;
        }
        if (HPGauge.value <= 0.0)
        {
            Debug.Log("Game Over!");
        }
        switch (CurrentLevel)
        {
            case GameLevel.Easy:
                //Debug.Log("Easy");
                Current_Music_Beat = Music_Beat;
                break;
            case GameLevel.Normal:
                //Debug.Log("Normal");
                Current_Music_Beat = Music_Beat/2f;
                break;
            case GameLevel.Hard:
                //Debug.Log("Hard");
                Current_Music_Beat = Music_Beat / Random.Range(2, 4);
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
                Sword_Obstacle();
                break;
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
