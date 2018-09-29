using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dissappearable : MonoBehaviour {
	private NavMeshHit closestHit;
	private NavMeshAgent agent;

	void Awake () {
		Messenger.AddListener (GameEvent.Start_Turn, OnStartTurn);
		Messenger.AddListener (GameEvent.End_Turn, OnEndTurn);
		agent = this.gameObject.GetComponent<NavMeshAgent> ();
	}

	// Update is called once per frame
	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.Start_Turn, OnStartTurn);
		Messenger.RemoveListener (GameEvent.End_Turn, OnEndTurn);
	}

	private void OnStartTurn() {
		this.gameObject.SetActive (false);
	}

	private void OnEndTurn() {
		this.gameObject.SetActive (true);
		this.gameObject.transform.localPosition = Vector3.zero;
		if (NavMesh.SamplePosition (this.gameObject.transform.position, out closestHit, 5f, NavMesh.AllAreas)) {
			this.gameObject.transform.position = closestHit.position;
			agent.enabled = false;
			agent.enabled = true;
		}
	}
}
