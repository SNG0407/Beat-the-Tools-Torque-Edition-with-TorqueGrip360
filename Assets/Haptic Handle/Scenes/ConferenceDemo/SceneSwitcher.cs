using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) // If "O" is pressed
        {
            SceneManager.LoadScene("Gun Demo"); // Replace "GunDemo" with your gun demo scene name
        }
        else if (Input.GetKeyDown(KeyCode.P)) // If "P" is pressed
        {
            SceneManager.LoadScene("Shield Demo"); // Replace "ShieldDemo" with your shield demo scene name
        }
    }
}
