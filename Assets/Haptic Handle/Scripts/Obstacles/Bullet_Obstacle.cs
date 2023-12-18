using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Obstacle : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private MeshRenderer meshRenderer;
    private MeshRenderer child1MeshRenderer1; // Reference to the Mesh Renderer of Child 1
    private MeshRenderer child1MeshRenderer2; // Reference to the Mesh Renderer of Child 1
    private MeshRenderer child1MeshRenderer3; // Reference to the Mesh Renderer of Child 1
    private MeshRenderer child1MeshRenderer4; // Reference to the Mesh Renderer of Child 1
    private Collider collider;
    private Collider child1Collider;




    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {

        // Get the Mesh Renderer component of the object.
        meshRenderer = GetComponent<MeshRenderer>();

        // Get the Mesh Renderer component of Child 1.
        child1MeshRenderer1 = transform.GetChild(0).GetComponent<MeshRenderer>();
        child1MeshRenderer2 = transform.GetChild(1).GetComponent<MeshRenderer>();
        child1MeshRenderer3 = transform.GetChild(2).GetComponent<MeshRenderer>();
        child1MeshRenderer4 = transform.GetChild(3).GetComponent<MeshRenderer>();

        // Get the Collider component of the object.
        collider = GetComponent<Collider>();

        // Get theCollider  component of Child 1.
        child1Collider = transform.GetChild(1).GetComponent<Collider>();

    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * transform.up * 2;
        if(transform.position.z <0.7)
        {
            
            Destroy(transform.gameObject);
            //Debug.Log("It wasn't destroied");
        }
    }
    public void HitObject()
    {
        // Disable the Mesh Renderer to make the object invisible.
        //meshRenderer.enabled = false;

        // Disable the Mesh Renderer of Child 1 to make it invisible.
        child1MeshRenderer1.enabled = false;
        child1MeshRenderer2.enabled = false;
        child1MeshRenderer3.enabled = false;
        child1MeshRenderer4.enabled = false;

        collider.enabled = false;
        //child1Collider.enabled = false;

        // Activate the particle system.
        particleSystem.Play();
        //Debug.Log(gameObject.transform.name + " is Particle....");

        // Start a coroutine to wait for the particle system to finish.
        StartCoroutine(WaitForParticleCompletion());
    }

    private IEnumerator WaitForParticleCompletion()
    {
        // Wait for the duration of the particle system.
        yield return new WaitForSeconds(particleSystem.main.duration);

        // Destroy the object.
        //Debug.Log(gameObject.transform.name + " is detroied.");
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("From the cubes Collision: " + collision.gameObject.transform.name);
    }
}
