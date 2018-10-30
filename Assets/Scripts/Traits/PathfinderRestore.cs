using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathfinderRestore : MonoBehaviour {
	private NavMeshHit closestHit;
	private NavMeshAgent agent;

	private void OnEnable() {
		agent = this.gameObject.GetComponent<NavMeshAgent>();
		this.gameObject.transform.localPosition = Vector3.zero;
		if (NavMesh.SamplePosition (this.gameObject.transform.position, out closestHit, 5f, NavMesh.AllAreas)) {
			this.gameObject.transform.position = closestHit.position;
			agent.enabled = false;
			agent.enabled = true;
		}
	}
}
