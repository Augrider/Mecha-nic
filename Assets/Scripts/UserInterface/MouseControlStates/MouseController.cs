using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour{

	public static MCtrlState state;
	private PointerEventData Left;
	private PointerEventData Right;
	private PointerEventData Mid;
	private Vector3 MPos;

	void Start(){
		state = MCtrlStateList.basicState;
		Debug.Log(state);
	}
	// Update is called once per frame
	void OnGUI () {
		if(state!=null){
			MPos = Input.mousePosition;
			MPos.z = Camera.main.transform.position.z;
			MPos = Camera.main.ScreenToWorldPoint (MPos);

			Left = EventSystem.current.GetComponent<StandaloneInputModuleCustom>().GetMouseData(-1);

			state.Controls(Left, MPos);
		} else {
			Debug.Log(state);
			state = MCtrlStateList.basicState;
		}

	}
}
