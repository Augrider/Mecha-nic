using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointCollision : MonoBehaviour {

	private int myId;
	private int playerId;
//	[SerializeField] private GameObject[] targets;

	void OnEnable(){
		myId = this.GetComponent<Pointer>().myId;
		playerId = this.GetComponent<Pointer>().playerId;
	}
	
	void OnTriggerEnter(Collider other){
		var executor = other.gameObject.GetComponent<ExecuteMovement>();
		if((executor!=null)&&(executor.playerId==playerId)){
			executor.nowId = myId;
		}
	}
}
