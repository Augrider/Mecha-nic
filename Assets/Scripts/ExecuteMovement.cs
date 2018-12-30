using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExecuteMovement : MonoBehaviour {

	public int nowId = -1;
	public int playerId;
	[SerializeField]private GameObject target;

	void Awake () {
		playerId = this.gameObject.GetComponent<ControllableCharacter>().playerId;
		Messenger.AddListener (GameEvent.Start_Turn, Execute);
		Messenger.AddListener (GameEvent.End_Turn, Deexecute);
	}
		
	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.Start_Turn, Execute);
		Messenger.RemoveListener (GameEvent.End_Turn, Deexecute);
	}

	public void Execute(){
		nowId = -1;
		StartCoroutine (Execution(playerId));
		StartCoroutine(Managers.spawn._chars[playerId].charCon.turret.Aim(target.transform));
		StartCoroutine(Managers.spawn._chars[playerId].charCon.turret.gun.Shoot(target.transform));
		Managers.cache.DisableAll();
	}

	public void Deexecute(){
		StopCoroutine (Execution(playerId));
		StopCoroutine(Managers.spawn._chars[playerId].charCon.turret.Aim(target.transform));
		StopCoroutine(Managers.spawn._chars[playerId].charCon.turret.gun.Shoot(target.transform));
		Managers.trajectory.ClearAll (playerId);
		Managers.movement.ClearAll (playerId);
		Managers.spawn._chars[playerId].state.stateIdle(playerId);
	}

	IEnumerator Execution(int playerId){
		int count = 0;
		while(count < Managers.spawn._chars[playerId].charCon._moves.Count) {
			switch (Managers.spawn._chars[playerId].charCon._moves [count]._name) {
				case "Move":
					Managers.spawn._chars[playerId].state.stateWalking(playerId);
					MovementMethod.walk.Realization(playerId, count);
				break;
				case "Jump":
					Managers.spawn._chars[playerId].state.stateJumping(playerId);
					MovementMethod.jump.Realization(playerId, count);
				break;
			}
			yield return new WaitUntil(()=>nowId==count);
			count++;
		}
		yield break;
	}
}
