using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSorter : MonoBehaviour {

	private Button[] buttons;

	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.position != new Vector3(-100, 0, 0)) {
			buttons = GetComponentsInChildren<Button>(false);

			int count = buttons.Length;
			int place = 0;
			foreach (Button button in buttons) {
				button.transform.localPosition = new Vector3 (200 * Mathf.Sin(place * 2 * Mathf.PI / count), 200 * Mathf.Cos(place * 2 * Mathf.PI / count), 0);
				place++;
			}
		}
	}
}
