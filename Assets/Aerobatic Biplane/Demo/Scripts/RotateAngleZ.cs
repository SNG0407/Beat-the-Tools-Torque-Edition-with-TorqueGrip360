using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAngleZ : MonoBehaviour
{
    public float MaxTiltAngle = 20.0f;
    public float MinTiltAngle = 20.0f;
    public float tiltSpeed = 30.0f; // tilting speed in degrees/second
    Vector3 curRot;
    float maxZ;
    float minZ;
    public bool invertMovement;

    void Start()
    {
        // Get initial rotation
        curRot = this.transform.localEulerAngles;
        // calculate limit angles:
        maxZ = curRot.z + MinTiltAngle;
        minZ = curRot.z - MaxTiltAngle;
    }

    void Update()
    {
        if (invertMovement)
        {
            curRot.z += Input.GetAxis("Horizontal") * Time.deltaTime * tiltSpeed;
        }
        else if (!invertMovement)
        {
            curRot.z -= Input.GetAxis("Horizontal") * Time.deltaTime * tiltSpeed;
        }

        // Restrict rotation along x to the limit angles:
        curRot.z = Mathf.Clamp(curRot.z, minZ, maxZ);

        // Set the object rotation
        this.transform.localEulerAngles = curRot;
    }
}
