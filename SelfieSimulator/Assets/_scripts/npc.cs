using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npc : MonoBehaviour {

	public NavMeshAgent agent;
	public float zoningcount = 0;
	public Animator anim;
	public Rigidbody rb;
	public float range;

	public bool staff;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		GetComponent<Animator> ().avatar = transform.GetChild (0).GetComponent<Animator> ().avatar;
		civilian();
	}

	public void push( float pushstrength = 3f){
			anim.SetFloat ("Hit", pushstrength);
			StartCoroutine (stayput ());
		}


	void LateUpdate(){

		anim.SetFloat ("Hit", 0);
		if (agent.velocity.magnitude < 1 && rb.angularVelocity.magnitude > 2)
			rb.angularVelocity = Vector3.zero;
	}


	// Update is called once per frame
	void Update () {
		if (!agent.enabled)
			return;
		if (goingtozone && agent.remainingDistance < 0.5f) {
			
			inZone ();
		}
		if (goingtoaction && agent.remainingDistance < 0.2f) {

		}

	}
	public void enableMovement(){
	}
	public void disableMovement(){
	}




    #region actions
    public zone zone;
	float changeZone = 0;
	bool goingtozone;
	bool goingtoaction;

	IEnumerator zonechangedecay(){
		yield return new WaitForSeconds (1);
		if (changeZone > 0)
			changeZone--;
		StartCoroutine (zonechangedecay());
	}

	public action inaction;

	public IEnumerator stayput(){
        zone = null;
		goingtozone = false;
		goingtoaction = false;
		if (anim.GetFloat ("Action") < 1) {
            if (zone != null)
            {
                Debug.Log(zone.name +" POASTING " + this.name);
            }
            
			anim.SetFloat ("Action", 100 + Random.Range (0, 7));
		}

		yield return new WaitForSeconds(Random.Range(10,100));
		if (anim.GetFloat ("Action") > 1 && inaction==null) {
			anim.SetFloat ("Action", 0);
		}
		civilian ();
	}
	void tryStartAction(){

	}
	void inZone(){
		changeZone = Random.Range (100, 500);
		civilian ();
	}
	public void civilian(){

		if (inaction) {
			agent.enabled = true;
			if(inaction.player==this.gameObject)
				inaction.onDisengage ();
			inaction = null;
		}

		Transform Destination = getTargetInRange (changeZone==0);
		if(Destination!=null)
			GetComponent<AICharacterControl> ().SetTarget (Destination);
		else {
			StartCoroutine (stayput ());
		}
	}

	#endregion

	#region scanning
	Transform getTargetInRange(bool changeZon){
		
		Transform ret = null;
		Collider[] colliders = Physics.OverlapSphere (transform.position + transform.forward * 5, range);
		List<Transform> actionobjects = new List<Transform> ();
		List<Transform> zones = new List<Transform>();
		if (changeZon && zones.Count == 0)
			changeZon = false;
		foreach (UnityEngine.Collider col in colliders) {
			if (col.GetComponent<action> () != null) {
				actionobjects.Add (col.transform);
			}
			else if (col.GetComponent<zone> () != null) {
				zones.Add (col.transform);
			}
		}

		if (actionobjects.Count > 0 && agent.enabled && !changeZon) {
			
			Transform chosenaction = actionobjects [Random.Range (0, actionobjects.Count)];
			if (chosenaction.GetComponent<action> ().player == null && !chosenaction.GetComponent<action> ().isInNPCFocus) {
				
				chosenaction.GetComponent<action> ().isInNPCFocus = true;
				inaction = chosenaction.GetComponent<action> ();
				ret = chosenaction;
				goingtoaction = true;
			}
		}
		else if (zones.Count > 0 && changeZon) {
			Transform closestZone = zones [0];
			if (zones.Count > 1) {
				for (int i = 1; i < zones.Count; i++) {
					if (zones [i].GetComponent<zone> ().hype >= closestZone.GetComponent<zone> ().hype &&
						Vector3.Distance (zones [i].position, transform.position) < Vector3.Distance (closestZone.position, transform.position)) {
						closestZone = zones [i];
					}

				}
			}
			ret = closestZone;
			goingtozone = true;
		}
		return ret;
	}
	#endregion
}
