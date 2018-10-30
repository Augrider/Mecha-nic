using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

	// Use this for initialization

	public void OnClick() {
		Messenger.Broadcast (GameEvent.Start_Turn);
	}
}
