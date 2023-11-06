using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	Animator anim;

	void Awake(){
		anim = GetComponent<Animator>();
	}

	public void TogglePreviewAnimation(bool b){
		anim.SetBool("Run",b);
	}
}
