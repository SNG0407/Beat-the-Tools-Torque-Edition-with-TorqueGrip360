using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

using UnityEngine.SceneManagement;
public class ShieldBehavior : MonoBehaviour
{
    //public Material markMaterial;
    //private LineRenderer lineRenderer;

    public GameObject markPrefab;
    public GameObject SendTorqueData;
    private SendTorqueFeedback send_T_D;
    private Transform markParent;
    private bool hasCollided = false;
    public Transform shieldCenter; // ������ �߽�

    
    private float Force_total = 0;
    private float duraion_total = 0;

    private int PWM_Motor_A=255;
    private int PWM_Motor_B=255;
    private int PWM_Motor_C=255;

    private float Force_RedBullet = 255;
    private float Force_BlueBullet = 255;

    private float duraion_RedBullet = 30;
    private float duraion_BlueBullet = 50;

    // 90��, 210��, 330�� ������ ���� ����
    private Vector3 direction_A = new Vector3(-Mathf.Sqrt(3) / 2, 0, -0.5f); // 210�� ����
    private Vector3 direction_B = new Vector3(Mathf.Sqrt(3) / 2, 0, -0.5f); // 330�� ����
    private Vector3 direction_C = new Vector3(0, 0, 1); // 90�� ����

    private Vector3 direction_X1;
    private Vector3 direction_X2;
    private Vector3 direction_Y1;
    private Vector3 direction_Y2;

    [SerializeField]
    private Transform marker; // ��ũ�� ��ġ
    private float weight_A;
    private float weight_B;
    private float weight_C;


    [Header("Events")]

    [Tooltip("Unity Event called when Shoot() method is successfully called")]
    public UnityEvent<string> onBlockEvent;


    private void Start()
    {
        if(SendTorqueData != null)
        {
            send_T_D = SendTorqueData.GetComponent<SendTorqueFeedback>();
        }
        //lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.enabled = false; // Hide the LineRenderer initially
        //lineRenderer.material = markMaterial;

        markParent = new GameObject("Marks").transform; // Create an empty parent for the marks

        //�־��� 3 ������ ���� 4���� �������� ġȯ
        direction_X1 = 2 / Mathf.Sqrt(3) * direction_B + 1 / Mathf.Sqrt(3) * direction_C;
        direction_X2 = 2 / Mathf.Sqrt(3) * direction_A + 1 / Mathf.Sqrt(3) * direction_C;
        direction_Y1 = direction_C;
        direction_Y2 = direction_A + direction_B;

        Debug.Log("direction_A: "+ direction_A+", "+ direction_A.magnitude);
        Debug.Log("direction_B: " + direction_B + ", " + direction_B.magnitude);
        Debug.Log("direction_C: " + direction_C + ", " + direction_C.magnitude);
        Debug.Log("direction_X1: " + direction_X1 + ", " + direction_X1.magnitude);
        Debug.Log("direction_X2: " + direction_X2 + ", " + direction_X2.magnitude);
        Debug.Log("direction_Y1: " + direction_Y1 + ", " + direction_Y1.magnitude);
        Debug.Log("direction_Y2: " + direction_Y2 + ", " + direction_Y2.magnitude);

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
        // ������ �߽ɰ� ��ũ ������ ����
        marker = mark.transform;
        Vector2 direction = marker.position - shieldCenter.position;

        // ���� �����̽����� ��ũ�� ��ġ�� ������ ���� �����̽��� ��ȯ
        Vector2 localPosition = shieldCenter.InverseTransformPoint(marker.position);

        // �Ÿ� ���
        float distance = localPosition.magnitude;

        // ���� ��� (���� -> ��)
        float angle = Mathf.Atan2(localPosition.y, localPosition.x) * Mathf.Rad2Deg;

        // ������ 0-360���� ��ȯ
        if (angle < 0)
        {
            angle += 360;
        }

        // ��� ���
        Debug.Log("Distance: " + distance); //Max d = 0.43
        Debug.Log("Angle: " + angle);

        CalculateTorqueFeedback(distance, angle, BulletColor);
    }

