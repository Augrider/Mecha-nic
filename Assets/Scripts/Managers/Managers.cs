﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Trajectory))]
[RequireComponent(typeof(Cache))]


public class Managers : MonoBehaviour {

	public static Movement movement { get; private set; }
	public static Trajectory trajectory { get; private set; }
	public static Cache cache { get; private set; }

	private List<IGameManager> _startSequence;

	void Awake () {
		movement = GetComponent<Movement> ();
		trajectory = GetComponent<Trajectory> ();
		cache = GetComponent<Cache> ();

		_startSequence = new List<IGameManager> ();
		_startSequence.Add (movement);
		_startSequence.Add (trajectory);
		_startSequence.Add (cache);

		StartCoroutine (StartupManagers ());
	}

	private IEnumerator StartupManagers(){
		foreach (IGameManager manager in _startSequence) {
			manager.Startup ();
		}
		yield return null;

		int numModules = _startSequence.Count;
		int numReady = 0;

		while (numReady < numModules) {
			int lastReady = numReady;
			numReady = 0;

			foreach (IGameManager manager in _startSequence) {
				if (manager.status == ManagerStatus.Started) {
					numReady++;
				}
			}
			if (numReady > lastReady)
				Debug.Log ("Progress: " + numReady + "/" + numModules);
			yield return null;
		}

		Debug.Log ("All managers started");
	}

}
