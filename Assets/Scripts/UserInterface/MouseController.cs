using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour {

	[SerializeField] private GameObject _point;
	[SerializeField] private GameObject _wheel;

	private Camera cam;
	private RaycastHit Hit;
	private int LayerMask = 1<<9;
	public int GotId;
	public float radius = 0.3f;
	public GameObject _HLine;

	/*
	 * Константа для двойного клика
	 * catchTime - время захвата для двойного нажатия
	 */ 
	private float lastClickTime = 0.0f;
	public float catchTime = 0.5f;

	void Awake () {
		cam = Camera.main.GetComponent<Camera>();
		GotId = -1;
	}

	void onEnable() {
		Messenger.AddListener (GameEvent.Start_Turn, OnStartTurn);
		Messenger.AddListener (GameEvent.End_Turn, OnEndTurn);

	}

	void onDisable() {
		Messenger.RemoveListener (GameEvent.Start_Turn, OnStartTurn);
		Messenger.RemoveListener (GameEvent.End_Turn, OnEndTurn);
	}

	// Update is called once per frame
	void OnDestroy() {
		
	}

	private void OnStartTurn() {
		this.gameObject.SetActive (false);
	}

	private void OnEndTurn() {
		this.gameObject.SetActive (true);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		/*
		 * Захватываем двойной клик мыши
		 */

		if (Input.GetMouseButtonDown (0)) { 
			if ((Time.fixedTime - lastClickTime) < catchTime) {

				Vector3 MPos = Input.mousePosition;
				MPos.z = cam.transform.position.z;
				MPos = cam.ScreenToWorldPoint (MPos);
			}

			lastClickTime = Time.fixedTime;
		}
				
		if (Input.GetAxis ("Mouse X") != 0.0 || Input.GetAxis ("Mouse Y") != 0.0) {
			Vector3 MPos = Input.mousePosition;
			// Скорее всего из-за этого мы не можем крутить камеру
			MPos.z = cam.transform.position.z;
			MPos = cam.ScreenToWorldPoint (MPos);
			MPos.z++;

			if (Physics.SphereCast (MPos, radius, new Vector3 (0, 0, -1), out Hit, 1.0f, LayerMask)) {
				_HLine = Hit.collider.gameObject;
				var liner = _HLine.GetComponent<Liner> ();
				if (liner != null) {
					GotId = liner.MyId;
					Vector3 pos = Managers.trajectory.FindPoint (GotId, MPos);
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
	}
}
