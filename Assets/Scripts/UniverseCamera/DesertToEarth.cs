using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertToEarth: MonoBehaviour
{

    public Transform airplane;
    public float dist = -0.1f;
    public float height = 2.0f;
    public float dampTrace = 20.0f;

  
    void LateUpdate(){

        transform.position = Vector3.Lerp(transform.position, airplane.position + (airplane.position * dist) + Vector3.up *height,
            Time.deltaTime * dampTrace);
        this.transform.localRotation = Quaternion.Euler(-20, 0, 0);

    }
}
