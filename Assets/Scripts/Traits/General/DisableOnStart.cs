using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour {

	void Awake () {
		Messenger.AddListener (GameEvent.Start_Turn, ObjectOff);
	}

	void OnDestroy () {
		Messenger.RemoveListener (GameEvent.Start_Turn, ObjectOff);
	}

	public void ObjectOff() {
		gameObject.SetActive (false);
	}
}
