using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Walk))]

public class MovementMethod : MonoBehaviour{

	public static Walk walk;
	public static Jump jump;

	void Awake() {
		walk = GetComponent<Walk>();
		jump = GetComponent<Jump>();
	}
}
