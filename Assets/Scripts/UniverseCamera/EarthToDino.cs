using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthToDino : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform unicorn;
    public float dist = -0.1f;
    public float height = 2.0f;
    public float dampTrace = 20.0f;

    //private GameObject unicorn;
    //public Vector3 offset;

    // void Awake()
    // {
    //     unicorn = GameObject.Find("LittleUnicorn");
    // }
    void LateUpdate(){
        //Vector3 camera_pos = unicorn.transform.position + offset;
        //transform.position = Vector3.Lerp(transform.position, camera_pos, 0.1f);
        transform.position = Vector3.Lerp(transform.position, unicorn.position + (unicorn.position * dist) + Vector3.up *height,
            Time.deltaTime * dampTrace);
        this.transform.localRotation = Quaternion.Euler(-20, 0, 0);
        
        //transform.LookAt(unicorn);
    }
}