    void CalculateTorqueFeedback(float distance, float angle, int BulletColor)
    {
        //If the distance is bigger, the duration will be longer for more torque.
        //The amount of the force(speed of the motor) depends on the bullet force.
        //Which means bigger and faster bullet makes higher force.

        //�Ÿ��� ���� duration�� weight�ֱ�
        float normalizedDistance = Mathf.Clamp01(distance / 0.43f);
        if (BulletColor == 1)
        {
            duraion_total = normalizedDistance * duraion_RedBullet;
            Force_total = Force_RedBullet;
        }
        else if (BulletColor == 2)
        {
            duraion_total = normalizedDistance * duraion_BlueBullet;
            Force_total = Force_BlueBullet;
        }



        // ������ �������� ��ȯ
        //float angleRad = angle * Mathf.Deg2Rad;

        // ��ǥ ���� ���� ���
        //Vector3 targetDirection = new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
        //targetDirection = new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));


        //float distance_duration_scaleFactor;

        //Devide the area into 12 areas (30degree)
        int area = 0;

        if(angle>=0 && angle < 90) //1
        {
            float DesiredAngleRad = (angle)* Mathf.Deg2Rad;
            weight_A = 0;
            weight_B = Mathf.Cos(DesiredAngleRad)* 1 / (Mathf.Cos(30* Mathf.Deg2Rad));
            weight_C = Mathf.Cos(DesiredAngleRad) * 1 / (2 * Mathf.Cos(30 * Mathf.Deg2Rad))+ Mathf.Sin(DesiredAngleRad);
        }
        else if(angle >= 90 && angle < 180) //2
        {
            float DesiredAngleRad = (180-angle) * Mathf.Deg2Rad;
            weight_A = Mathf.Cos(DesiredAngleRad) * 1 / (Mathf.Cos(30 * Mathf.Deg2Rad));
            weight_B = 0;
            weight_C = Mathf.Cos(DesiredAngleRad) * 1 / (2 * Mathf.Cos(30 * Mathf.Deg2Rad)) + Mathf.Sin(DesiredAngleRad);
        }
        else if (angle >= 180 && angle <270) //3
        {
            float DesiredAngleRad = (angle-180) * Mathf.Deg2Rad;
            weight_A = Mathf.Cos(DesiredAngleRad) * 1 / (Mathf.Cos(30 * Mathf.Deg2Rad)) + Mathf.Sin(DesiredAngleRad);
            weight_B = Mathf.Sin(DesiredAngleRad);
            weight_C = Mathf.Cos(DesiredAngleRad) * 1 / (2 * Mathf.Cos(30 * Mathf.Deg2Rad));
        }
        else if (angle >= 270 && angle < 360) //4
        {
            float DesiredAngleRad = (360-angle) * Mathf.Deg2Rad;
            weight_A = Mathf.Sin(DesiredAngleRad);
            weight_B = Mathf.Cos(DesiredAngleRad) * 1 / (Mathf.Cos(30 * Mathf.Deg2Rad))+ Mathf.Sin(DesiredAngleRad);
            weight_C = Mathf.Cos(DesiredAngleRad) * 1 / (2 * Mathf.Cos(30 * Mathf.Deg2Rad));
        }
        Debug.Log("Weight: " + weight_A + ", " + weight_B + ", " + weight_C);

        PWM_Motor_A = (int)(Force_total * weight_A) > 255 ? 255 : (int)(Force_total * weight_A);
        PWM_Motor_B = (int)(Force_total * weight_B) > 255 ? 255 : (int)(Force_total * weight_B);
        PWM_Motor_C = (int)(Force_total * weight_C) > 255 ? 255 : (int)(Force_total * weight_C);
        Debug.Log("PWM_Motor: " + PWM_Motor_A + ", " + PWM_Motor_B + ", " + PWM_Motor_C);
        int dur = (int)duraion_total;
        string TorqueData = "A";
        TorqueData += PWM_Motor_A.ToString();
        TorqueData += "#";
        TorqueData += PWM_Motor_B.ToString();
        TorqueData += "#";
        TorqueData += PWM_Motor_C.ToString();
        TorqueData += "#";
        TorqueData += dur.ToString();
        send_T_D.SendShootData(TorqueData);
        Debug.Log("TorqueData: " + TorqueData);

    }
    //private void HideMark()
    //{
    //    lineRenderer.enabled = false; // Hide the LineRenderer
    //}
}