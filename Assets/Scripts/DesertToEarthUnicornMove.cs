
using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DesertToEarthUnicornMove : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    private BNG.SceneLoader loader;
    //[SerializeField]
    public Transform target;

    void Start ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.SetDestination (target.position);
        loader = GetComponent<SceneLoader>();
    }

    void Update ()
    {
        navMeshAgent.SetDestination (target.position); 
    }

    private void OnTriggerEnter()
    {
        Debug.Log("Collide");
        loader.LoadScene("FinalScene");
    }
}
