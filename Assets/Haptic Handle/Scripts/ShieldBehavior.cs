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
    public Transform shieldCenter; // 방패의 중심

    [SerializeField]
    private Transform marker; // 마크의 위치
    private float Force_total = 0;
    private float duraion_total = 0;
    private float Force_RedBullet = 150;
    private float Force_BlueBullet = 255;
    private float duraion_RedBullet = 30;
    private float duraion_BlueBullet = 50;
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
        if (collision.gameObject.CompareTag("Red"))
        {
            if (collision.gameObject.name.Contains("Bullet"))
            {
                ContactPoint contact = collision.contacts[0]; // Assuming the first contact point is what you want

                // Create a mark at the contact point
                GameObject mark = Instantiate(markPrefab, contact.point, Quaternion.identity, this.gameObject.transform);
                Calculate_Distance_Angle(mark, shieldCenter,1);

                // Destroy the mark after one second
                Destroy(mark, 2f);

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
        if (collision.gameObject.CompareTag("Blue"))
        {
            if (collision.gameObject.name.Contains("Bullet"))
            {
                ContactPoint contact = collision.contacts[0]; // Assuming the first contact point is what you want

                // Create a mark at the contact point
                GameObject mark = Instantiate(markPrefab, contact.point, Quaternion.identity, this.gameObject.transform);
                Calculate_Distance_Angle(mark, shieldCenter,2);

                // Destroy the mark after one second
                Destroy(mark, 2f);

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
    public void Calculate_Distance_Angle(GameObject mark, Transform shieldCenter, int BulletColor) //BulletColor: 1=red, 2=blue
    {
        // 방패의 중심과 마크 사이의 벡터
        marker = mark.transform;
        Vector2 direction = marker.position - shieldCenter.position;

        // 월드 스페이스에서 마크의 위치를 방패의 로컬 스페이스로 변환
        Vector2 localPosition = shieldCenter.InverseTransformPoint(marker.position);

        // 거리 계산
        float distance = localPosition.magnitude;

        //거리에 따른 duration에 weight주기
        float normalizedDistance = Mathf.Clamp01(distance / 0.43f);
        if(BulletColor == 1)
        {
            duraion_total = normalizedDistance * duraion_RedBullet;
        } 
        else if (BulletColor == 2)
        {
            duraion_total = normalizedDistance * duraion_BlueBullet;
        }

        // 각도 계산 (라디안 -> 도)
        float angle = Mathf.Atan2(localPosition.y, localPosition.x) * Mathf.Rad2Deg;

        // 각도를 0-360도로 변환
        if (angle < 0)
        {
            angle += 360;
        }

        // 결과 출력
        Debug.Log("Distance: " + distance); //Max d = 0.43
        Debug.Log("Angle: " + angle);
    }

    void CalculateTorqueFeedback(float distance, float angle, int BulletColor)
    {
        //If the distance is bigger, the duration will be longer for more torque.
        //The amount of the force(speed of the motor) depends on the bullet force.
        //Which means bigger and faster bullet makes higher force.

        float distance_duration_scaleFactor;

        //Devide the area into 12 areas (30degree)
        int area = 0;

        if(angle>=0 && angle < 30) //area 1
        {
            if(distance>0.2 && distance < 0.43) //Up side
            {

            }else if (distance > 0.0 && distance <= 0.2) //Down side
            {

            }
        }
        else if(angle >= 30 && angle < 60) //area 2
        {
            if (distance > 0.2 && distance < 0.43) //Up side
            {

            }
            else if (distance > 0.0 && distance <= 0.2) //Down side
            {

            }
        }
        else if (angle >= 60 && angle < 90) //area 3
        {
            if (distance > 0.2 && distance < 0.43) //Up side
            {

            }
            else if (distance > 0.0 && distance <= 0.2) //Down side
            {

            }
        }
        else if (angle >= 90 && angle < 120) //area 4
        {
            if (distance > 0.2 && distance < 0.43) //Up side
            {

            }
            else if (distance > 0.0 && distance <= 0.2) //Down side
            {

            }
        }
        else if (angle >= 120 && angle < 150) //area 5
        {
            if (distance > 0.2 && distance < 0.43) //Up side
            {

            }
            else if (distance > 0.0 && distance <= 0.2) //Down side
            {

            }
        }
        else if (angle >= 120 && angle < 180) //area 6
        {
            if (distance > 0.2 && distance < 0.43) //Up side
            {

            }
            else if (distance > 0.0 && distance <= 0.2) //Down side
            {

            }
        }
        else if (angle >= 180 && angle < 210) //area 7
        {
            if (distance > 0.2 && distance < 0.43) //Up side
            {

            }
            else if (distance > 0.0 && distance <= 0.2) //Down side
            {

            }
        }
        else if (angle >= 210 && angle < 240) //area 8
        {
            if (distance > 0.2 && distance < 0.43) //Up side
            {

            }
            else if (distance > 0.0 && distance <= 0.2) //Down side
            {

            }
        }
        else if (angle >= 240 && angle < 270) //area 9
        {
            if (distance > 0.2 && distance < 0.43) //Up side
            {

            }
            else if (distance > 0.0 && distance <= 0.2) //Down side
            {

            }
        }
        else if (angle >= 270 && angle < 300) //area 10
        {
            if (distance > 0.2 && distance < 0.43) //Up side
            {

            }
            else if (distance > 0.0 && distance <= 0.2) //Down side
            {

            }
        }
        else if (angle >= 300 && angle < 330) //area 11
        {
            if (distance > 0.2 && distance < 0.43) //Up side
            {

            }
            else if (distance > 0.0 && distance <= 0.2) //Down side
            {

            }
        }
        else if (angle >= 330 && angle < 360) //area 12
        {
            if (distance > 0.2 && distance < 0.43) //Up side
            {

            }
            else if (distance > 0.0 && distance <= 0.2) //Down side
            {

            }
        }
    }
    //private void HideMark()
    //{
    //    lineRenderer.enabled = false; // Hide the LineRenderer
    //}
}