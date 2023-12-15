using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class ShieldBehavior : MonoBehaviour
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

        markParent = new GameObject("Marks").transform; // Create an empty parent for the marks
    }
    private void OnCollisionExit(Collision collision)
    {
        hasCollided = false; // Reset the flag after the hit message has been displayed and hidden
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Something hit");
        // Check if the collision is from an object that you want to leave a mark
        if (!hasCollided && collision.gameObject.CompareTag("Red"))
        {
            if (collision.gameObject.name.Contains("Bullet"))
            {
                ContactPoint contact = collision.contacts[0]; // Assuming the first contact point is what you want

                // Create a mark at the contact point
                GameObject mark = Instantiate(markPrefab, contact.point, Quaternion.identity, this.gameObject.transform);

                // Destroy the mark after one second
                Destroy(mark, 1f);

                Debug.Log(gameObject.transform.name + " hit : " + collision.gameObject.transform.name);

                // Get the contact point of the collision
                //if (collision.transform.gameObject.GetComponent<Bullet_Obstacle>())
                collision.transform.gameObject.GetComponent<Bullet_Obstacle>().HitObject();
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
                // Add your desired logic here based on the collided child
                hasCollided = true;
            }
        }
        if (!hasCollided && collision.gameObject.CompareTag("Blue"))
        {
            if (collision.gameObject.name.Contains("Bullet"))
            {
                ContactPoint contact = collision.contacts[0]; // Assuming the first contact point is what you want

                // Create a mark at the contact point
                GameObject mark = Instantiate(markPrefab, contact.point, Quaternion.identity, this.gameObject.transform);

                // Destroy the mark after one second
                Destroy(mark, 1f);

                Debug.Log(gameObject.transform.name + " hit : " + collision.gameObject.transform.name);

                // Get the contact point of the collision
                //if (collision.transform.gameObject.GetComponent<Bullet_Obstacle>())
                collision.transform.gameObject.GetComponent<Bullet_Obstacle>().HitObject();
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
                // Add your desired logic here based on the collided child
                hasCollided = true;
            }
        }
    }

    //private void HideMark()
    //{
    //    lineRenderer.enabled = false; // Hide the LineRenderer
    //}
}