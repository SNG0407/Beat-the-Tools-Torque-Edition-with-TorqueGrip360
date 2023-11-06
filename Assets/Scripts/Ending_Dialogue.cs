using System.Collections;
using System.Collections.Generic;
using BNG;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_Dialogue : MonoBehaviour
{
    [SerializeField] private DialogueSystem[] dialogs;
    [SerializeField] private SnakeMove Snake;
    [SerializeField] private FoxMove Fox;
    [SerializeField] private GameObject RewardUI;
    [SerializeField] private GameObject light;


    private static Ending_Dialogue _instance;
    public bool isNearPilot;
    public bool bottle;

    public static Ending_Dialogue Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Ending_Dialogue>();
            if (_instance == null)
            {
                GameObject container = new GameObject("DialogManager");
                _instance = container.AddComponent<Ending_Dialogue>();
            }

            return _instance;
        }
    }

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => dialogs[0].UpdateDialog());

        
    }
}