using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour {

	public float moveSpeed = 2.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftControl)) {
			CWheel.Reset();
			float Zoom = Input.GetAxis ("Mouse Y");
			transform.Translate (0, 0, Zoom * moveSpeed);
		}
		if (Input.GetMouseButton (2)) {
			CWheel.Reset();
			float horInput = Input.GetAxis ("Mouse X");
			float verInput = Input.GetAxis ("Mouse Y");
			transform.Translate (horInput * moveSpeed, verInput * moveSpeed, 0);
		}
		/*if (Input.GetAxis ("Mouse ScrollWheel")!=0) {
			float Zoom = Input.GetAxis("Mouse ScrollWheel");
			transform.Translate (0, 0, Zoom * moveSpeed);
		}*/
	}
}
