using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllers : MonoBehaviour {

	public static MouseController mouse;
	public static UIWorldElements wElements;
	public static UIElements elements;

	// Use this for initialization
	void Start () {
		mouse = GetComponent<MouseController>();
		wElements = GetComponent<UIWorldElements>();
		elements = GetComponent<UIElements>();
	}
}
