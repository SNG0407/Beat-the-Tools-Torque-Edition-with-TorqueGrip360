using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnakeMove : MonoBehaviour
{
    [SerializeField] Animator moveAni;
    [SerializeField] Transform[] TargetPos;
    [SerializeField] float speed = 1f;
    [SerializeField] float RotateSpeed = 1f;
    [SerializeField] private GameObject tutorialUI;
    int TargetNum = 0;
    public bool isMoving = false;
    private Vector3 NewTargetPos;
    void Start()
    {
       // transform.position = TargetPos[TargetNum].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MovePath();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("A pressed");
            moveAni.SetTrigger("Moving");
            isMoving = true;
            //moveAni.
        }
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    moveAni.SetTrigger("Stopped");
        //}
    }

    public void SnakeMovingBtn()
    {
        moveAni.SetTrigger("Moving");
        isMoving = true;
    }

    private void MovePath()
    {
        NewTargetPos = new Vector3(TargetPos[TargetNum].transform.position.x, transform.position.y, TargetPos[TargetNum].transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, NewTargetPos, speed * Time.deltaTime);
        //transform.LookAt(TargetPos[TargetNum]);

        Vector3 dir = TargetPos[TargetNum].transform.position - this.transform.position;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * RotateSpeed);

        if (transform.position == NewTargetPos && isMoving == true)
        {
            
            if (TargetNum == TargetPos.Length-1)
            {
                isMoving = false;
                moveAni.SetTrigger("Stopped");
                tutorialUI.SetActive(true);
            }
            else
            {
                TargetNum++;
            }
        }
    }
}
