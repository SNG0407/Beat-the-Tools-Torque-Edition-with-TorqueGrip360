using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCollCheck : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        Desert_Dialogue.Instance.bottle = true;
    }

    private void OnCollisionExit(Collision other)
    {
        Desert_Dialogue.Instance.bottle = false;
    }
}
