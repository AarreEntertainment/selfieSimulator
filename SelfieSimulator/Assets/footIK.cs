using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footIK : MonoBehaviour {
	public Transform leftobj;
	public Transform rightobj;
	public Vector3 lcollisionPoint;
	public Quaternion lrot;
	public Vector3 rcollisionPoint;
	public Quaternion rrot;
	public float lik;
	public float rik;
	public Transform leftFoot;
	public Transform rightFoot;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	void footRay(string ikstring, ref float ik, ref Vector3 collisionPoint, ref Quaternion rotation, ref Transform foot){
		RaycastHit hit;
		if (Physics.Raycast (foot.position + Vector3.up, Vector3.down, out hit, 2f)) {
			Vector3 rot = new Vector3 (0, transform.rotation.eulerAngles.y, 0);
			rotation = Quaternion.Euler (hit.normal + rot);
			if (Mathf.Abs (foot.position.y - hit.point.y) < 0.1f) {
				ik = Mathf.Abs( foot.position.y - hit.point.y) * 10f;
		
			} else {
				ik = 0;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (anim.enabled) {
			footRay ("leftfoot", ref lik, ref lcollisionPoint, ref lrot, ref leftFoot);
			footRay ("rightfoot", ref rik, ref rcollisionPoint, ref rrot, ref rightFoot);

		}
	}
	void OnAnimatorIK(){
		anim.SetIKPositionWeight (AvatarIKGoal.LeftFoot, lik);
		anim.SetIKPositionWeight (AvatarIKGoal.RightFoot, rik);
		anim.SetIKPosition (AvatarIKGoal.LeftFoot, leftobj.position);
		anim.SetIKPosition (AvatarIKGoal.RightFoot, rightobj.position);
		anim.SetIKRotationWeight (AvatarIKGoal.LeftFoot, lik);
		anim.SetIKRotationWeight (AvatarIKGoal.RightFoot, rik);
		anim.SetIKRotation (AvatarIKGoal.LeftFoot, lrot);
		anim.SetIKRotation (AvatarIKGoal.RightFoot, rrot);
	}
}
