using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class selfieController : MonoBehaviour {
	public List<string> visibleObjects;
	public void addToList(string name){
		bool isin = false;
		foreach(string objname in visibleObjects){
			if (name.Split (' ') [0].ToLower() == objname.ToLower())
				return;
		}
		visibleObjects.Add (name.Split (' ') [0]);
	}
	void Update(){
		visibleObjects.Clear ();
		if (!camera.enabled) 
			return;
		

		Collider[] colliders = Physics.OverlapSphere (transform.position + transform.forward*3, 3f);
		foreach (UnityEngine.Collider col in colliders) {
			if(col.GetComponent<MeshRenderer>()!=null && col.GetComponent<Rigidbody>()!=null)
				if(col.GetComponent<MeshRenderer>().isVisible)
					addToList (col.name);
		}
	}

	public Camera camera;
	public RenderTexture original;
	public int resWidth = 600; 
	public int resHeight = 600;

	private bool takeHiResShot = false;

	public static string ScreenShotName(int width, int height) {
		
		return string.Format("{0}/screen_{1}x{2}_{3}.png", 
			System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures), 
			width, height, 
			System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}

	public void TakeHiResShot() {
		takeHiResShot = true;
	}

	void LateUpdate() {
		takeHiResShot |= Input.GetButton ("RightSB");
		if (takeHiResShot) {
			takeHiResShot = false;
				RenderTexture rt = new RenderTexture (resWidth, resHeight, 24);
				camera.targetTexture = rt;
				Texture2D screenShot = new Texture2D (resWidth, resHeight, TextureFormat.RGB24, false);
				RenderTexture.active = rt;	
				camera.Render ();
				
				screenShot.ReadPixels (new Rect (0, 0, resWidth, resHeight), 0, 0);
				
				Destroy (rt);
				
				byte[] bytes = screenShot.EncodeToPNG ();
				string filename = ScreenShotName (resWidth, resHeight);
				System.IO.File.WriteAllBytes (filename, bytes);
				RenderTexture.active = original; // JC: added to avoid errors
				camera.targetTexture = original;
				//Debug.Log (string.Format ("Took screenshot to: {0}", filename));

		}
	}
	}

