using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorModifier : MonoBehaviour {
	public GameObject selfieCamera;
	public GameObject thirdPerson;

	public Animator anim;
	public NavMeshAgent nav;


	public GameObject handObject;
	public GameObject watchObject;
	public bool IKActive = true;

	public bool selfieMode = false;

	public GameObject phone;
	public float speed;
	// Use this for initialization
	void Start () {
		

		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();

		Transform rightHand = anim.GetBoneTransform (HumanBodyBones.RightHand);

		phone.transform.SetParent (rightHand, false);



	}


	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.Tab)) {

			selfieMode = !selfieMode;
			selfieCamera.SetActive (!selfieCamera.activeSelf);
			thirdPerson.SetActive (!thirdPerson.activeSelf);
			if (selfieMode) {
				anim.ResetTrigger("startMoving");
				anim.SetFloat ("pose", 1);
				anim.SetTrigger("selfieMode");
			} else {
				anim.ResetTrigger("selfieMode");
				anim.SetTrigger("startMoving");
			}

		}

			if (selfieMode) {
				moveSkeleton ();
		} else {
				moveCharacter ();
			}
		
	}

	void OnAnimatorIK(){
		if (anim) {
			if (IKActive) {
				anim.SetIKPositionWeight (AvatarIKGoal.RightHand, 1);
				anim.SetIKRotationWeight (AvatarIKGoal.RightHand, 1);
				anim.SetIKPosition (AvatarIKGoal.RightHand, handObject.transform.position);
				anim.SetIKRotation (AvatarIKGoal.RightHand, handObject.transform.rotation);

				anim.SetLookAtWeight (1);
				anim.SetLookAtPosition (watchObject.transform.position);

			} else {
				anim.SetLookAtWeight (0);
				anim.SetIKPositionWeight (AvatarIKGoal.RightHand, 0);
				anim.SetIKRotationWeight (AvatarIKGoal.RightHand, 0);
			}
		}
	}

	void moveCharacter(){

		IKActive = false;
		float x, y, z;
		x = Input.GetAxis ("HorizontalWASD");
		z = Input.GetAxis ("VerticalWASD");
		y = Input.GetAxis ("TiltWASD");
		float speed = nav.speed;
		if (z < 0)
			speed = speed * 0.5f;

		nav.velocity = transform.forward * z * nav.speed;
		transform.Rotate (0, x * Time.deltaTime * nav.angularSpeed, 0);
		anim.SetFloat ("speed", z);
		anim.SetFloat ("direction", x);
	}

	void moveSkeleton(){
		IKActive = true;
		float x, y, z, poslimit, neglimit;
		x = Input.GetAxis ("HorizontalWASD");
		z = Input.GetAxis ("VerticalWASD");
		y = Input.GetAxis ("TiltWASD");
			

		if (Input.GetKey ("left shift"))
			handObject.transform.Rotate (new Vector3 (z * Time.deltaTime * 30, x * Time.deltaTime * 30, y * Time.deltaTime * 30));
		else {
			if (handObject.transform.parent.localPosition.z < .8f && y > 0)
				handObject.transform.parent.Translate (Vector3.forward * y * Time.deltaTime*0.5f);
			if (handObject.transform.parent.localPosition.z > 0.2f && y < 0)
				handObject.transform.parent.Translate (Vector3.forward * y * Time.deltaTime*0.5f);

			handObject.transform.parent.parent.Rotate (new Vector3 (z * Time.deltaTime * 30, x * Time.deltaTime * 30, 0));
		}
		
		x = Input.GetAxis ("Mouse X");
		z = Input.GetAxis ("Mouse Y");

		watchObject.transform.parent.Rotate (new Vector3 (z, x, 0));


	}
}
