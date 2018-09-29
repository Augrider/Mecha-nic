using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour {

	private LineRenderer liner;

	void Awake() {
		liner = this.gameObject.GetComponent<LineRenderer> ();
	}

	void OnEnable() {
		Messenger.AddListener (GameEvent.Start_Turn, OnStartTurn);
	}

	private void OnStartTurn() {
		this.gameObject.SetActive (false);
		if (liner != null) {
			liner.positionCount = 0;
		}
	}
}
