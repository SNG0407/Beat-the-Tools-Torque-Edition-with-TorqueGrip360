using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HammerBehavior : MonoBehaviour
{
    //public Material markMaterial;
    //private LineRenderer lineRenderer;

    public GameObject markPrefab;
    private Transform markParent;
    private bool hasCollided = false;
    private void Start()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.enabled = false; // Hide the LineRenderer initially
        //lineRenderer.material = markMaterial;

        //markParent = new GameObject("Marks").transform; // Create an empty parent for the marks
    }
    private void OnCollisionExit(Collision collision)
    {
        hasCollided = false; // Reset the flag after the hit message has been displayed and hidden
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Something hit");
        // Check if the collision is from an object that you want to leave a mark
        //Debug.Log(gameObject.transform.name + " hit : " + collision.gameObject.transform.name);

        // Check which collider of the sword hit the object
        foreach (ContactPoint contact in collision.contacts)
        {
            if (!hasCollided && contact.thisCollider.CompareTag("Red"))
            {
                if (collision.gameObject.name.Contains("Crate_Red"))
                {
                    if (SceneManager.GetActiveScene().name == "BeatDevil")
                    {
                        GameManageBeatDevil.instance.HPGauge.value = GameManageBeatDevil.instance.HPGauge.value - 5;
                        StartCoroutine(GameManageBeatDevil.instance.ShowHitMessage("[Red] Perfect hit!", Color.red));
                    }
                    else if (SceneManager.GetActiveScene().name == "Tutorial")
                    {
                        StartCoroutine(GameManageTutorial.instance.ShowHitMessage("[Red] Perfect hit!", Color.red));
                    }
                    else
                    {
                        GameManage.instance.TorqueGauge.value++;
                        StartCoroutine(GameManage.instance.ShowHitMessage("[Red] Perfect hit!", Color.red));
                    }
                    Debug.Log($"child: {contact.thisCollider.transform.name}, " + $"Beat Obj: {collision.gameObject.transform.name}");
                    // Add your desired logic here based on the collided child
                    hasCollided = true;
                }
            }
            else if (!hasCollided && contact.thisCollider.CompareTag("Blue"))
            {
                if (contact.thisCollider.CompareTag("Blue"))
                {
                    if (collision.gameObject.name.Contains("Crate_Blue"))
                    {
                        if (SceneManager.GetActiveScene().name == "BeatDevil")
                        {
                            GameManageBeatDevil.instance.HPGauge.value = GameManageBeatDevil.instance.HPGauge.value - 5;
                            StartCoroutine(GameManageBeatDevil.instance.ShowHitMessage("[Blue] Perfect hit!", Color.blue));
                        }
                        else if (SceneManager.GetActiveScene().name == "Tutorial")
                        {
                            StartCoroutine(GameManageTutorial.instance.ShowHitMessage("[Blue] Perfect hit!", Color.blue));
                        }
                        else
                        {
                            GameManage.instance.TorqueGauge.value++;
                            StartCoroutine(GameManage.instance.ShowHitMessage("[Blue] Perfect hit!", Color.blue));
                        }
                        Debug.Log($"child: {contact.thisCollider.transform.name}, " + $"Beat Obj: {collision.gameObject.transform.name}");
                        // Add your desired logic here based on the collided child
                        hasCollided = true;
                    }
                }
            }
        }
        //if (collision.gameObject.name.Contains("Crate_Blue") || collision.gameObject.name.Contains("Crate_Red"))
        //{
        //    ContactPoint contact = collision.contacts[0]; // Assuming the first contact point is what you want

        //    // Create a mark at the contact point
        //    //GameObject mark = Instantiate(markPrefab, contact.point, Quaternion.identity, this.gameObject.transform);

        //    // Destroy the mark after one second
        //    //Destroy(mark, 1f);
        //    // Check which child collided

        //    foreach (Transform child in transform)
        //    {
        //        if (collision.gameObject.name.Contains("Crate_Blue") && child.gameObject.name.Contains("Blue"))
        //        {
        //            Debug.Log($"child: {child.name}, "+ $"Beat Obj: {collision.gameObject.transform.name}");
        //            // Add your desired logic here based on the collided child
        //            StartCoroutine(GameManageBeatDevil.instance.ShowHitMessage());
        //        }
        //    }
            //Debug.Log(gameObject.transform.name+" hit : "+collision.gameObject.transform.name);

            // Get the contact point of the collision
            //if (collision.transform.gameObject.GetComponent<Cube>())
            //{
            //    Debug.Log(collision.transform.name + " is hit by the Hammer");
            //    //collision.transform.gameObject.GetComponent<Cube>().HitObject();
            //}
            // Set the positions of the LineRenderer to create a point at the contact point
            //Vector3[] positions = { contact.point, contact.point };
            //lineRenderer.positionCount = 2;
            //lineRenderer.SetPositions(positions);

            //// Show the LineRenderer for a brief moment (e.g., 1 second)
            //lineRenderer.enabled = true;
            //Invoke("HideMark", 10f);
        //}
    }

    //private void HideMark()
    //{
    //    lineRenderer.enabled = false; // Hide the LineRenderer
    //}
}