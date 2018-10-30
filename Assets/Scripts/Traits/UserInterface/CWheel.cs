using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWheel : MonoBehaviour {

	public static GameObject wheel;
	public static Vector3 wheelPos {
		get{return Camera.main.ScreenToWorldPoint(wheel.transform.position);}
		set{wheel.transform.position = Camera.main.WorldToScreenPoint(value);}
	}
	// Use this for initialization
	void OnEnable (){
		wheel = this.gameObject;
	}

	public static void Reset(){
		wheel.transform.position = new Vector3 (-100, 0, 0);
	}
}
