using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCtrlStateList : MonoBehaviour {
	public static BasicState basicState;
	public static NoState noState;

	void Start(){
		basicState = new BasicState ();
		noState = new NoState ();
	}
}
