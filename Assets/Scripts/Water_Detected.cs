using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Detected : MonoBehaviour
{
	[SerializeField] private GameObject tutorialUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Water")
		{

            GameManager.Instance.Pilot = true;
            //Debug.Log(PilotManager.GetComponent<GameManager>().Pilot);
            tutorialUI.SetActive(false);
		}
	}
}
