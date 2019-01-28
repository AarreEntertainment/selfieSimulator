using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RotateAction{
	public string ControlButtonCode;
	public string HorizontalAxisName;
	public string VerticalAxisName;
	public string TiltAxisName;
	public float HorizontalPositiveLimit;
	public float HorizontalNegativeLimit;
	float xVal;
	public float VerticalPositiveLimit;
	public float VerticalNegativeLimit;
	float yVal;
	public float TiltPositiveLimit;
	public float TiltNegativeLimit;
	float zVal;
	public float AngularSpeed;
	public Transform RotatableObject;
	bool initialized = false;
	bool isActive = true;
	public void update(){
		if (ControlButtonCode.Length > 0) {
			if (Input.GetButton (ControlButtonCode)) {
				isActive = true;
			} else {
				isActive = false;
			}
		}
		if (isActive) {
			if(Mathf.Abs(Input.GetAxis(HorizontalAxisName))>0){
				float x = Input.GetAxis (HorizontalAxisName)*Time.deltaTime*AngularSpeed;
				if (xVal + x > HorizontalNegativeLimit && xVal + x < HorizontalPositiveLimit) {
					RotatableObject.Rotate (0, x, 0);
					xVal += x;
				}
			}
			if (VerticalAxisName.Length > 0) {
				if (Mathf.Abs (Input.GetAxis (VerticalAxisName)) > 0) {
					float y = Input.GetAxis (VerticalAxisName)*Time.deltaTime*AngularSpeed;
					if (yVal + y > VerticalNegativeLimit && yVal + y < VerticalPositiveLimit) {
						RotatableObject.Rotate (y,0, 0);
						yVal += y;
					}
				}
			}
			if (TiltAxisName.Length > 0) {
				if (Mathf.Abs (Input.GetAxis (TiltAxisName)) > 0) {
					float z = Input.GetAxis (TiltAxisName)*Time.deltaTime*AngularSpeed;
					if (zVal + z > TiltNegativeLimit && zVal + z < TiltPositiveLimit) {
						RotatableObject.Rotate (0,0, z);
						zVal += z;
					}
				}
			}
		}
	}
}

public class TiltAction{
	public string ControlButtonCode;
	public string HorizontalAxisName;
	public float HorizontalPositiveLimit;
	public float HorizontalNegativeLimit;
	public float AngularSpeed;
	public Transform RotatableObject;
	Vector3 zeroPoint;
}