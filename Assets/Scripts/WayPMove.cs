using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPMove : MonoBehaviour {

	[SerializeField] private GameObject _point;
	[SerializeField] private GameObject _Liner;

	private Camera cam;
	private float clFirst;
	private float clLast;
	private RaycastHit Hit;
	private NavMeshAgent agent;
	private bool isEnded;

	// Use this for initialization
	void Start () {
		clFirst = 0;
		clLast = 1;
		cam = Camera.main.GetComponent<Camera>();
		agent = this.gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			clFirst = clLast;
			clLast = Time.fixedTime;
			if ((clLast - clFirst) < 0.5) {
				clFirst = 0;
				clLast = 1;

				Vector3 MPos = Input.mousePosition;
				MPos.z = cam.transform.position.z;
				MPos=cam.ScreenToWorldPoint (MPos);

				if (Physics.Raycast (MPos, transform.TransformDirection (Vector3.down), out Hit, 10)) {
					NavMeshPath pathos = new NavMeshPath();
					if (agent.CalculatePath (Hit.point, pathos)) {
						GameObject liner = Instantiate (_Liner, new Vector3(0.0f, 3.0f, 0.0f), Quaternion.identity);
						Vector3 param = new Vector3 ();
						Managers.movement.AddCommand ("Move", Hit.point, param);
						int end = pathos.corners.Length - 1;
						Instantiate (_point, new Vector3 (Hit.point.x, pathos.corners[end].y + 3.0f, Hit.point.z), Quaternion.identity);
						Managers.trajectory.AddCommand (pathos.corners, liner);
						this.gameObject.transform.position = Hit.point;
					}
				}
			}
		}
	}
}
