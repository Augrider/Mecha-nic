using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasicState : MCtrlState{
	public override void Controls(PointerEventData Left, Vector3 MPos){
		if (Input.GetMouseButtonDown(0)){
			if (Left.clickCount >= 2){
//				Debug.Log("2");
				MovementMethod.walk.Assign(ControllableCharacter.pathfinder, MPos);
				Left.clickCount = 0;
			}
			CWheel.wheel.transform.position = new Vector3 (-100, 0, 0);
//			Left.clickCount = 0;
		}
		if(Input.GetMouseButtonDown(1)){
			CWheel.wheelPos = MPos;
//			Right.clickCount = 0;
		}
	}
}
