using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IGameManager {

	public ManagerStatus status { get; private set; }

	public void Startup() {
		Debug.Log ("Command Manager starting... ");
		status = ManagerStatus.Started;
	}

	public void AddCommand(int playerId, string name, Vector3 position, Vector3 parameters){
		Moves move = new Moves ();
		move._name = name;
		move._place = position;
		move._param = parameters;
		Managers.spawn._chars[playerId].charCon._moves.Add(move);
	}

	public void ClearAll(int playerId){
		Managers.spawn._chars[playerId].charCon._moves.Clear();
	}

}
