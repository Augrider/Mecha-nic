using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour {

	private LineRenderer _line;

	// Use this for initialization
	void Awake () {
		_line = this.gameObject.GetComponent<LineRenderer> ();
	}		
	// Update is called once per frame
	void Update () {
		if (Managers.trajectory._isChanged) {
			int last = Managers.trajectory.trajectories.Count-1;
			Managers.trajectory.Highlight (_line, last);
		}
		if (_line.positionCount > 0 && Managers.trajectory._isHighlighted==false)
			StartCoroutine (LightsOff());
	}

	IEnumerator LightsOff(){
		yield return new WaitForSeconds (0.5f);
		_line.positionCount = 0;
	}
}
