using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Freeze : MonoBehaviour {

	Rigidbody body;
	private NavMeshAgent agent;

	void Awake () {
		Messenger.AddListener (GameEvent.Start_Turn, unFreeze);
		Messenger.AddListener (GameEvent.End_Turn, freeze);
	}

	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.Start_Turn, unFreeze);
		Messenger.RemoveListener (GameEvent.End_Turn, freeze);
	}

	void Start(){
		body = GetComponent<Rigidbody> ();
		agent = GetComponent<NavMeshAgent> ();
	}

	void unFreeze(){
		body.constraints = RigidbodyConstraints.None;
		//agent.isStopped = false;
	}

	void freeze(){
		//agent.isStopped = true;
		agent.SetDestination (agent.transform.position);
		body.constraints = RigidbodyConstraints.FreezeAll;
	}
}
