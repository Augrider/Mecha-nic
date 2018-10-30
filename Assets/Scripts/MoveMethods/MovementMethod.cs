using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Walk))]

public class MovementMethod : MonoBehaviour{

	public static Walk walk;

	void Awake() {
		walk = GetComponent<Walk>();
	}
}
