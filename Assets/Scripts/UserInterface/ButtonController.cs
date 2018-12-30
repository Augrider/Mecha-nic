using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
	// Use this for initialization

	public void OnClick() {
		Messenger.Broadcast (GameEvent.Start_Turn);
	}

	public void WalkButtonClick(){
		Controllers.mouse.stateIdle();
		StartCoroutine(MovementMethod.walk.BuildCommand(MouseController.playerId));
	}

	public void JumpButtonClick(){
		Controllers.mouse.stateIdle();
		StartCoroutine(MovementMethod.jump.BuildCommand(MouseController.playerId));
	}
}
