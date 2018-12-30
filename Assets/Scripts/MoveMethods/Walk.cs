using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walk : MonoBehaviour {

	[SerializeField]private LayerMask mask;
	private RaycastHit Hit;
	private NavMeshHit closestHit;

	/*
	 * Функция задания в массив (будет в каждом методе движения) 
	*/
	public void Assign(int playerId, Vector3[] path) {
		int count = path.Length - 1;
		Managers.movement.AddCommand (playerId, "Move", path[count], new Vector3());
		Managers.spawn._chars[playerId].charCon.pathfinder.Warp(path[count]);
		for (int i = 0; i < count + 1; i++)
			path[i] += new Vector3 (0.0f, Managers.spawn._chars[playerId].charCon.height, 0.0f);
		Managers.trajectory.AddCommand (playerId, path);
	}

	public NavMeshPath Try(NavMeshAgent agent, Vector3 MPos){
		if (Physics.Raycast(MPos, Vector3.down, out Hit, 6.0f, mask)) {
			NavMeshPath pathos = new NavMeshPath ();
			if (agent.CalculatePath(Hit.point, pathos)) {
				return pathos;
			}
		}
		return null;
	}

	/*
	 * Функция выполнения команды (будет в каждом методе движения) 
	*/
	public void Realization(int playerId, int id) {
		NavMeshAgent agent = Managers.spawn._chars[playerId].charCon.walker;

		agent.SetDestination (Managers.spawn._chars[playerId].charCon._moves[id]._place);
	}

	public IEnumerator BuildCommand(int playerId){
		NavMeshPath path=new NavMeshPath();
		NavMeshAgent agent = Managers.spawn._chars[playerId].charCon.pathfinder;
		agent.enabled = true;
		Managers.spawn._chars[playerId].state.PathfinderRestore(playerId, agent);
		StartCoroutine(Controllers.elements.MessageDisplay("Выберите точку назначения"));
		while(!Input.GetMouseButton(1)){
			if (Input.GetAxis("Mouse X") != 0.0 || Input.GetAxis("Mouse Y") != 0.0) {
				path=Try(agent, MouseController.MPos);

				if (path!=null){
					Controllers.wElements.DrawLine(path.corners, Managers.spawn._chars[playerId].charCon.height, Color.cyan);
					int count = path.corners.Length - 1;
					Controllers.wElements.PointerSet(path.corners[count], Managers.spawn._chars[playerId].charCon.height);
				}
			}
			if(Input.GetMouseButton(0) && path.corners.Length!=0){
				Assign(playerId, path.corners);
				StartCoroutine(Controllers.wElements.DrawLast(playerId, Color.green));
				break;
			}
			yield return null;
		}
		if(Input.GetMouseButton(1))
			Controllers.wElements.Erase();
		Controllers.wElements.PointerReset();
		Controllers.mouse.stateBasic();
		yield return null;
	}

}
