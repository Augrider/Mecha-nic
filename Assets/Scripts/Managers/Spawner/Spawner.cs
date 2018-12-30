using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MechComponents;

public class Spawner : MonoBehaviour, IGameManager {
	public ManagerStatus status { get; private set; }
	public struct mecha{
		public ControllableCharacter charCon;
		public CharState state;
	}

	[SerializeField]private ControllableCharacter _mechaTemplate;
	[SerializeField]private int spawnCount;
	public int _playerCount;

	private enum charStatus {Ready, Loading};
	[SerializeField]private Transform[] spawnPoints;

	public List<mecha> _chars;

	public void Startup() {
		Debug.Log ("Spawn Manager starting... ");
		_chars = new List<mecha> ();
		StartCoroutine(Spawn());
	}

	private IEnumerator Spawn(){
		mecha temp = new mecha ();
		for (int i=0; i<_playerCount; i++){
			temp.charCon = Instantiate(_mechaTemplate, spawnPoints[i]);
			temp.state = temp.charCon.GetComponent<CharState>();
			_chars.Add(temp);
			_chars[i].charCon.playerId = i;
			MechaLoader(i);
			yield return null;
		}
		status = ManagerStatus.Started;
		yield return null;
	}

	private void MechaLoader(int id){		
		GameObject mech = Instantiate(Resources.Load("Mech/Robo1", typeof(GameObject)), _chars[id].charCon.transform) as GameObject;
		mech.transform.parent = _chars[id].charCon.transform;
		mech.transform.Translate(0.0f, _chars[id].charCon.height, 0.0f);
		var turret = mech.GetComponentInChildren<Turret>();
		if (turret != null)
			_chars[id].charCon.turret = turret;
	}
}
