﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liner : MonoBehaviour {

	public int MyId;

	// Use this for initialization
	void Awake () {
		MyId = Managers.trajectory.id;
		Managers.trajectory.id++;
	}
}
