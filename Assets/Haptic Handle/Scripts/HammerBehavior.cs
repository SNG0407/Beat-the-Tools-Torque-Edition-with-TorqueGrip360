using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class HammerBehavior : MonoBehaviour
{
    //public Material markMaterial;
    //private LineRenderer lineRenderer;

    public GameObject markPrefab;
    private Transform markParent;
    private bool hasCollided = false;

    public GameObject IdleBox;
    public GameObject Hammer_Center;

    public Vector3 previous_Transform_Hammer;
    public Vector3 current_Transform_Hammer;
    public Vector3 velocity_Hammer;

    public Vector3 dir_Hammer_Box; //Torque direction = -1 * dir_Hammer_Box;
    Vector3 hammerNormal;// = Hammer_Center.transform.up;  // 평면의 법선 벡터
    Vector3 projectedDirection; // 공격 방향을 해머의 평평한 면에 프로젝션

    public GameObject SendTorqueData;
    private SendTorqueFeedback send_T_D;


    private float Force_total = 0;
    private float duraion_total = 0;

    private int PWM_Motor_A = 255;
    private int PWM_Motor_B = 255;
    private int PWM_Motor_C = 255;


    // 90도, 210도, 330도 방향의 단위 벡터
    private Vector3 direction_A = new Vector3(-Mathf.Sqrt(3) / 2, 0, -0.5f); // 210도 방향
    private Vector3 direction_B = new Vector3(Mathf.Sqrt(3) / 2, 0, -0.5f); // 330도 방향
    private Vector3 direction_C = new Vector3(0, 0, 1); // 90도 방향

    private Vector3 direction_X1;
    private Vector3 direction_X2;
    private Vector3 direction_Y1;
    private Vector3 direction_Y2;

    [SerializeField]
    private Transform marker; // 마크의 위치
    private float weight_A;
    private float weight_B;
    private float weight_C;
    private void Start()
    {
        if (SendTorqueData != null)
        {
            send_T_D = SendTorqueData.GetComponent<SendTorqueFeedback>();
        }
        //lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.enabled = false; // Hide the LineRenderer initially
        //lineRenderer.material = markMaterial;

        //markParent = new GameObject("Marks").transform; // Create an empty parent for the marks
        if(Hammer_Center != null)
        {
            previous_Transform_Hammer = Hammer_Center.transform.position;
            hammerNormal= Hammer_Center.transform.up;
        }

        //주어진 3 방향을 쉬운 4가지 방향으로 치환
        direction_X1 = 2 / Mathf.Sqrt(3) * direction_B + 1 / Mathf.Sqrt(3) * direction_C;
        direction_X2 = 2 / Mathf.Sqrt(3) * direction_A + 1 / Mathf.Sqrt(3) * direction_C;
        direction_Y1 = direction_C;
        direction_Y2 = direction_A + direction_B;

        Debug.Log("direction_A: " + direction_A + ", " + direction_A.magnitude);
        Debug.Log("direction_B: " + direction_B + ", " + direction_B.magnitude);
        Debug.Log("direction_C: " + direction_C + ", " + direction_C.magnitude);
        Debug.Log("direction_X1: " + direction_X1 + ", " + direction_X1.magnitude);
        Debug.Log("direction_X2: " + direction_X2 + ", " + direction_X2.magnitude);
        Debug.Log("direction_Y1: " + direction_Y1 + ", " + direction_Y1.magnitude);
        Debug.Log("direction_Y2: " + direction_Y2 + ", " + direction_Y2.magnitude);
    }

    private void Update()
    {
        //Calculate the velocity of the hammer.
        current_Transform_Hammer = Hammer_Center.transform.position;
        velocity_Hammer = current_Transform_Hammer - previous_Transform_Hammer;
        //Debug.Log("V_Hammer: " + velocity_Hammer);
        //Debug.Log("V_Hammer: " + velocity_Hammer.x.ToString("F4")+", " + velocity_Hammer.y.ToString("F4") + ", " + velocity_Hammer.z.ToString("F4"));
        //Debug.Log("S_Hammer: " + velocity_Hammer.magnitude.ToString("F4"));
        if (IdleBox != null)
        {
            dir_Hammer_Box = IdleBox.transform.position - current_Transform_Hammer;
            //Debug.Log("dir_Hammer_Box: " + dir_Hammer_Box);
            //Debug.Log("dir_Hammer_Box: " + dir_Hammer_Box.magnitude.ToString("F4"));
        }
        if (Hammer_Center != null)
        {
            previous_Transform_Hammer = current_Transform_Hammer;
            hammerNormal = Hammer_Center.transform.up;
        }

        //Debug.Log("Local X-Axis: " + Hammer_Center.transform.right);   // 로컬 X 축
        //Debug.Log("Local Y-Axis: " + Hammer_Center.transform.up);      // 로컬 Y 축 (평면의 법선 벡터)
        //Debug.Log("Local Z-Axis: " + Hammer_Center.transform.forward); // 로컬 Z 축 (평면에서 0도가 될 방향)

    }
    private void OnDrawGizmos()
    {
        // 공격 방향 Gizmos로 화살표 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Hammer_Center.transform.position, IdleBox.transform.position);
        //Gizmos.DrawRay(Hammer_Center.transform.position, dir_Hammer_Box);

        // 월드 스페이스에서 마크의 위치를 방패의 로컬 스페이스로 변환
        //var localPosition = Hammer_Center.transform.InverseTransformPoint(dir_Hammer_Box);

        // 공격 역 방향 Gizmos로 화살표 그리기
        Gizmos.color = Color.blue;
        //Gizmos.DrawLine(Hammer_Center.transform.position, IdleBox.transform.position-Hammer_Center.transform.position);
        Gizmos.DrawRay(Hammer_Center.transform.position, Hammer_Center.transform.position - 1* IdleBox.transform.position);


        // 토크 방향 Gizmos로 화살표 그리기
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Hammer_Center.transform.position, -1*(projectedDirection));
        //Gizmos.DrawRay(Hammer_Center.transform.position, Hammer_Center.transform.position - projectedDirection);

    }
    void Calculate_Force_Angle(Vector3 dir_Hammer_Box)
    {
        Debug.Log("dir_Hammer_Box: " + dir_Hammer_Box);
        Debug.Log("dir_Hammer_Box: " + dir_Hammer_Box.magnitude.ToString("F4"));

        // 적용되는 힘 (예시로 프로젝션된 방향을 힘으로 가정)
        projectedDirection =  dir_Hammer_Box - Vector3.Dot(dir_Hammer_Box, hammerNormal) * hammerNormal;
        Debug.Log("projectedDirection: " + projectedDirection);
        Debug.Log("projectedDirection: " + projectedDirection.magnitude.ToString("F4"));

        // 월드 스페이스에서 마크의 위치를 방패의 로컬 스페이스로 변환
        var localDirection = Hammer_Center.transform.InverseTransformDirection(-1*(projectedDirection));
        Debug.Log("localDirection: " + localDirection);
        
        // x-z 평면을 기준으로 +z 방향을 0도로 설정하여 각도 계산
        float angle = Mathf.Atan2(localDirection.x, localDirection.z) * Mathf.Rad2Deg;

        // 각도를 0-360도로 변환
        if (angle < 0)
        {
            angle += 360;
        }

        // 결과 출력
        Debug.Log("Angle: " + angle);

      
    }
    void CalculateTorqueFeedback(float distance, float angle, int BulletColor)
    {
        //If the distance is bigger, the duration will be longer for more torque.
        //The amount of the force(speed of the motor) depends on the bullet force.
        //Which means bigger and faster bullet makes higher force.

        //거리에 따른 duration에 weight주기
        float normalizedDistance = Mathf.Clamp01(distance / 0.43f);
        if (BulletColor == 1)
        {
            //duraion_total = normalizedDistance * duraion_RedBullet;
            //Force_total = Force_RedBullet;
        }
        else if (BulletColor == 2)
        {
            //duraion_total = normalizedDistance * duraion_BlueBullet;
            //Force_total = Force_BlueBullet;
        }



        // 각도를 라디안으로 변환
        //float angleRad = angle * Mathf.Deg2Rad;

        // 목표 방향 벡터 계산
        //Vector3 targetDirection = new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
        //targetDirection = new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));


        //float distance_duration_scaleFactor;

        //Devide the area into 12 areas (30degree)
        int area = 0;

        if (angle >= 0 && angle < 90) //1
        {
            float DesiredAngleRad = (angle) * Mathf.Deg2Rad;
            weight_A = 0;
            weight_B = Mathf.Cos(DesiredAngleRad) * 1 / (Mathf.Cos(30 * Mathf.Deg2Rad));
            weight_C = Mathf.Cos(DesiredAngleRad) * 1 / (2 * Mathf.Cos(30 * Mathf.Deg2Rad)) + Mathf.Sin(DesiredAngleRad);
        }
        else if (angle >= 90 && angle < 180) //2
        {
            float DesiredAngleRad = (180 - angle) * Mathf.Deg2Rad;
            weight_A = Mathf.Cos(DesiredAngleRad) * 1 / (Mathf.Cos(30 * Mathf.Deg2Rad));
            weight_B = 0;
            weight_C = Mathf.Cos(DesiredAngleRad) * 1 / (2 * Mathf.Cos(30 * Mathf.Deg2Rad)) + Mathf.Sin(DesiredAngleRad);
        }
        else if (angle >= 180 && angle < 270) //3
        {
            float DesiredAngleRad = (angle - 180) * Mathf.Deg2Rad;
            weight_A = Mathf.Cos(DesiredAngleRad) * 1 / (Mathf.Cos(30 * Mathf.Deg2Rad)) + Mathf.Sin(DesiredAngleRad);
            weight_B = Mathf.Sin(DesiredAngleRad);
            weight_C = Mathf.Cos(DesiredAngleRad) * 1 / (2 * Mathf.Cos(30 * Mathf.Deg2Rad));
        }
        else if (angle >= 270 && angle < 360) //4
        {
            float DesiredAngleRad = (360 - angle) * Mathf.Deg2Rad;
            weight_A = Mathf.Sin(DesiredAngleRad);
            weight_B = Mathf.Cos(DesiredAngleRad) * 1 / (Mathf.Cos(30 * Mathf.Deg2Rad)) + Mathf.Sin(DesiredAngleRad);
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
            //Torque feedback
            if (collision.gameObject.CompareTag("Blue"))
            {
                Calculate_Force_Angle(dir_Hammer_Box);
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

