using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxFollowing : MonoBehaviour
{
    public Transform TargetPlayer;
    public float followSpeed;
    public float followTime;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        iTween.MoveUpdate(this.gameObject, iTween.Hash("position", TargetPlayer.position + offset, "time", followTime, "easetype", iTween.EaseType.easeInOutSine));
    }
}
