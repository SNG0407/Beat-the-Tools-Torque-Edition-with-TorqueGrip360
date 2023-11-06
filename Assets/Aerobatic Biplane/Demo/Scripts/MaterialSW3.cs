using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSW3 : MonoBehaviour {
    public Material[] mats;
    Renderer rend;
     

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        
                
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Slot1 ()
    {
        rend.sharedMaterial = mats[0];
    }
    public void Slot2()
    {
        rend.sharedMaterial = mats[1];
    }
    public void Slot3()
    {
        rend.sharedMaterial = mats[2];
    }
    public void Slot4()
    {
        rend.sharedMaterial = mats[3];
    }
    public void Slot5()
    {
        rend.sharedMaterial = mats[4];
    }
    public void Slot6()
    {
        rend.sharedMaterial = mats[5];
    }
    public void Slot7()
    {
        rend.sharedMaterial = mats[6];
    }
}
