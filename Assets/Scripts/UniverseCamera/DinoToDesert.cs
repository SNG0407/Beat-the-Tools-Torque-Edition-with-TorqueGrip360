using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoToDesert: MonoBehaviour
{

    public Transform unicorn;
    public float dist = 0.04f;
    public float height = 1.0f;
    public float dampTrace = 20.0f;

  
    void LateUpdate(){

        transform.position = Vector3.Lerp(transform.position, unicorn.position + (unicorn.position * dist) + Vector3.up *height,
            Time.deltaTime * dampTrace);
        this.transform.localRotation = Quaternion.Euler(-20, 0, -10);

    }
}
