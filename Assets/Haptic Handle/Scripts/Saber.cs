using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class Saber : MonoBehaviour
{
    public LayerMask layer;
    private Vector3 previousPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1, layer))
        { 
            if(Vector3.Angle(transform.position - previousPos, hit.transform.up) > 130)
            {
                if (hit.transform.gameObject.GetComponent<Cube>())
                {
                    //Debug.Log(hit.transform.name + " is hit.");
                    Debug.Log(layer.value+ " is hit. Layer");
                    hit.transform.gameObject.GetComponent<Cube>().HitObject();
                    if(layer.value== 512)
                    {
                        if (SceneManager.GetActiveScene().name == "BeatDevil")
                        {
                            GameManageBeatDevil.instance.HPGauge.value = GameManageBeatDevil.instance.HPGauge.value - 5;
                            StartCoroutine(GameManageBeatDevil.instance.ShowHitMessage("[Blue] Perfect hit!", Color.blue));
                        }
                        else
                        {
                            GameManage.instance.TorqueGauge.value++;
                            StartCoroutine(GameManage.instance.ShowHitMessage("[Blue] Perfect hit!", Color.blue));
                        }
                    }
                    else if (layer.value == 1024)
                    {
                        if (SceneManager.GetActiveScene().name == "BeatDevil")
                        {
                            GameManageBeatDevil.instance.HPGauge.value = GameManageBeatDevil.instance.HPGauge.value - 5;
                            StartCoroutine(GameManageBeatDevil.instance.ShowHitMessage("[Red] Perfect hit!", Color.red));
                        }
                        else
                        {
                            GameManage.instance.TorqueGauge.value++;
                            StartCoroutine(GameManage.instance.ShowHitMessage("[Red] Perfect hit!", Color.red));
                        }
                    }
                }

                //Destroy(hit.transform.gameObject);
            }
        }
        previousPos = transform.position;


    }
}
