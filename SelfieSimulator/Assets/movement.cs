using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
	public Rigidbody rb;
	public Animator anim;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float y = Input.GetAxis ("Vertical");
		float x = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (y) > 0) {
			rb.velocity = transform.forward * y;
			anim.SetFloat ("speed",  rb.velocity.magnitude);
		} else
			anim.SetFloat ("speed", 0f);
		if (Mathf.Abs (x) > 0) {
			transform.Rotate (new Vector3 (0, x, 0));
		}
	}
}
