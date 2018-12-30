using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		ControllableCharacter character = other.GetComponentInParent<ControllableCharacter>();
		if (character!=null){
			Debug.Log("You lost");
		}
	}
}
