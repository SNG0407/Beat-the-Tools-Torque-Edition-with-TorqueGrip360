using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManageTutorial : MonoBehaviour
{
    public static GameManageTutorial instance;

    public WeaponType CurrentStage;
    public WeaponType HoldingWeapon;
    public GameLevel CurrentLevel;

    //Game Objects
    public GameObject[] Sword_Obstacles;
    public GameObject[] Gun_Obstacles;
    public GameObject[] Shield_Obstacles;
    public GameObject[] Hammer_Obstacles;
    public Transform[] Obstacle_points;

    public GameObject Dummy;
    public GameObject TutorialUI1;
    public GameObject TutorialUI2;

    //Holding Weapons
    public GameObject Sword_R;
    public GameObject Sword_L;
    public GameObject Gun_R;
    public GameObject Gun_L;
    public GameObject Shield;
    public GameObject Hammer;

    //Game UI
    public TextMeshProUGUI GameLevel_Text;

    //Game Beat
    public float Music_Beat = (60 / 95) * 2; //105
    private float Current_Music_Beat = (60 / 95) * 2;

    private float Total_Timer;
    private float Beat_timer;
    private float Random_timer;

    bool IsBossStage = false;

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
        //RemainTime.value = 180;

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
        WeaponChange();

       
    }

    public void Tool_Practice_Btn(string toolName)
    {
        switch (toolName)
        {
            case "Sword":
                CurrentStage = WeaponType.Sword;
                IsBossStage = false;
                Dummy.SetActive(false);
                break;
            case "Gun":
                CurrentStage = WeaponType.Gun;
                IsBossStage = false;
                Dummy.SetActive(false);
                break;
            case "Shield":
                CurrentStage = WeaponType.Shield;
                IsBossStage = false;
                Dummy.SetActive(false);
                break;
            case "Hammer":
                CurrentStage = WeaponType.Hammer;
                IsBossStage = false;
                Dummy.SetActive(false);
                break;
            case "Boss":
                IsBossStage = true;
                Dummy.SetActive(false);
                break;
            case "Dummy":
                CurrentStage = WeaponType.Main;
                IsBossStage = false;
                Dummy.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void Tutorial_UI_Btn(string num)
    {
        if(num == "1")
        {
            if (TutorialUI1.activeSelf)
            {
                TutorialUI1.SetActive(false);
            }
            else
            {
                TutorialUI1.SetActive(true);
            }
        }
        if (num == "2")
        {
            if (TutorialUI2.activeSelf)
            {
                TutorialUI2.SetActive(false);
            }
            else
            {
                TutorialUI2.SetActive(true);
            }
        }
    }
    public void Level_Practice_Btn(string Level)
    {
        switch (Level)
        {
            case "Easy":
                CurrentLevel = GameLevel.Easy;
                break;
            case "Normal":
                CurrentLevel = GameLevel.Normal;
                break;
            case "Hard":
                CurrentLevel = GameLevel.Hard;
                break;
            default:
                break;
        }
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
   
    public void GameOverCheck()
    {
        if (IsBossStage)
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
        //if(HPGauge.value>70.0 && HPGauge.value <= 100.0)
        //{
        //    CurrentLevel = GameLevel.Easy;
        //}else if (HPGauge.value > 40.0 && HPGauge.value <= 70.0)
        //{
        //    CurrentLevel = GameLevel.Normal;
        //}
        //if (HPGauge.value > 0.0 && HPGauge.value <= 40.0)
        //{
        //    CurrentLevel = GameLevel.Hard;
        //}
        //if (HPGauge.value <= 0.0)
        //{
        //    Debug.Log("Game Over!");
        //}
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
                //Current_Music_Beat = Music_Beat / Random.Range(2, 4);

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
            default:
                
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
