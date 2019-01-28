using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class publicEvent {
    public string name;
    public UnityEngine.Events.UnityEvent myEvent;
    }

public class globalEvents : MonoBehaviour {
    public List<publicEvent> events;

    public void runEvent(string name) {
        events.Find(obj => obj.name == name).myEvent.Invoke();
        }
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
