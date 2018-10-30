using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Freeze : MonoBehaviour {

	Rigidbody body;
	private NavMeshAgent agent;

	void OnEnable () {
		body = GetComponent<Rigidbody> ();
		agent = GetComponent<NavMeshAgent> ();
		Messenger.AddListener (GameEvent.Start_Turn, unFreeze);
		Messenger.AddListener (GameEvent.End_Turn, freeze);
	}

	void OnDisable () {
		Messenger.RemoveListener (GameEvent.Start_Turn, unFreeze);
		Messenger.RemoveListener (GameEvent.End_Turn, freeze);
	}
		
	void unFreeze(){
		body.constraints = RigidbodyConstraints.None;
	}

	void freeze(){
		if(agent.isOnNavMesh)
			agent.SetDestination (agent.transform.position);
		body.constraints = RigidbodyConstraints.FreezeAll;
	}
}
