using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class expression{
	public string emojiName;
	public string[] name;
	public Sprite emoji;

	bool checkNames(string check)
	{
		foreach (string nam in name) {
			if (check.ToLower() == nam.ToLower()) {
				return true;
			}
		}
		return false;
	}

	public bool checkEmoji(ref SkinnedMeshRenderer renderer){
			bool isEmoji = true;
			for (int j = 0; j < renderer.sharedMesh.blendShapeCount; j++) {
			if (!checkNames (renderer.sharedMesh.GetBlendShapeName(j))) {
					if (renderer.GetBlendShapeWeight (j) > 15) {
						isEmoji = false;
					}
				} else {
					if (renderer.GetBlendShapeWeight (j) < 70) {
						isEmoji = false;
					}
				}
			}
		return isEmoji;
	}
}

public class emojiController : MonoBehaviour {
	public SkinnedMeshRenderer body;
	public expression[] expressions;
	// Use this for initialization
	void Start () {
		body =  GameObject.FindGameObjectWithTag ("Player").GetComponent<expressionController> ().EditableMeshes [0];
	}

	// Update is called once per frame
	void Update () {

		foreach (expression exp in expressions) {
		if (exp.checkEmoji(ref body)) {
			GetComponent<Image> ().sprite = exp.emoji;
			GetComponent<Image> ().color = Color.white;
			break;
		}
		else {
			GetComponent<Image> ().color = Color.clear;
		}
	}
	}
}
