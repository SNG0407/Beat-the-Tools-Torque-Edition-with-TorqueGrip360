using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilot_Detect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Light;

    private bool isPilot;
    //public GameObject dialogue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Desert_Dialogue.Instance.isNearPilot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Desert_Dialogue.Instance.isNearPilot = false;
        }
    }
}
