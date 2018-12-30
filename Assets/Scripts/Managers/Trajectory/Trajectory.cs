using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MoreLinq;

public class Trajectory : MonoBehaviour, IGameManager {

	public ManagerStatus status { get; private set; }

	public int id;
	[SerializeField]private float defaultBounds;

	public void Startup() {
		Debug.Log ("Trajectory Manager starting... ");
		status = ManagerStatus.Started;
	}
		
	public void AddCommand(int playerId, Vector3[] pathos){
		int last = pathos.Length - 1;
		Vector3 pointPos = pathos[last];
		Managers.cache.CacheCheck(playerId, Managers.spawn._chars[playerId].charCon.id, pointPos);
		Managers.spawn._chars[playerId].charCon.trajectories.Add (pathos);
		Managers.spawn._chars[playerId].charCon.id++;
	}

	public void ReDraw(int playerId){
		Mesh mesh = new Mesh ();
		LineRenderer liner;
		for (int i=0; i<Managers.spawn._chars[playerId].charCon.trajectories.Count; i++) {
			liner = Managers.cache._cache[i].liner;
			liner.useWorldSpace = false;
			liner.positionCount = Managers.spawn._chars[playerId].charCon.trajectories[i].Length;
			liner.SetPositions (Managers.spawn._chars[playerId].charCon.trajectories[i]);
			liner.BakeMesh (mesh, false);
			MeshCollider col = liner.gameObject.GetComponent<MeshCollider> ();
			col.sharedMesh = mesh;
		}
	}

	//Найти уточнение положения точки на линии
	public Vector3 FindPointOnLine (Vector3[] path, Vector3 point){
		int i = 0;
		int j = 0;
		int count = path.Length;
		var closestPoint = path.MinBy(t=>Vector3.SqrMagnitude(t-point));
		for(i=0; i<count; i++){
			if (path[i] == closestPoint)
				break;
		}
		if(i==0){
			j = 1;
		} else if(i==count-1){
			j = count - 2;
		} else {
			if (Vector3.Dot(point - path[i], path[i - 1] - path[i]) > 0.0f) {
				j = i - 1;
			} else if (Vector3.Dot(point - path[i], path[i + 1] - path[i]) > 0.0f) {
				j = i + 1;
			} else {
				return closestPoint;
			}
		}
		Vector3 pointA = point - path[i];
		Vector3 lineDir = (path[j] - path[i]).normalized;
		Vector3 result = path[i] + lineDir * Vector3.Dot(lineDir, pointA);
		if (Vector3.SqrMagnitude(point-path[0])<=defaultBounds*defaultBounds)
			return path[0];
		else if (Vector3.SqrMagnitude(point-path[count-1])<=defaultBounds*defaultBounds)
			return path[count-1];
		else return result;
	}
		
	public void ClearAll(int playerId){
		Managers.spawn._chars[playerId].charCon.trajectories.Clear ();
	}
}
