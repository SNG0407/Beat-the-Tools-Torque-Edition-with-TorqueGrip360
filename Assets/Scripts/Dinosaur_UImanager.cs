using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Dinosaur_UImanager : MonoBehaviour
{
    private static Dinosaur_UImanager _instance;
    public static Dinosaur_UImanager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Dinosaur_UImanager>();
            if (_instance == null)
            {
                GameObject container = new GameObject("UImanager");
                _instance = container.AddComponent<Dinosaur_UImanager>();
            }
            return _instance;
        }
    }

    //[SerializeField] TextMeshProUGUI Text_HitCount,Text_MissCount;
    public Text Text_HitCount,Text_MissCount,Text_Mission;
    public int MissionSuccessCount;
    public int MissionFailureCount;
    int HitCount = 0;
    int MissCount = 0;
    public bool Successed = false;
    public bool Failed = false;

    public Slider slider;



    // Start is called before the first frame update
    void Start()
    {
         slider.value =0;
        Text_HitCount.text = HitCount.ToString();
        //Text_MissCount.text = MissCount.ToString();
    }

    public void HitFunction()
    {
        HitCount++;
        Text_HitCount.text = HitCount.ToString();
        Debug.Log("운석 파괴 성공:"+HitCount);

        if(HitCount == MissionSuccessCount && Text_Mission.text == "미션 스타트")
        {
            Text_Mission.text = "미션 성공";
            Successed = true;
        }
    }
    public void MissFunction()
    {
        MissCount++;
        slider.value = MissCount;
        //Text_MissCount.text = MissCount.ToString();
        //Debug.Log("운석 파괴 실패:"+MissCount);
        if(MissCount == MissionFailureCount && Text_Mission.text == "미션 스타트")
        {
            Text_Mission.text = "미션 실패";
            Failed = true;
        }
    }



}
