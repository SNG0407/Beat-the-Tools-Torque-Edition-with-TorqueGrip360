using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public static GameManage instance;

    public WeaponType CurrentWeapon;
    public GameLevel CurrentLevel;

    public GameObject[] Sword_Obstacles;
    public GameObject[] Gun_Obstacles;
    public GameObject[] Shield_Obstacles;
    public GameObject[] Hammer_Obstacles;
    public Transform[] Obstacle_points;

    public float Music_Beat = (60 / 95) * 2; //105
    private float Current_Music_Beat = (60 / 95) * 2;

    private float Total_Timer;
    private float Beat_timer;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //CurrentWeapon = WeaponType.Sword;

    }

    // Update is called once per frame
    void Update()
    {
        Obstacle_Play();
        Update_GameLevel();
        Total_Timer += Time.deltaTime;
        //Debug.Log(Total_Timer+ ": 경과");
        
    }


    public void Update_GameLevel()
    {
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
        switch (CurrentWeapon)
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
}

public enum WeaponType
{
    Sword,
    Gun,
    Shield,
    Hammer,
    // Add more weapon types as needed
}

public enum GameLevel
{
    Easy,
    Normal,
    Hard,
    // Add more weapon types as needed
}