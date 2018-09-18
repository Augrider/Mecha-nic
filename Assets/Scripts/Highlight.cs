using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour {

	private LineRenderer _line;

	// Use this for initialization
	void Awake () {
		Messenger.AddListener (GameEvent.Start_Turn, OnStartTurn);
		Messenger.AddListener (GameEvent.End_Turn, OnEndTurn);
		_line = this.gameObject.GetComponent<LineRenderer> ();
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
	}
		
	// Update is called once per frame
	void Update () {
		if (Managers.trajectory._isChanged) {
			int end = Managers.trajectory.trace.Count-1;
			Managers.trajectory.Highlight (_line, end);
		}
		if (_line.positionCount > 0 && Managers.trajectory._isHighlighted==false)
			StartCoroutine (LightsOff());
	}

	IEnumerator LightsOff(){
		yield return new WaitForSeconds (0.5f);
		_line.positionCount = 0;
	}
}
