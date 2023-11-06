using UnityEngine;
using System.Collections;

public class cameraswitch : MonoBehaviour {

    public Camera[] cams;

    public void camMainMove(){
        cams[0].enabled = true;
        cams[1].enabled = false;
   
    }
    public void camOneMove(){
        cams[0].enabled = false;
        cams[1].enabled = true;
  
    }
}
