using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.sys

public class StarGenerator : MonoBehaviour
{
    public GameObject shootingStarPrefab;
    public Transform parent;
    public float SpawnRate = 0.5f;
    bool DelayTime = false;
    // Start is called before the first frame update
    void Start()
    {
        //   // Instance 생성
  	    // GameObject myInstance = Instantiate(shootingStarPrefab); 
        // // position 위치 지정
        // myInstance.transform.position =  new Vector3(Random.Range(-15, 15), Random.Range(10, 15), Random.Range(15, 30));
        // // Component에 접근 
        // //myInstance.GetComponent<DestoryableObject>().MoveStars();
        // // 오브젝트를 월드상에 보이도록 설정
        // myInstance.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
         //1. 랜덤 시간마다 랜덤 위치로 별 생성하기

        //2. 생성된 별 떨어지기
        if(DelayTime==false){
            DelayTime = true;
            //Debug.Log("별똥별 생성");
            StartCoroutine(StarGenerate());
        }

        
    }

    IEnumerator StarGenerate(){
        yield return new WaitForSecondsRealtime(SpawnRate);

        GameObject myInstance = Instantiate(shootingStarPrefab); 
        // position 위치 지정
        myInstance.transform.position =  new Vector3(Random.Range(15, 30), Random.Range(8, 30), Random.Range(3, 18));
        float SettingNum = Random.Range(1, 10);
        myInstance.transform.localScale += new Vector3(SettingNum*5, SettingNum*5, SettingNum*5);
        //myInstance.transform.Find("WFX_4_StarDestroy_Explosion Small").gameObject.SetActive(false);
        //myInstance.transform.Find("WFX_6_StarDestroy_ExplosiveSmoke").gameObject.SetActive(false);

        //Debug.Log("setting : "+SettingNum+", 별똥별 scale : "+myInstance.transform.localScale);
        myInstance.GetComponent<Rigidbody>().drag=SettingNum*0.2f;
        myInstance.GetComponent<Rigidbody>().mass=3;
        // Component에 접근 
        //myInstance.GetComponent<DestoryableObject>().MoveStars();
        // 오브젝트를 월드상에 보이도록 설정
        myInstance.SetActive(true);
        DelayTime = false;

    }
}
