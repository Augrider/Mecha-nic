using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Freeze : MonoBehaviour {

	Rigidbody body;
	private NavMeshAgent agent;

	void Awake () {
		Messenger.AddListener (GameEvent.Start_Turn, Unfreeze);
		Messenger.AddListener (GameEvent.End_Turn, StopHere);
	}

	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.Start_Turn, Unfreeze);
		Messenger.RemoveListener (GameEvent.End_Turn, StopHere);
	}

	void Start(){
		body = GetComponent<Rigidbody> ();
		agent = GetComponent<NavMeshAgent> ();
	}

	void Unfreeze(){
		body.constraints = RigidbodyConstraints.None;
		//agent.isStopped = false;
	}

	void StopHere(){
		//agent.isStopped = true;
		agent.SetDestination (agent.transform.position);
		body.constraints = RigidbodyConstraints.FreezeAll;
	}
}
