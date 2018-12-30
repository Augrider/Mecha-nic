using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharState : MonoBehaviour {
	/**
* Названия состояний описываются enum или строковыми константами, если язык не поддерживает enums.
*/ 
	private enum State { Idle, Walking, Jumping }

	/**
* Текущее состояние всегда скрыто. Иногда, бывает полезно добавить еще и переменную с предыдущим состоянием.
*/ 
	private State state;

	private Vector3 speed;
	/**
* Все смены состояний происходят только через вызов методов state<название состояния>().
* В них сперва может быть выполнена логика для выхода из КОНКРЕТНОГО предыдущего состояния в КОНКРЕТНОЕ новое.
* После чего выполняется setState(newValue) и специфическая для состояния логика.
*/ 
	public void stateIdle(int playerId) {
		Managers.cache.DisableAll();
		Managers.spawn._chars[playerId].charCon.id = 0;
		switch (state) {			
			case State.Jumping:
				speed = Managers.spawn._chars[playerId].charCon.rbody.velocity;
				Debug.Log(speed);
				if(speed.sqrMagnitude>=0.01){
					Managers.spawn._chars[playerId].charCon.rbody.isKinematic = true;
					Vector3 startPoint = Managers.spawn._chars[playerId].charCon.walker.transform.position + new Vector3 (0.0f, Managers.spawn._chars[playerId].charCon.height, 0.0f);
					Vector3[] path = MovementMethod.jump.JumpTrajectory(playerId, speed, startPoint);
					MovementMethod.jump.Assign(playerId, path, speed);
				}
				break;
			case State.Walking:
				Managers.spawn._chars[playerId].charCon.walker.SetDestination(Managers.spawn._chars[playerId].charCon.transform.position);
				break;
		}
		setState(State.Idle);
		Managers.spawn._chars[playerId].charCon.pathfinder.transform.localPosition = Vector3.zero;
	}

	public void stateWalking(int playerId) {
		Managers.spawn._chars[playerId].charCon.rbody.isKinematic = true;
		NavMeshAgent agent = Managers.spawn._chars[playerId].charCon.walker;
		agent.enabled = true;

//		PathfinderRestore(playerId, agent);
		setState(State.Walking);
	}

	public void stateJumping(int playerId) {
		Managers.spawn._chars[playerId].charCon.rbody.isKinematic = false;
		Managers.spawn._chars[playerId].charCon.walker.enabled = false;
		setState(State.Jumping);
	}

	/**
* У функций смены состояний могут быть параметры.
* stateIdle(0);
*/
	/**
* Обычно setState состоит только из
* state = value;
* или еще prevState = state; если нужно хранить предыдущее состояние.
* Но, также здесь находится общая логика выхода из предыдущего состояния.
*/
	void setState(State value) {
//		switch ( state ) {
//			case State.Animating:
//				// state Animating exit logic
//				break;
//				// other states
//		}
		state = value;
	}

	/**
* Обработчики событий делают только то, что можно в текущем состоянии.
*/
	void event1Handler() {
//		switch (state) {
//			case State.Idle:
//				// state Idle logic
//				break;
//				// other states
//		}
	}

	public void PathfinderRestore(int playerId, NavMeshAgent agent){
		NavMeshHit closestHit;
		if (NavMesh.SamplePosition (agent.gameObject.transform.position, out closestHit, 5f, NavMesh.AllAreas)) {
			agent.gameObject.transform.position = closestHit.position;
			agent.enabled = false;
			agent.enabled = true;
		}
	}
}
