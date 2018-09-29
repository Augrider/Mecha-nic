using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IGameManager {

	public ManagerStatus status { get; private set; }

	public List<Moves> _moves;

	public void Startup() {
		Debug.Log ("Command Manager starting... ");
		_moves= new List<Moves>();
		status = ManagerStatus.Started;
	}

	public void AddCommand(string name, Vector3 position, Vector3 parameters){
		Moves move = new Moves ();
		move._name = name;
		move._place = position;
		move._param = parameters;
		_moves.Add (move);
	}

	public void ClearAll(){
		_moves.Clear ();
	}

}
