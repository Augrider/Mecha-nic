using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSorter : MonoBehaviour {

	private Transform[] buttons;

	// Update is called once per frame
	void LateUpdate () {
		if (Input.GetMouseButtonDown (1)) {
			Vector3 MPos = Input.mousePosition;

			this.gameObject.transform.position = MPos;


			buttons = GetComponentsInChildren<Transform> (false);

			int count = buttons.Length;
			int place = 0;
			foreach (Transform button in buttons) {
				button.localPosition = new Vector3 (160 * Mathf.Cos (place*2 * Mathf.PI / count), 160 * Mathf.Sin (place*2 * Mathf.PI / count), 0);
				place++;
			}
		}
		if (Input.GetMouseButtonDown (0))
			this.gameObject.transform.position = new Vector3 (-100, 0, 0);
	}
}
