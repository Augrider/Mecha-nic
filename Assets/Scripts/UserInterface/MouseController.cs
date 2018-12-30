using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour{

	public static int playerId = 0;
	public static Vector3 MPos;
	public static Ray mouseRay;
	public int GotId;

	private PointerEventData Left;
	private PointerEventData Right;
	private PointerEventData Mid;
	private Vector3 position;

	[SerializeField]private LayerMask mask;
	[SerializeField]private float radius = 1.0f;
	[SerializeField]private float moveSpeed = 2.0f;

	private enum mouseState {Idle, Basic, Selected};

	private mouseState state;

	public void stateIdle(){
		switch (state){
			case mouseState.Basic:
				Controllers.elements.WheelReset();
				break;
		}
		setState(mouseState.Idle);
	}

	public void stateBasic(){
		
		setState(mouseState.Basic);
	}

	public void stateSelected(){
		setState(mouseState.Selected);

	}

	void setState(mouseState value) {
		//		switch ( state ) {
		//			case State.Animating:
		//				// state Animating exit logic
		//				break;
		//				// other states
		//		}
		state = value;
	}

	/**
* Обработчики событий делают только то, что можно в текущем состоянии.
*/
	void event1Handler() {
		switch (state) {
			case mouseState.Basic:
				if (Input.GetAxis("Mouse X") != 0.0 || Input.GetAxis("Mouse Y") != 0.0) {
					GotId = GetId(radius, out position);
				}

				if (Input.GetMouseButtonDown(0) && (!EventSystem.current.IsPointerOverGameObject())) {
					Controllers.elements.WheelReset();
				}

				if (Input.GetMouseButtonDown(1)) {						
					Controllers.elements.WheelSet(position);
					if (GotId>=0)
						Debug.Log("Selected");
				}

				if ((Input.GetMouseButtonUp(1)) && (EventSystem.current.IsPointerOverGameObject())) {
					var up = Left.pointerCurrentRaycast.gameObject;
					if (up != null) {
						var button = up.GetComponent<ButtonId>();
						if (button != null)
							Controllers.elements.buttons[button.buttonId].onClick.Invoke();
					}
				}

				if (Input.GetKey (KeyCode.LeftControl)) {
					Controllers.elements.WheelReset();
					float Zoom = Input.GetAxis ("Mouse Y");
					Camera.main.transform.Translate (0, 0, Zoom * moveSpeed);
				}

				if (Input.GetMouseButton (2)) {
					Controllers.elements.WheelReset();
					float horInput = Input.GetAxis ("Mouse X");
					float verInput = Input.GetAxis ("Mouse Y");
					Camera.main.transform.Translate (horInput * moveSpeed, verInput * moveSpeed, 0);
				}
				break;

			case mouseState.Idle:
				if (Input.GetKey(KeyCode.LeftControl)) {
					Controllers.elements.WheelReset();
					float Zoom = Input.GetAxis("Mouse Y");
					Camera.main.transform.Translate(0, 0, Zoom * moveSpeed);
				}

				if (Input.GetMouseButton(2)) {
					Controllers.elements.WheelReset();
					float horInput = Input.GetAxis("Mouse X");
					float verInput = Input.GetAxis("Mouse Y");
					Camera.main.transform.Translate(horInput * moveSpeed, verInput * moveSpeed, 0);
				}
				break;
		}
	}

	void Start(){
		state = mouseState.Basic;
	}

	// Update is called once per frame
	void OnGUI () {
		MPos = Input.mousePosition;
		MPos.z = -Camera.main.transform.position.z;
		MPos = Camera.main.ScreenToWorldPoint(MPos);

		mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		Left = EventSystem.current.GetComponent<StandaloneInputModuleCustom>().GetMouseData(-1);

		event1Handler();
	}

	//

	private int GetId(float radius, out Vector3 position){
		RaycastHit Hit = new RaycastHit ();
		int gotId=-1;
		position = Input.mousePosition;
		if (Physics.SphereCast(mouseRay.origin, radius, mouseRay.direction, out Hit, 30.0f, mask)) {		
			var pointer =  Hit.collider.gameObject.GetComponent<Pointer> ();
			if (pointer != null) {
				if (pointer.playerId==playerId){
					gotId = pointer.myId;
					if (Managers.spawn._chars[playerId].charCon.trajectories.Count>0){
						position = Managers.trajectory.FindPointOnLine(Managers.spawn._chars[playerId].charCon.trajectories[gotId], Hit.point);
						Controllers.wElements.PointerSet(position, 0.0f);
						position = Camera.main.WorldToScreenPoint(position);
					} else {
						gotId = -1;
						Controllers.wElements.PointerReset();
					}
				}
			} else {
				gotId = -2;
				Controllers.wElements.PointerReset();
			}
		} else {
			Controllers.wElements.PointerReset();
		}
		return gotId;
	}
}
