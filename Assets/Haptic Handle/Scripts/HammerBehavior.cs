using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using BNG;
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
    public float velocity_Hammer_mag;

    public Vector3 dir_Hammer_Box; //Torque direction = -1 * dir_Hammer_Box;
    Vector3 hammerNormal;// = Hammer_Center.transform.up;  // ����� ���� ����
    Vector3 projectedDirection; // ���� ������ �ظ��� ������ �鿡 ��������

    public GameObject SendTorqueData;
    private SendTorqueFeedback send_T_D;


    private float Force_total = 0;
    private float duraion_total = 100;

    private int PWM_Motor_A = 255;
    private int PWM_Motor_B = 255;
    private int PWM_Motor_C = 255;


    // 90��, 210��, 330�� ������ ���� ����
    private Vector3 direction_A = new Vector3(-Mathf.Sqrt(3) / 2, 0, -0.5f); // 210�� ����
    private Vector3 direction_B = new Vector3(Mathf.Sqrt(3) / 2, 0, -0.5f); // 330�� ����
    private Vector3 direction_C = new Vector3(0, 0, 1); // 90�� ����

    private Vector3 direction_X1;
    private Vector3 direction_X2;
    private Vector3 direction_Y1;
    private Vector3 direction_Y2;



    //Haptic vibration
    private InputBridge input;

    [SerializeField]
    private Transform marker; // ��ũ�� ��ġ
    private float weight_A;
    private float weight_B;
    private float weight_C;

    protected virtual void Awake()
    {
        input = InputBridge.Instance;
    }
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

        //�־��� 3 ������ ���� 4���� �������� ġȯ
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
        velocity_Hammer_mag = velocity_Hammer.magnitude;
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

        //Debug.Log("Local X-Axis: " + Hammer_Center.transform.right);   // ���� X ��
        //Debug.Log("Local Y-Axis: " + Hammer_Center.transform.up);      // ���� Y �� (����� ���� ����)
        //Debug.Log("Local Z-Axis: " + Hammer_Center.transform.forward); // ���� Z �� (��鿡�� 0���� �� ����)

    }
    private void OnDrawGizmos()
    {
        // ���� ���� Gizmos�� ȭ��ǥ �׸���
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Hammer_Center.transform.position, IdleBox.transform.position);
        //Gizmos.DrawRay(Hammer_Center.transform.position, dir_Hammer_Box);

        // ���� �����̽����� ��ũ�� ��ġ�� ������ ���� �����̽��� ��ȯ
        //var localPosition = Hammer_Center.transform.InverseTransformPoint(dir_Hammer_Box);

        // ���� �� ���� Gizmos�� ȭ��ǥ �׸���
        Gizmos.color = Color.blue;
        //Gizmos.DrawLine(Hammer_Center.transform.position, IdleBox.transform.position-Hammer_Center.transform.position);
        Gizmos.DrawRay(Hammer_Center.transform.position, Hammer_Center.transform.position - 1* IdleBox.transform.position);


        // ��ũ ���� Gizmos�� ȭ��ǥ �׸���
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Hammer_Center.transform.position, -1*(projectedDirection));
        //Gizmos.DrawRay(Hammer_Center.transform.position, Hammer_Center.transform.position - projectedDirection);

    }
    void Calculate_Force_Angle(Vector3 dir_Hammer_Box)
    {
        //Debug.Log("dir_Hammer_Box: " + dir_Hammer_Box);
        //Debug.Log("dir_Hammer_Box: " + dir_Hammer_Box.magnitude.ToString("F4"));

        // ����Ǵ� �� (���÷� �������ǵ� ������ ������ ����)
        projectedDirection =  dir_Hammer_Box - Vector3.Dot(dir_Hammer_Box, hammerNormal) * hammerNormal;
        //Debug.Log("projectedDirection: " + projectedDirection);
        //Debug.Log("projectedDirection: " + projectedDirection.magnitude.ToString("F4"));

        // ���� �����̽����� ��ũ�� ��ġ�� ������ ���� �����̽��� ��ȯ
        var localDirection = Hammer_Center.transform.InverseTransformDirection(-1*(projectedDirection));
        //Debug.Log("localDirection: " + localDirection);
        
        // x-z ����� �������� +z ������ 0���� �����Ͽ� ���� ���
        float angle = Mathf.Atan2(localDirection.x, localDirection.z) * Mathf.Rad2Deg;

        // ������ 0-360���� ��ȯ
        if (angle < 0)
        {
            angle += 360;
        }

        // ��� ���
        Debug.Log("Angle: " + angle);

        CalculateTorqueFeedback(angle);
    }
    void CalculateTorqueFeedback(float angle)
    {
        //more higher the velocity is more torque you will get.

        //velocity_Hammer
        
        if (angle >= 0 && angle < 90) //1
        {
            float DesiredAngleRad = (angle) * Mathf.Deg2Rad;
            weight_A = 0;
            weight_C = Mathf.Cos(DesiredAngleRad) * 1 / (Mathf.Cos(30 * Mathf.Deg2Rad));
            weight_B = Mathf.Cos(DesiredAngleRad) * 1 / (2 * Mathf.Cos(30 * Mathf.Deg2Rad)) + Mathf.Sin(DesiredAngleRad);
        }
        else if (angle >= 90 && angle < 180) //2
        {
            float DesiredAngleRad = (180 - angle) * Mathf.Deg2Rad;
            weight_A = Mathf.Cos(DesiredAngleRad) * 1 / (Mathf.Cos(30 * Mathf.Deg2Rad));
            weight_C = 0;
            weight_B = Mathf.Cos(DesiredAngleRad) * 1 / (2 * Mathf.Cos(30 * Mathf.Deg2Rad)) + Mathf.Sin(DesiredAngleRad);
        }
        else if (angle >= 180 && angle < 270) //3
        {
            float DesiredAngleRad = (angle - 180) * Mathf.Deg2Rad;
            weight_A = Mathf.Cos(DesiredAngleRad) * 1 / (Mathf.Cos(30 * Mathf.Deg2Rad)) + Mathf.Sin(DesiredAngleRad);
            weight_C = Mathf.Sin(DesiredAngleRad);
            weight_B = Mathf.Cos(DesiredAngleRad) * 1 / (2 * Mathf.Cos(30 * Mathf.Deg2Rad));
        }
        else if (angle >= 270 && angle < 360) //4
        {
            float DesiredAngleRad = (360 - angle) * Mathf.Deg2Rad;
            weight_A = Mathf.Sin(DesiredAngleRad);
            weight_C = Mathf.Cos(DesiredAngleRad) * 1 / (Mathf.Cos(30 * Mathf.Deg2Rad)) + Mathf.Sin(DesiredAngleRad);
            weight_B = Mathf.Cos(DesiredAngleRad) * 1 / (2 * Mathf.Cos(30 * Mathf.Deg2Rad));
        }
        //Debug.Log("Weight: " + weight_A + ", " + weight_B + ", " + weight_C);

        //Velocity -> 0~0.15
        // �ӵ��� 0���� 1 ���̷� ����ȭ
        float normalizedSpeed = Mathf.Clamp01(velocity_Hammer_mag / 0.02f);
        Debug.Log("normalizedSpeed: " + normalizedSpeed);
        normalizedSpeed = 1;
        PWM_Motor_A = (int)(255 * 1* weight_A) > 255 ? 255 : (int)(255 * 1 * weight_A);
        PWM_Motor_B = (int)(255 * 1 * weight_B) > 255 ? 255 : (int)(255 * 1 * weight_B);
        PWM_Motor_C = (int)(255 * 1 * weight_C) > 255 ? 255 : (int)(255 * 1 * weight_C);
        //Debug.Log("PWM_Motor: " + PWM_Motor_A + ", " + PWM_Motor_B + ", " + PWM_Motor_C);
        int dur = (int)duraion_total;
        string TorqueData = "A";
        TorqueData += PWM_Motor_A.ToString();
        TorqueData += "#";
        TorqueData += PWM_Motor_B.ToString();//'0';// 
        TorqueData += "#";
        TorqueData += PWM_Motor_C.ToString();
        TorqueData += "#";
        TorqueData += dur.ToString();
        send_T_D.SendShootData(TorqueData);
        //Debug.Log("TorqueData: " + TorqueData);

        input.VibrateController(0.1f, normalizedSpeed, 0.1f, ControllerHand.Right);
        //Debug.Log("HAAHAHA");
    }
    private void OnCollisionExit(Collision collision)
    {
        hasCollided = false; // Reset the flag after the hit message has been displayed and hidden
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Blue") || collision.gameObject.name== "Crate_Blue_Demo")
        {
            //float normalizedSpeed = Mathf.Clamp01(velocity_Hammer_mag / 0.02f);

            //input.VibrateController(0.1f, normalizedSpeed * 1f, 0.1f, ControllerHand.Right);
            //Debug.Log("HAAHAHA");
        }
           
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
                if (collision.gameObject.GetComponent<BoxCollider>() != null)
                {
                    if(collision.gameObject.GetComponent<BoxCollider>().enabled == true)
                    {
                        Calculate_Force_Angle(dir_Hammer_Box);
                    }
                    collision.gameObject.GetComponent<BoxCollider>().enabled = false;
                    //Debug.Log("Collider disenabled22...");
                    
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

