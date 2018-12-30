using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MechComponents;

public class ControllableCharacter : MonoBehaviour {

	[SerializeField]private NavMeshAgent _pathfinder;

	public List<Moves> _moves;
	public List<Vector3[]> trajectories;
	public int id = 0;
	public int playerId;

	public NavMeshAgent walker;
	public Rigidbody rbody;
	public NavMeshAgent pathfinder;
	public Turret turret;

	public float maxSpeed = 10f;
	public float height = 3.0f;
	// Use this for initialization
	void Start () {
		walker = this.gameObject.GetComponent<NavMeshAgent>();
		rbody = this.gameObject.GetComponent<Rigidbody>();
		pathfinder = Instantiate(_pathfinder, walker.gameObject.transform);
		_moves = new List<Moves> ();
		trajectories = new List<Vector3[]> ();
		Managers.spawn._chars[playerId].state.stateIdle(playerId);
	}

	void Update(){
		if (walker==null)
			walker = this.gameObject.GetComponent<NavMeshAgent>();		
	}
}
