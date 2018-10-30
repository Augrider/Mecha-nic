using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlanningOnly : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Messenger.AddListener (GameEvent.Start_Turn, ObjectOff);
		Messenger.AddListener (GameEvent.End_Turn, ObjectOn);
	}

	void OnDestroy () {
		Messenger.RemoveListener (GameEvent.Start_Turn, ObjectOff);
		Messenger.RemoveListener (GameEvent.End_Turn, ObjectOn);
	}
		
	public void ObjectOff() {
		gameObject.SetActive (false);
	}
	public void ObjectOn() {
		gameObject.SetActive (true);
	}
}
