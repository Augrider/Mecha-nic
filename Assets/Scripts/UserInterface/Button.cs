using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
		/*Messenger.AddListener (GameEvent.Start_Turn, ObjectOff);
		Messenger.AddListener (GameEvent.End_Turn, ObjectOn);*/
	}

	public void OnClick() {
		Messenger.Broadcast (GameEvent.Start_Turn);
	}

	/*public void ObjectOff() {
		gameObject.SetActive (false);
	}
	public void ObjectOn() {
		gameObject.SetActive (true);
	}*/
}
