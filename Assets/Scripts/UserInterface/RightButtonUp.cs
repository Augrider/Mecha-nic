using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RightButtonUp : MonoBehaviour {

	private Button button;

	void Start(){
		button = GetComponent<Button>();
	}
	// Update is called once per frame
	void Update () {
		if((Input.GetMouseButtonUp(1))&&(EventSystem.current.IsPointerOverGameObject()))
		{
			button.onClick.Invoke();
		}
	}
}
