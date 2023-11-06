using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClick_MainMenu : MonoBehaviour
{
    public GameObject MainMenu;

    public Canvas Main;
    // Start is called before the first frame update
    public void Quit_Clicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    
    
}
