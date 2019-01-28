using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class action : MonoBehaviour {
	public float Action;
	public Vector3 entrypos;
	public GameObject player;
	public bool isInPlace;
	public bool isInAction;
	public UnityEvent inPlace;
	public UnityEvent outOfPlace;
	Quaternion original;
	BoxCollider impact;
	Rigidbody rb;
	public bool isInNPCFocus;
	// Use this for initialization
	void Start () {

		if(transform.parent.GetComponent<BoxCollider>()!=null)
			impact = transform.parent.GetComponent<BoxCollider> ();

		if (transform.parent.GetComponent<Rigidbody> () !=null)
			rb = transform.parent.GetComponent<Rigidbody> ();
		else
			rb = null;
		original = transform.parent.rotation;
	}


	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<globalEvents>().runEvent("ding");
			player = col.gameObject;
			isInPlace = true;
			inPlace.Invoke ();
		} else if (col.name.Contains ("npc") && player == null) {
			if (col.GetComponent<npc> ().inaction == this) {
				col.GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
				player = col.gameObject;
				isInPlace = true;
               
				onAction ();
                if (col.GetComponent<npc>().zone != null) {
                    Debug.Log(col.name + " POAST " + col.GetComponent<npc>().zone.name);
                }
				StartCoroutine (col.GetComponent<npc> ().stayput ());
			}
		} 
	}
	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			isInPlace = false;
			outOfPlace.Invoke();
		}
	}


	public void onAction(){
		if(rb!=null)
			rb.isKinematic = true;
		entrypos = player.transform.position;
		player.GetComponent<Rigidbody> ().isKinematic = true;
		if(impact!=null)
		impact.enabled = false;
		transform.parent.rotation = original;
		RaycastHit hit;
		//tähän pitäis vielä keksiä jotain
		if(rb!=null){
		//if(Physics.Raycast(transform.parent.position+Vector3.up/2, Vector3.down, out hit, 2f))
				//transform.parent.position = new Vector3 (transform.parent.position.x, hit.point.y, transform.parent.position.z);
			}
		player.GetComponent<Animator> ().SetFloat ("Action", Action);
		player.transform.position = transform.GetChild (0).position;
		player.transform.rotation = transform.GetChild (0).rotation;
		isInAction = true;
	}
	public void onDisengage(){
		player.GetComponent<Animator> ().SetFloat ("Action", 0);
		player.transform.position = entrypos;
		if(impact!=null)
		impact.enabled = true;
		if(rb!=null)
			rb.isKinematic = false;
		if (player.GetComponent<npc> () != null)
			return;
		player.GetComponent<Rigidbody> ().isKinematic = false;
		isInNPCFocus = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isInPlace && Input.GetButtonDown ("A") && player.tag=="Player") {
			onAction ();

		}

		if (isInAction) {
			
		}

		if (isInAction && Input.GetButtonDown ("B") && player.tag=="Player") {
			onDisengage ();
			isInNPCFocus = false;
			isInAction = false;
		}
	}
}
