using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllableCharacter : MonoBehaviour {

	public static NavMeshAgent mecha;
	public static NavMeshAgent pathfinder;
	// Use this for initialization
	void Start () {
		NavMeshAgent agent;
		mecha = this.gameObject.GetComponent<NavMeshAgent>();
		int i = this.gameObject.transform.childCount;
		for(int k=0; k<i; k++){
			agent = this.gameObject.transform.GetChild(k).GetComponent<NavMeshAgent>();
			if (agent != null)
				pathfinder = agent;
		}
	}
}
