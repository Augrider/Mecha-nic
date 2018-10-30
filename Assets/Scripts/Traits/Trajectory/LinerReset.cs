using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinerReset : MonoBehaviour {

	private LineRenderer liner;

	void OnEnable() {
		liner = this.gameObject.GetComponent<LineRenderer> ();
	}

	void OnDisable() {
		if (liner != null) {
			liner.positionCount = 0;
		}
	}
}
