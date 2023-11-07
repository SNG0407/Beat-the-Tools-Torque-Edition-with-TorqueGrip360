using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {

    

    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * transform.forward * 2;
        if(transform.position.z <0.7)
        {
            
            Destroy(transform.gameObject);
            //Debug.Log("It wasn't destroied");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("From the cubes Collision: " + collision.gameObject.transform.name);
    }
}
