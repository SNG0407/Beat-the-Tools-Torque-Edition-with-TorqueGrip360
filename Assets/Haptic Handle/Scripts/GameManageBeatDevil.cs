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

    //Holding Weapons
    public GameObject Sword_R;
    public GameObject Sword_L;
    public GameObject Gun_R;
    public GameObject Gun_L;
    public GameObject Shield;
    public GameObject Hammer;

    //Game UI
    public Slider TorqueGauge;
    public Slider HPGauge;
    public Slider RemainTime;
    public TextMeshProUGUI GameLevel_Text;
    public GameObject BossFigure;
    public GameObject GameOverFigure;
    public GameObject VictoryFigure;
    public GameObject Music;

    //Game Beat
    public float Music_Beat = (60 / 95) * 2; //105
    private float Current_Music_Beat = (60 / 95) * 2;

    private float Total_Timer;
    private float Beat_timer;
    private float Random_timer;

    //Badge system
    public bool Sword_Master = false;
    public bool Gun_Master = false;
    public bool Shield_Master = false;
    public bool Hammer_Master = false;
    public bool Is_Boss_Possible = false;

    //Game state
    private bool IsGameOver = false;
    private bool IsVictory = false;


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
        Over,
        Victory,
        // Add more weapon types as needed
    }

    public enum GameLevel
    {
        Easy,
        Normal,
        Hard,
        Over,
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
        //BadgeCheck();
        WeaponChange();

       
    }
    public void LoadTargetScene(string targetSceneName)
    {
        SceneManager.LoadScene(targetSceneName);
    }


    public void WeaponChange()
    {
        switch (HoldingWeapon)
        {
            case WeaponType.Sword:
                Sword_R.SetActive(true);
                Sword_L.SetActive(true);
                Gun_R.SetActive(false);
                Gun_L.SetActive(false);
                Shield.SetActive(false);
                Hammer.SetActive(false);
                break;
            case WeaponType.Gun:
                Sword_R.SetActive(false);
                Sword_L.SetActive(false);
                Gun_R.SetActive(true);
                Gun_L.SetActive(true);
                Shield.SetActive(false);
                Hammer.SetActive(false);
                break;
            case WeaponType.Shield:
                Sword_R.SetActive(false);
                Sword_L.SetActive(false);
                Gun_R.SetActive(false);
                Gun_L.SetActive(false);
                Shield.SetActive(true);
                Hammer.SetActive(false);
                break;
            case WeaponType.Hammer:
                Sword_R.SetActive(false);
                Sword_L.SetActive(false);
                Gun_R.SetActive(false);
                Gun_L.SetActive(false);
                Shield.SetActive(false);
                Hammer.SetActive(true);
                break;
            default:
                break;
        }
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
        if(HPGauge.value <= 0)
        {
            IsVictory = true;
            Debug.Log("Game Victory!");
            CurrentStage = WeaponType.Victory;
            CurrentLevel = GameLevel.Over;
            float Score = Total_Timer;
            Debug.Log("Your score: " + Score);
            Music.SetActive(false);
        }
        if (RemainTime.value <= 0)
        {
            IsGameOver = true;
            if (!IsVictory)
            {
                Debug.Log("Game Over!");
                CurrentStage = WeaponType.Over;
                CurrentLevel = GameLevel.Over;
                Music.SetActive(false);
            }
        }
        if (!IsVictory && !IsGameOver)
        {
            //Boss Random Obstacles
            if (Random_timer > 4f) //Easy 2개 나올 시간
            {
                int ranInt = Random.Range(0, 4);
                Debug.Log("Random : " + ranInt);
                if (ranInt == 0)
                    CurrentStage = WeaponType.Sword;
                else if (ranInt == 1)
                    CurrentStage = WeaponType.Gun;
                else if (ranInt == 2)
                    CurrentStage = WeaponType.Shield;
                else if (ranInt == 3)
                    CurrentStage = WeaponType.Hammer;
                // Reset the timer
                Random_timer = 0f;
            }
            Random_timer += Time.deltaTime;
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
            case GameLevel.Over:
                GameLevel_Text.text = "Over";
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
                break;
            case WeaponType.Over:
                //Debug.Log("Hard");
                GameLevel_Text.text = "Game Over";
                BossFigure.SetActive(false);
                GameOverFigure.SetActive(true);
                DestroyObjectsWithTag("Blue");
                DestroyObjectsWithTag("BeatCube");
                DestroyObjectsWithTag("Red");
                break;
            case WeaponType.Victory:
                //Debug.Log("Hard");
                GameLevel_Text.text = "Victory!";
                BossFigure.SetActive(false);
                VictoryFigure.SetActive(true);
                DestroyObjectsWithTag("Blue");
                DestroyObjectsWithTag("BeatCube");
                DestroyObjectsWithTag("Red");
                break;
            default:
                
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
