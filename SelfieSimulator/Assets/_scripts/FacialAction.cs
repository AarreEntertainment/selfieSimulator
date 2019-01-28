using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FacialAction{
	public string ControlButtonCode;
	public string LeftTriggerBlendshape;
	public string RightTriggerBlendshape;

	public string LeftStickLeftBlendshape;
	public string RightStickRightBlendshape;

	public string LeftStickRightBlendshape;
	public string RightStickLeftBlendshape;

	public string LeftStickUpBlendshape;
	public string RightStickUpBlendshape;

	public string LeftStickDownBlendshape;
	public string RightStickDownBlendshape;

	public string DPadUpBlendshape;
	public string DPadDownBlendshape;

	public string DPadLeftBlendshape;
	public string DPadRightBlendshape;


	private void modifyValue(ref SkinnedMeshRenderer renderer, string blendShapenames, float speed){
		string[] blendshapes = blendShapenames.Split (',');
		foreach (string blendShapename in blendshapes) {
			if (renderer.sharedMesh.GetBlendShapeIndex (blendShapename) > -1) {
				int blendShape = renderer.sharedMesh.GetBlendShapeIndex (blendShapename);
				if (renderer.GetBlendShapeWeight (blendShape) < 100) {
					renderer.SetBlendShapeWeight (blendShape, renderer.GetBlendShapeWeight (blendShape) + Time.deltaTime * speed);
				}
			}
		}

	}
	public static void ResetBlendShapes(ref SkinnedMeshRenderer[]editableMeshes){
		for (int i = 0; i < editableMeshes.Length; i++) {
			for (int j = 0; j < editableMeshes [i].sharedMesh.blendShapeCount; j++) {
				editableMeshes [i].SetBlendShapeWeight (j, 0);
			}
		}
	}
	public void ModifyBlendShapes (ref SkinnedMeshRenderer[] editableMeshes, float speed)
	{
		for (int i = 0; i< editableMeshes.Length;i++) {
			float mod = 0;
			mod = Input.GetAxis ("Trigger");
			if (mod < 0) {
				modifyValue (ref editableMeshes[i], LeftTriggerBlendshape, speed * Mathf.Abs(mod));
			}
			if (mod > 0) {
				modifyValue (ref editableMeshes[i], RightTriggerBlendshape, speed * Mathf.Abs(mod));
			}
			mod = Input.GetAxis ("HorizontalL");
			if (mod < 0) {
				modifyValue (ref editableMeshes[i], LeftStickLeftBlendshape, speed * Mathf.Abs(mod));
			}
			if (mod > 0) {
				modifyValue (ref editableMeshes[i], LeftStickRightBlendshape, speed * Mathf.Abs(mod));
			}
			mod = Input.GetAxis ("VerticalL");
			if (mod < 0) {
				modifyValue (ref editableMeshes[i], LeftStickDownBlendshape, speed * Mathf.Abs(mod));
			}
			if (mod > 0) {
				modifyValue (ref editableMeshes[i], LeftStickUpBlendshape, speed * Mathf.Abs(mod));
			}
			mod = Input.GetAxis ("HorizontalR");
			if (mod < 0) {
				modifyValue (ref editableMeshes[i], RightStickLeftBlendshape, speed * Mathf.Abs(mod));
			}
			if (mod > 0) {
				modifyValue (ref editableMeshes[i], RightStickRightBlendshape, speed * Mathf.Abs(mod));
			}
			mod = Input.GetAxis ("VerticalR");
			if (mod < 0) {
				modifyValue (ref editableMeshes[i], RightStickDownBlendshape, speed * Mathf.Abs(mod));
			}
			if (mod > 0) {
				modifyValue (ref editableMeshes[i], RightStickUpBlendshape, speed * Mathf.Abs(mod));
			}
			mod = Input.GetAxis ("HorizontalD");
			if (mod < 0) {
				modifyValue (ref editableMeshes[i], DPadLeftBlendshape, speed * Mathf.Abs(mod));
			}
			if (mod > 0) {
				modifyValue (ref editableMeshes[i], DPadRightBlendshape, speed * Mathf.Abs(mod));
			}
			mod = Input.GetAxis ("VerticalD");
			if (mod < 0) {
				modifyValue (ref editableMeshes[i], DPadDownBlendshape, speed * Mathf.Abs(mod));
			}
			if (mod > 0) {
				modifyValue (ref editableMeshes[i], DPadUpBlendshape, speed * Mathf.Abs(mod));
			}
		}
	}
}
