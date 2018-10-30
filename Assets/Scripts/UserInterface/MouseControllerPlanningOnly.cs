using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseControllerPlanningOnly : MonoBehaviour  {

	[SerializeField] private GameObject _point;

	private Camera cam;
	private RaycastHit Hit;
	private int LayerMask = 1<<9;
	private Vector3 MPos;
	private Vector3 pos;
	private GameObject _HLine;

	public static int GotId;
	public float radius = 0.3f;

	/*
	 * Константа для двойного клика
	 * catchTime - время захвата для двойного нажатия
	 */ 
	private float firstClickTime = 0.0f;
	private float lastClickTime = 0.0f;
	public float catchTime = 0.5f;

	void Awake () {
		cam = Camera.main.GetComponent<Camera>();
		GotId = -1;
	}

	// Update is called once per frame
	void Update () {
		MPos = Input.mousePosition;
		MPos.z = cam.transform.position.z;
		MPos = cam.ScreenToWorldPoint (MPos);

		if (Input.GetMouseButtonDown(0)) { 
			firstClickTime=lastClickTime;
			lastClickTime=Time.fixedTime;
			if ((lastClickTime - firstClickTime) < catchTime){
				lastClickTime = Time.fixedTime;
				if(GotId!=-2)
					MovementMethod.walk.Assign (ControllableCharacter.pathfinder, MPos);
			}
			//if(_wheel.transform.position!=new Vector3(-100,0,0))
			//	_wheel.transform.position = new Vector3(-100,0,0);
		}
				
		if (Input.GetAxis ("Mouse X") != 0.0 || Input.GetAxis ("Mouse Y") != 0.0) {
			MPos.z+=3;

			if (Physics.SphereCast (MPos, radius, new Vector3 (0, 0, -1), out Hit, 5.0f, LayerMask)) {
				_HLine = Hit.collider.gameObject;
				var liner = _HLine.GetComponent<Liner> ();
				if (liner != null) {
					GotId = liner.MyId;
					pos = Managers.trajectory.FindPoint (GotId, MPos);
					pos.y += 3.0f;
					_point.transform.position = pos;
				} else {
					var pointer = _HLine.GetComponent<Pointer> ();
					if (pointer != null)
						GotId = pointer.MyId;
					else
						GotId = -2;
					_point.transform.localPosition = new Vector3 (0, 0, -1);
				}
			} else {
				_point.transform.localPosition = new Vector3 (0, 0, -1);
				GotId=-1;
			}
		}

		if (Input.GetMouseButtonDown (1)) { 
			if (GotId!=-2){
				if(GotId!=-1)
					CWheel.wheelPos = pos;
				else
					CWheel.wheelPos = MPos;
			}
		}
	}
}
