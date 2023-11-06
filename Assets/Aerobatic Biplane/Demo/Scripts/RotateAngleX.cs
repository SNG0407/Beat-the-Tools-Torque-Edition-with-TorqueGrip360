using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAngleX : MonoBehaviour {
    public float MaxTiltAngle = 20.0f;
    public float MinTiltAngle = 20.0f;
    public float tiltSpeed = 30.0f; // tilting speed in degrees/second
    Vector3 curRot;
    float maxX;
    float minX;
    public bool invertMovement;
    
    void Start()
    {
        // Get initial rotation
        curRot = this.transform.localEulerAngles;
        // calculate limit angles:
        maxX = curRot.x + MinTiltAngle;
        minX = curRot.x - MaxTiltAngle;        
    }

    void Update()
    {
        if (invertMovement)
        {
            curRot.x += Input.GetAxis("Horizontal") * Time.deltaTime * tiltSpeed;
        }
        else if (!invertMovement)
        {
            curRot.x -= Input.GetAxis("Horizontal") * Time.deltaTime * tiltSpeed;
        }
        
        // Restrict rotation along x to the limit angles:
        curRot.x = Mathf.Clamp(curRot.x, minX, maxX);
        
        // Set the object rotation
        this.transform.localEulerAngles = curRot;
    }
}
