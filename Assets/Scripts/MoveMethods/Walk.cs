using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Walk : MonoBehaviour {

	private int LayerMask = 1 << 10;
	private RaycastHit Hit;

	/*
	 * Функция задания в массив (будет в каждом методе движения) 
	*/
	public void Assign(NavMeshAgent agent, Vector3 MPos) {
		if (Physics.Raycast (MPos, Vector3.down, out Hit, 10, LayerMask)) {
			NavMeshPath pathos = new NavMeshPath();
			if (agent.CalculatePath (Hit.point, pathos)) {
				Managers.movement.AddCommand ("Move", Hit.point, new Vector3());
				Managers.trajectory.AddCommand (pathos.corners);
				agent.Warp (Hit.point);
			}
		}
	}
	/*
	 * Функция выполнения команды (будет в каждом методе движения) 
	*/
	public void Realization(NavMeshAgent agent, Vector3 destination) {
		agent.SetDestination (destination);
	}

	public void OnClick(){
		Assign(ControllableCharacter.pathfinder, CWheel.wheelPos);
		CWheel.Reset();
	}
}
