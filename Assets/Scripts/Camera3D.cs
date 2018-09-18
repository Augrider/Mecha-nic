using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3D : MonoBehaviour {

	public float moveSpeed = 2.0f;
	public GameObject target; 

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftControl)) {
			float Zoom = Input.GetAxis ("Mouse Y");
			transform.Translate (0, 0, Zoom * moveSpeed);
		}
		if (Input.GetMouseButton (2)) {
			float horInput = Input.GetAxis ("Mouse X");
			float verInput = Input.GetAxis ("Mouse Y");
			transform.Translate (horInput * moveSpeed, verInput * moveSpeed, 0);
		}
		/*
		 * Движение камеры при помощи Arrow кнопок
		 * TODO: Переписать говнокод хотя бы с switch 
		 */ 
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (moveSpeed, 0, 0);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (-moveSpeed, 0, 0);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Translate (0, moveSpeed, 0);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Translate (0, -moveSpeed, 0);
		}
		if (Input.GetKey (KeyCode.KeypadPlus)) {
			transform.Translate (0, 0, moveSpeed);
		}
		if (Input.GetKey (KeyCode.KeypadMinus)) {
			transform.Translate (0, 0, -moveSpeed);
		}
		if (Input.GetKey (KeyCode.Keypad1)) {
			transform.LookAt(target.transform);
			transform.Translate (Vector3.right * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.Keypad2)) {
			transform.Translate (0, 0, -moveSpeed);
		}
			/*if (Input.GetAxis ("Mouse ScrollWheel")!=0) {
			float Zoom = Input.GetAxis("Mouse ScrollWheel");
			transform.Translate (0, 0, Zoom * moveSpeed);
		}*/
	}
}

