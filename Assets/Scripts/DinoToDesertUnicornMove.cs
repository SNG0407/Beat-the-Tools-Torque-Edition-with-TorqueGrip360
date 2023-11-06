
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DinoToDesertUnicornMove : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    //[SerializeField]
    public Transform target;
    private BNG.SceneLoader loader;
    


    void Start ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.SetDestination (target.position);
		loader = GetComponent<BNG.SceneLoader>();
		
    }

    void Update ()
    {
        
        navMeshAgent.SetDestination (target.position);
		
      
    }

    private void OnTriggerEnter()
    {
	    loader.LoadScene("PathInDesert_Sample");
		//씬이름 바꾸면 같이 바꿔줘야 함 
    }
	
}
