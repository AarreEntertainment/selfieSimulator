using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class blendSetting{
	public string primaryKey;
	public string combinationKey; 
	public int blendShapeIndex;
}

public class blendShapeModifier : MonoBehaviour {
	public GameObject makeupgui;
	public Camera selfieCam;
	//public Camera drawCam;
	public blendSetting[] editableBlendShapes;
	public float decreaseSpeed;
	public float increaseSpeed;
	SkinnedMeshRenderer rend;
	public SkinnedMeshRenderer optionalMeshRenderer;
	// Use this for initialization
	void Start () {
		rend = GetComponent<SkinnedMeshRenderer> ();
	}
	void modifyBlendShape(int index){
		if (rend.GetBlendShapeWeight (index) < 100)
			rend.SetBlendShapeWeight (index, rend.GetBlendShapeWeight(index) + increaseSpeed);
		if (optionalMeshRenderer != null) {
			if (optionalMeshRenderer.GetBlendShapeWeight (index) < 100)
				optionalMeshRenderer.SetBlendShapeWeight (index, rend.GetBlendShapeWeight(index) + increaseSpeed);
		}
	}
	void subtractFromBlendShape(int index)
	{
		if (rend.GetBlendShapeWeight (index) > 0)
			rend.SetBlendShapeWeight (index, rend.GetBlendShapeWeight(index) - Time.deltaTime*decreaseSpeed);
		if (optionalMeshRenderer != null) {
			if (optionalMeshRenderer.GetBlendShapeWeight (index) > 0)
				optionalMeshRenderer.SetBlendShapeWeight (index, rend.GetBlendShapeWeight(index) - Time.deltaTime*decreaseSpeed);
		}
	}
	void nullBlendShapes()
	{
		for (int i = 0; i < editableBlendShapes.Length; i++) {
			rend.SetBlendShapeWeight (editableBlendShapes [i].blendShapeIndex, 0);
			optionalMeshRenderer.SetBlendShapeWeight (editableBlendShapes [i].blendShapeIndex, 0);
		}
	}
	public bool noComboKeys(){
		bool ret = true;
		if(Input.GetKey("left shift") || Input.GetKey("left ctrl"))
			ret=false;
		return ret;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			if (!Input.GetKey (KeyCode.Backspace) && !Input.GetKey (KeyCode.Return))
				for (int i = 0; i < editableBlendShapes.Length; i++) {
					if (editableBlendShapes [i].combinationKey.Length > 0) {
						if (Input.GetKey (editableBlendShapes [i].combinationKey) && Input.GetKey (editableBlendShapes [i].primaryKey))
							modifyBlendShape (editableBlendShapes [i].blendShapeIndex);
						else
							subtractFromBlendShape (editableBlendShapes [i].blendShapeIndex);
					} else if (noComboKeys () && Input.GetKey (editableBlendShapes [i].primaryKey))
						modifyBlendShape (editableBlendShapes [i].blendShapeIndex);
					else
						subtractFromBlendShape (editableBlendShapes [i].blendShapeIndex);
				}
			else if (Input.GetKey (KeyCode.Backspace))
				nullBlendShapes ();
			}
		/*if(Input.GetKeyDown (KeyCode.Return)) {
			drawCam.enabled = !drawCam.enabled;
			selfieCam.enabled = !selfieCam.enabled;
			GetComponent<draw> ().drawMode = !GetComponent<draw> ().drawMode;
			makeupgui.SetActive (!makeupgui.activeSelf);
		}*/
	}
}
