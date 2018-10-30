using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModeSwitch : MonoBehaviour {

	void Awake () {
		Messenger.AddListener (GameEvent.Start_Turn, Perspective);
	}

	void OnDestroy () {
		Messenger.RemoveListener (GameEvent.Start_Turn, Perspective);
	}

	void Perspective(){
		Camera.main.orthographic = false;
	}
}
