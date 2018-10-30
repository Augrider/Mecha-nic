using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventHandler : MonoBehaviour {

	public void CameraSwitch( BaseEventData data )
	{
		if (Camera.main.orthographic)
			Camera.main.orthographic = false;
		else
			Camera.main.orthographic = true;
	}
}
