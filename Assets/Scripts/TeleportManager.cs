using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Samples;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TeleportManager : MonoBehaviour
{
    private BNG.SceneLoader loader;
    // Start is called before the first frame update
    void Awake()
    {
        loader = GetComponent<BNG.SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Player") // && 공룡별 NPC일 경우 넣기
        {
            
            
            Debug.Log("Teleporting");
            
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Earth_plane")
                loader.LoadScene("Dino_Sample");
            
            else if (scene.name == "Dino_Sample")
                loader.LoadScene("PathInDesert_Sample");
            
            else if (scene.name == "PathInDesert_Sample")
                loader.LoadScene("FinalScene");
        }
        
    }
}
