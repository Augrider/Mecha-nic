using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Messenger.AddListener (GameEvent.Start_Turn, OnStartTurn);
	}
	
	// Update is called once per frame
	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.Start_Turn, OnStartTurn);
	}

	private void OnStartTurn(){
		Destroy (this.gameObject);
	}
}
