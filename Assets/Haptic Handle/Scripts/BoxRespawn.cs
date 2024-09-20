using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BNG;
public class BoxRespawn : MonoBehaviour
{
    public GameObject boxPrefab; // ���� ������
    public Transform respawnPoint; // ���ڰ� ������� ��ġ
    public float respawnTime = 1f; // ���ڰ� ������Ǳ������ �ð�

    private bool isDestroyed = false;

    // CrateDestroyed �ڽ� ������Ʈ���� �ʱ� ��ġ ����� ��ųʸ� (�̸� ���)
    private Dictionary<string, Vector3> initialPositions = new Dictionary<string, Vector3>();
    private Dictionary<string, Quaternion> initialRotations = new Dictionary<string, Quaternion>();
    // ������ �ڽ� ������Ʈ ã��
    Transform crateUndestroyed;
    Transform crateDestroyed;

    //Haptic vibration
    private InputBridge input;

    void Start()
    {

        // ������ �ڽ� ������Ʈ ã��
        crateUndestroyed = boxPrefab.transform.Find("CrateUndestroyed");
        crateDestroyed = boxPrefab.transform.Find("CrateDestroyed");
        // CrateDestroyed �ڽ� ������Ʈ���� �ʱ� ��ġ�� ȸ�� ����
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
        // ����� �浹 üũ (������ �±װ� "Weapon"�̶� ����)
        if (collision.gameObject.CompareTag("Hammer") && !isDestroyed)
        {
            
            DestroyBox();
        }
    }

    void DestroyBox()
    {
        // ���� �ı�
        isDestroyed = true;

        Debug.Log("Hit");

        
        // �ڽ� ������Ʈ Ȱ��ȭ/��Ȱ��ȭ ����
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
        // ���� ����� �ڷ�ƾ ����
        StartCoroutine(RespawnBox());
    }

    IEnumerator RespawnBox()
    {
        // respawnTime ��ŭ ���
        yield return new WaitForSeconds(respawnTime);


        //Destroy(boxPrefab.gameObject);
        // ���ο� ���� ����
        // ���ο� ���� ����
        //GameObject newBox = Instantiate(boxPrefab, respawnPoint.position, respawnPoint.rotation);
        // ������ ������Ʈ�� �̸� ���� (Clone ����)
        //newBox.name = boxPrefab.name;

        // ������ �θ�� ������ ������Ʈ�� �ִٸ� ����
        //newBox.transform.SetParent(transform.parent);  // �θ� ���� (�ʿ��� ���)

        // ������ ���� ��ġ�� (0, 0, 0)���� ����
        //newBox.transform.localPosition = Vector3.zero;

        // ������ �ڽ� ������Ʈ ã��
        //Transform crateUndestroyed = newBox.transform.Find("CrateUndestroyed");
        //Transform crateDestroyed = newBox.transform.Find("CrateDestroyed");

        // �ڽ� ������Ʈ Ȱ��ȭ/��Ȱ��ȭ ����
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


        // �μ��� ���� �ʱ�ȭ
        isDestroyed = false;
        Debug.Log("Respawn");
    }
}