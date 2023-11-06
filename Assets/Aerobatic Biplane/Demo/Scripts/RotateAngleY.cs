using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAngleY : MonoBehaviour {
    public float MaxTiltAngle = 20.0f;
    public float MinTiltAngle = 20.0f;
    public float tiltSpeed = 30.0f; // tilting speed in degrees/second
    Vector3 curRot;
    float maxY;
    float minY;
    public bool invertMovement;

    void Start()
    {
        // Get initial rotation
        curRot = this.transform.localEulerAngles;
        // calculate limit angles:
        maxY = curRot.y + MinTiltAngle;
        minY = curRot.y - MaxTiltAngle;
    }

    void Update()
    {
        if (invertMovement)
        {
            curRot.y += Input.GetAxis("Horizontal") * Time.deltaTime * tiltSpeed;
        }
        else if (!invertMovement)
        {
            curRot.y -= Input.GetAxis("Horizontal") * Time.deltaTime * tiltSpeed;
        }

        // Restrict rotation along x to the limit angles:
        curRot.y = Mathf.Clamp(curRot.y, minY, maxY);

        // Set the object rotation
        this.transform.localEulerAngles = curRot;
    }
}
