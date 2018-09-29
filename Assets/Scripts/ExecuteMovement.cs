using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExecuteMovement : MonoBehaviour {

	public NavMeshAgent agent;

	private bool isStopped;

	void Awake () {
		Messenger.AddListener (GameEvent.Start_Turn, Execute);
		Messenger.AddListener (GameEvent.End_Turn, Deexecute);
		isStopped = false;
	}
		
	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.Start_Turn, Execute);
		Messenger.RemoveListener (GameEvent.End_Turn, Deexecute);
	}

	public void Execute(){
		//isStopped = false;
		StartCoroutine (Execution());
	}

	public void Deexecute(){
		StopCoroutine (Execution());
		Managers.trajectory.ClearAll ();
		Managers.movement.ClearAll ();
	}

	IEnumerator Execution(){
		int count = 0;
		while(count < Managers.movement._moves.Count) {
			if (isStopped)
				break;
			switch (Managers.movement._moves [count]._name) {
			case "Move":
				agent.SetDestination (Managers.movement._moves [count]._place);
				break;
			}
			if (agent.pathPending)
				yield return null;
			while (agent.remainingDistance >= 0.0001f) {
				yield return null;
			}
			count++;
		}
		yield break;
	}

}
