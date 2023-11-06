using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EarthToDinoUnicornMove : MonoBehaviour
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

    private float speed = 0.5f;
    void Update ()
    {
        
        navMeshAgent.SetDestination (target.position);
        float fMove = Time.deltaTime * speed;
        transform.Translate(Vector3.forward * fMove);
		
      
    }

    private void OnTriggerEnter()
    {
	    loader.LoadScene("Dino_Sample");
    }
	
}
