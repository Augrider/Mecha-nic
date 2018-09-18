using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissappearable : MonoBehaviour {
	void Awake () {
		Messenger.AddListener (GameEvent.Start_Turn, OnStartTurn);
		Messenger.AddListener (GameEvent.End_Turn, OnEndTurn);
	}

	// Update is called once per frame
	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.Start_Turn, OnStartTurn);
		Messenger.RemoveListener (GameEvent.End_Turn, OnEndTurn);
	}

	private void OnStartTurn() {
		this.gameObject.SetActive (false);
	}

	private void OnEndTurn() {
		this.gameObject.SetActive (true);
		this.gameObject.transform.localPosition = Vector3.zero;
	}
}
