using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBehavior : MonoBehaviour
{
    //public Material markMaterial;
    //private LineRenderer lineRenderer;

    public GameObject markPrefab;
    private Transform markParent;

    private void Start()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.enabled = false; // Hide the LineRenderer initially
        //lineRenderer.material = markMaterial;

        //markParent = new GameObject("Marks").transform; // Create an empty parent for the marks
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Something hit");
        // Check if the collision is from an object that you want to leave a mark
        if (collision.gameObject.tag == "BeatCube")
        {
            ContactPoint contact = collision.contacts[0]; // Assuming the first contact point is what you want
            
            // Create a mark at the contact point
            //GameObject mark = Instantiate(markPrefab, contact.point, Quaternion.identity, this.gameObject.transform);

            // Destroy the mark after one second
            //Destroy(mark, 1f);

            Debug.Log(gameObject.transform.name+" hit : "+collision.gameObject.transform.name);

            // Get the contact point of the collision
            //if (collision.transform.gameObject.GetComponent<Cube>())
            {
                Debug.Log(collision.transform.name + " is hit by the Hammer");
                //collision.transform.gameObject.GetComponent<Cube>().HitObject();
            }
            // Set the positions of the LineRenderer to create a point at the contact point
            //Vector3[] positions = { contact.point, contact.point };
            //lineRenderer.positionCount = 2;
            //lineRenderer.SetPositions(positions);

            //// Show the LineRenderer for a brief moment (e.g., 1 second)
            //lineRenderer.enabled = true;
            //Invoke("HideMark", 10f);

           
        }
    }

    //private void HideMark()
    //{
    //    lineRenderer.enabled = false; // Hide the LineRenderer
    //}
}