using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPMove : MonoBehaviour {

	[SerializeField] private GameObject _point;
	[SerializeField] private GameObject _Liner;

	private Camera cam;
	private RaycastHit Hit;
	private NavMeshAgent agent;
	private bool isEnded;
	private int LayerMask = 1 << 10;

	/*
	 * Константа для двойного клика
	 * Обнуляем константы
	 */ 
	private float lastClickTime = 0.0f;
	float catchTime = 0.5f;

	// Use this for initialization
	void Start () {
		cam = Camera.main.GetComponent<Camera>();
		agent = this.gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown (0)) {

			/*
			 * Захватываем двойной клик мыши
			 */ 
			if ((Time.fixedTime - lastClickTime) < catchTime) {
				
				Vector3 MPos = Input.mousePosition;
				MPos.z = cam.transform.position.z;
				MPos=cam.ScreenToWorldPoint (MPos);

				if (Physics.Raycast (MPos, transform.TransformDirection (Vector3.down), out Hit, 10, LayerMask)) {
					NavMeshPath pathos = new NavMeshPath();
					if (agent.CalculatePath (Hit.point, pathos)) {
						Managers.movement.AddCommand ("Move", Hit.point, new Vector3());
						Managers.trajectory.AddCommand (pathos.corners);
						this.gameObject.transform.position = Hit.point;
					}
				}
			}
			lastClickTime = Time.fixedTime;
		}
	}
}
