using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Security.Cryptography.X509Certificates;
using static UnityEditorInternal.VersionControl.ListControl;

public class SendTorqueFeedback : MonoBehaviour
{
    public SerialPort serialPort;
    public const string ComPortNum = "COM10";
    public SerialPort serial_Torque;
    // Start is called before the first frame update
    void Start()
    {
        serial_Torque  = new SerialPort(ComPortNum, 9600); //here change port - where you have connected arduino to computer
        serial_Torque.Open();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    serial_Torque.Write("A");
        //    Debug.Log("Collide with Glass");
        //}
        
       // sendTorque("0#0#100");
    }
    public void SendShootData(string data)
    {
        if (serial_Torque.IsOpen)
        {
            serial_Torque.WriteLine(data + "\n");
            Debug.Log("Data Sent: " + data);
        }
    }
    public void sendTorque(string data)
    {
        serial_Torque.Write(data);
/*        if (serial_Torque.IsOpen == false)
        {
            if (PortExists(serial_Torque.PortName))
            {
                Debug.Log("Collide with Glass");
                
                
                //serial_Torque.Close();
                Debug.Log("Haptic: " + data);
            }
            else
            {
                Debug.LogWarning($"Port {serial_Torque.PortName} does not exist.");
            }
        }*/
    }
    bool PortExists(string port)
    {
        string[] portNames = SerialPort.GetPortNames();
        foreach (string portName in portNames)
        {
            if (portName == port)
            {
                return true;
            }
        }
        return false;
    }
    void OnApplicationQuit()
    {
        // 어플리케이션 종료 시 시리얼 포트 닫기
        if (serialPort.IsOpen)
        {
            serial_Torque.WriteLine("A0#0#0#0" + "\n");
            serialPort.Close();
        }
    }
}
