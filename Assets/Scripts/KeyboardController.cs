using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {

//	[SerializeField] private GameObject _point;
//
//
//	private Camera cam;
//	private RaycastHit Hit;
//	private int LayerMask = 1<<9;
//	public int GotId;
//	public float radius=0.3f;
//	public GameObject _HLine;
//

	void Awake () {
//		Messenger.AddListener (GameEvent.Start_Turn, OnStartTurn);
//		Messenger.AddListener (GameEvent.End_Turn, OnEndTurn);
//		cam = Camera.main.GetComponent<Camera>();
//		GotId = -1;
	}

	// Update is called once per frame
	void OnDestroy() {
//		Messenger.RemoveListener (GameEvent.Start_Turn, OnStartTurn);
//		Messenger.RemoveListener (GameEvent.End_Turn, OnEndTurn);
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
		if (Input.GetAxis ("Mouse X") != 0.0 || Input.GetAxis ("Mouse Y") != 0.0) {
			Vector3 MPos = Input.mousePosition;
			MPos.z = cam.transform.position.z;
			MPos = cam.ScreenToWorldPoint (MPos);
			MPos.z++;

			if (Physics.SphereCast (MPos, radius, new Vector3 (0, 0, -1), out Hit, 1.0f, LayerMask)) {
				_point.transform.position = Hit.point;
				_HLine = Hit.collider.gameObject;
				var liner = _HLine.GetComponent<Liner> ();
				GotId = liner.MyId;
			} else {
				_point.transform.localPosition = new Vector3 (0, 0, -1);
				GotId=-1;
			}
		}
		*/
		if (Input.GetKeyDown(KeyCode.Space)) {
			Messenger.Broadcast (GameEvent.Start_Turn);
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			/*
			 * TODO: Вызов главного меню (в отдельном файле)
			 */ 
		}
	}
}
