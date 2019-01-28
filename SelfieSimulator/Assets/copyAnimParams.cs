using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyAnimParams : MonoBehaviour {
	public Animator otherAnimator;
	Animator Anim;

	// Use this for initialization
	void Start () {
		Anim = GetComponent<Animator> ();
	}

	void setFloat(string floatname){
		Anim.SetFloat (floatname, otherAnimator.GetFloat (floatname));
	}
	void setBool(string boolname){
		Anim.SetBool (boolname, otherAnimator.GetBool (boolname));
	}

	// Update is called once per frame
	void Update () {
		setFloat ("Forward");
		setFloat ("Turn");
		setBool ("Crouch");
		setBool ("OnGround");
		setFloat ("Jump");
		setFloat ("JumpLeg");
		setFloat ("Hit");
		setFloat ("Action");
	}
}
