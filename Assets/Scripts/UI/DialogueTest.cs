using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTest : MonoBehaviour
{
    [SerializeField]
    private DialogueSystem dialogSystem;

    [SerializeField] private GameObject starGen;
    [SerializeField] private DialogueSystem dialogSystem2;

    private IEnumerator Start()
    {

        yield return new WaitUntil(()=>dialogSystem.UpdateDialog());
        
        int count = 5;
        while (count > 0)
        {
            
            count--;

            yield return new WaitForSeconds(1);
            
            starGen.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
