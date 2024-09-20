using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BNG;
public class BoxRespawn : MonoBehaviour
{
    public GameObject boxPrefab; // 상자 프리팹
    public Transform respawnPoint; // 상자가 재생성될 위치
    public float respawnTime = 1f; // 상자가 재생성되기까지의 시간

    private bool isDestroyed = false;

    // CrateDestroyed 자식 오브젝트들의 초기 위치 저장용 딕셔너리 (이름 기반)
    private Dictionary<string, Vector3> initialPositions = new Dictionary<string, Vector3>();
    private Dictionary<string, Quaternion> initialRotations = new Dictionary<string, Quaternion>();
    // 상자의 자식 오브젝트 찾기
    Transform crateUndestroyed;
    Transform crateDestroyed;

    //Haptic vibration
    private InputBridge input;

    void Start()
    {

        // 상자의 자식 오브젝트 찾기
        crateUndestroyed = boxPrefab.transform.Find("CrateUndestroyed");
        crateDestroyed = boxPrefab.transform.Find("CrateDestroyed");
        // CrateDestroyed 자식 오브젝트들의 초기 위치와 회전 저장
        if (crateDestroyed != null)
        {
            foreach (Transform child in crateDestroyed)
            {
                initialPositions[child.name] = child.localPosition;
                initialRotations[child.name] = child.localRotation;

                //Debug.Log("Init: "+ child.name+": "+ initialPositions[child.name]);
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        // 무기와 충돌 체크 (무기의 태그가 "Weapon"이라 가정)
        if (collision.gameObject.CompareTag("Hammer") && !isDestroyed)
        {
            
            DestroyBox();
        }
    }

    void DestroyBox()
    {
        // 상자 파괴
        isDestroyed = true;

        Debug.Log("Hit");

        
        // 자식 오브젝트 활성화/비활성화 설정
        if (crateUndestroyed != null)
        {
            crateUndestroyed.gameObject.SetActive(false);
        }

        if (crateDestroyed != null)
        {
            crateDestroyed.gameObject.SetActive(true);
        }
        if(boxPrefab.GetComponent<BoxCollider>() != null)
        {
            //boxPrefab.GetComponent<BoxCollider>().enabled = false;
            //Debug.Log("Collider disenabled...");
        }
        // 상자 재생성 코루틴 시작
        StartCoroutine(RespawnBox());
    }

    IEnumerator RespawnBox()
    {
        // respawnTime 만큼 대기
        yield return new WaitForSeconds(respawnTime);


        //Destroy(boxPrefab.gameObject);
        // 새로운 상자 생성
        // 새로운 상자 생성
        //GameObject newBox = Instantiate(boxPrefab, respawnPoint.position, respawnPoint.rotation);
        // 생성된 오브젝트의 이름 변경 (Clone 제거)
        //newBox.name = boxPrefab.name;

        // 상자의 부모로 설정할 오브젝트가 있다면 설정
        //newBox.transform.SetParent(transform.parent);  // 부모 설정 (필요한 경우)

        // 상자의 로컬 위치를 (0, 0, 0)으로 설정
        //newBox.transform.localPosition = Vector3.zero;

        // 상자의 자식 오브젝트 찾기
        //Transform crateUndestroyed = newBox.transform.Find("CrateUndestroyed");
        //Transform crateDestroyed = newBox.transform.Find("CrateDestroyed");

        // 자식 오브젝트 활성화/비활성화 설정
        if (crateUndestroyed != null)
        {
            crateUndestroyed.gameObject.SetActive(true);
        }
        //Transform newCrateDestroyed = newBox.transform.Find("CrateDestroyed");
        if (crateDestroyed != null)
        {

            foreach (Transform child in crateDestroyed)
            {
                if (initialPositions.ContainsKey(child.name))
                {
                    child.localPosition = initialPositions[child.name];
                    child.localRotation = initialRotations[child.name];

                    //Debug.Log("Relo: " + child.name + ": " + child.localPosition);
                }
                else
                {
                    //Debug.Log("No initial data for: " + child.name);
                }
            }
            if (boxPrefab.GetComponent<BoxCollider>() != null)
            {
                boxPrefab.GetComponent<BoxCollider>().enabled = true;
                //Debug.Log("Collider enabled...");
            }
            crateDestroyed.gameObject.SetActive(false);
            //Debug.Log("Relocate");
        }


        // 부셔진 상태 초기화
        isDestroyed = false;
        Debug.Log("Respawn");
    }
}