using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone : MonoBehaviour {
	public List<GameObject> people;
	public float hype;
	public float stagnation;
	bool stagnate = false;
	void OnTriggerEnter(Collider col){
		if (col.GetComponent<npc> () != null || col.GetComponent<expressionController> () != null) {
			people.Add (col.gameObject);

			if (col.GetComponent<npc> () != null) {
                col.GetComponent<npc>().zoningcount = 0;
                col.GetComponent<npc>().zone = this;
            }
			
		}
	}

	void OnTriggerExit(Collider col){
		people.Remove (col.gameObject);
	}
	// Use this for initialization
	void Start () {
		people = new List<GameObject> ();
		StartCoroutine (checkHypeStagnation ());
	}
	IEnumerator checkHypeStagnation(){
		yield return new WaitForSeconds (3);
		if (!stagnate) {
			if (people.Count > 0) {
				hype += people.Count;
				stagnation += people.Count / 2;
				if (stagnation > 100) {
					stagnate = true;
				}
			}
		} else {
			if (hype > 0) {
				hype -= 1f;
			} else {
				stagnate = false;
				stagnation = 0;
			}
		}
		StartCoroutine (checkHypeStagnation ());
	}
	// Update is called once per frame
	void Update () {
		
	}
}
