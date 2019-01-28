using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expressionController : MonoBehaviour {
	bool _canMove = true;
	public bool canMove{get{
			if (selfieMode)
				return false;
			else
				return _canMove;
		}
		set{_canMove = value;}
	}
	public bool selfieMode;

	public Camera thirdPerson;
	public Camera selfieCamera;
	public Camera phoneCamera;

	public FacialAction[] FacialActions;
	public SkinnedMeshRenderer[] EditableMeshes;
	public float blendSpeed;
	public float DecayMultiplier;
	public string TakePhotoButton;
	public string ResetExpressionButton;

	public GameObject handObject;
	public GameObject watchObject;
	public RotateAction handRotation;
	public RotateAction phoneRotation;
	public RotateAction lookRotation;
	public RotateAction leyeRotation;
	public RotateAction reyeRotation;
	public GameObject phone;
	public GameObject phoneParent;
	public Animator anim;

	public void enableMovement(){
		canMove = true;
	}
	public void disableMovement(){
		canMove = false;
	}
	// Use this for initialization
	void Start () {
		
			
		anim = GetComponent<Animator> ();
		phoneParent = anim.GetBoneTransform (HumanBodyBones.RightHand).gameObject;
		leyeRotation.RotatableObject = anim.GetBoneTransform (HumanBodyBones.Head).GetChild(1);
		reyeRotation.RotatableObject = anim.GetBoneTransform (HumanBodyBones.Head).GetChild(2);
	}
	void LateUpdate(){
		if (!selfieMode)
			Camera.main.transform.root.Rotate (0, Input.GetAxis ("HorizontalR") * Time.deltaTime * 120, 0);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("LeftSB")) {
			selfieMode = !selfieMode;
			thirdPerson.enabled = !thirdPerson.enabled;
			selfieCamera.enabled = !selfieCamera.enabled;
			phoneCamera.enabled = !phoneCamera.enabled;
		}
		if (!selfieMode) {
			FacialAction.ResetBlendShapes (ref EditableMeshes);
			return;
		}
		
		if (Input.GetButton ("LeftB") && Input.GetButtonDown ("Y")) {
			FacialAction.ResetBlendShapes (ref EditableMeshes);
		}


		phone.transform.position = phoneParent.transform.position;
		phone.transform.rotation = phoneParent.transform.rotation;
		handRotation.update ();
		phoneRotation.update ();
		lookRotation.update ();
		leyeRotation.update ();
		reyeRotation.update ();
		foreach (SkinnedMeshRenderer renderer in EditableMeshes) {
			for (int i = 0; i < renderer.sharedMesh.blendShapeCount; i++) {
				
				if (renderer.GetBlendShapeWeight (i) > 0) {
					renderer.SetBlendShapeWeight (i, renderer.GetBlendShapeWeight(i) - Time.deltaTime * DecayMultiplier);
				}
			}
		}
		foreach (FacialAction action in FacialActions) {
			if(Input.GetButton(action.ControlButtonCode)){
				action.ModifyBlendShapes (ref EditableMeshes, blendSpeed);
			}
		}
	}

	void OnAnimatorIK(){
			if(selfieMode){
				anim.SetIKPositionWeight (AvatarIKGoal.RightHand, 1);
				anim.SetIKRotationWeight (AvatarIKGoal.RightHand, 1);
				anim.SetIKPosition (AvatarIKGoal.RightHand, handObject.transform.position);
				anim.SetIKRotation (AvatarIKGoal.RightHand, handObject.transform.rotation);

				anim.SetLookAtWeight (1);
				anim.SetLookAtPosition (watchObject.transform.position);

		} else if (anim.GetIKPositionWeight(AvatarIKGoal.LeftHand)>0) {
				anim.SetLookAtWeight (0);
				anim.SetIKPositionWeight (AvatarIKGoal.RightHand, 0);
				anim.SetIKRotationWeight (AvatarIKGoal.RightHand, 0);
			}

	}
}
