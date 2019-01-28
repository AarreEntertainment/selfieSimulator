using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class pen {
	public Color col;
	public int radius;
}
public class draw : MonoBehaviour {
	public Camera drawcam;
	public bool drawMode;
	Material drawableMat;
	public Color drawcol;
	public int radius;
	Texture2D mainTex;
	Texture2D paintedTex;
	public pen[] pens;
	public void changeColor(int index)
	{
		drawcol = pens [index].col;
		radius = pens [index].radius;
	}
	// Use this for initialization7
	void Start(){
		drawMode = false;
		drawableMat = GetComponent<SkinnedMeshRenderer> ().material;
		mainTex = drawableMat.mainTexture as Texture2D;
		paintedTex = drawableMat.GetTexture ("_DetailAlbedoMap") as Texture2D;
		//paintedTex = new Texture2D (mainTex.width, mainTex.height);
		Color[] arr = new Color[mainTex.width * mainTex.height];
		for(int i =0;i<arr.Length;i++)
		{
			arr[i] = Color.gray;
		}
		paintedTex.SetPixels(arr);
		paintedTex.Apply ();
		drawableMat.SetTexture ("_DetailAlbedoMap", paintedTex);
		drawableMat.EnableKeyword("_DETAIL_MULX2");
	}

	// Update is called once per frame
	void Update ()
	{


		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			if (Physics.Raycast (drawcam.ScreenPointToRay (Input.mousePosition), out hit)) {
				
				int xpos = Mathf.RoundToInt(hit.textureCoord.x*paintedTex.width);
				int ypos = Mathf.RoundToInt(hit.textureCoord.y*paintedTex.height);
				for (int i = -radius; i < radius; i++) {
					for (int j = -radius; j < radius; j++) {
						if(Mathf.Abs(i)+Mathf.Abs(j)<radius*1.5f)
							paintedTex.SetPixel ((int)xpos+j, (int)ypos+i, paintedTex.GetPixel((int)xpos+j, (int)ypos+i) * drawcol+(Color.grey/5));
					}
				}
			}
			paintedTex.Apply ();
			drawableMat.SetTexture ("_DetailAlbedoMap", paintedTex);
		}
	}
}
