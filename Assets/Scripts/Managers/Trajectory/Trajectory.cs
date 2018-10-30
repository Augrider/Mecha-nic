using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour, IGameManager {

	public ManagerStatus status { get; private set; }

	public int id;

	public List<Vector3[]> trajectories;
	public bool _isChanged;
	public bool _isHighlighted;

	public void Startup() {
		Debug.Log ("Trajectory Manager starting... ");
		trajectories = new List<Vector3[]>();
		_isChanged = false;
		_isHighlighted = false;
		id = 0;
		status = ManagerStatus.Started;
	}
		
	private IEnumerator isChange (){
		_isChanged = true;
		yield return new WaitForSeconds(0.5f);
		_isChanged = false;
		yield break;
	}
		
	public void AddCommand(Vector3[] pathos){
		int last = pathos.Length - 1;
		Vector3 pointPos = new Vector3 (pathos [last].x, pathos [last].y + 3.0f, pathos [last].z);
		Managers.cache.CacheCheck(id, pointPos);
		trajectories.Add (pathos);
		id++;
		StartCoroutine (isChange ());
	}

	public void ReDraw(){
		Mesh mesh = new Mesh ();
		LineRenderer liner;
		for (int i=0; i<trajectories.Count; i++) {
			liner = Managers.cache._cache[i].liner;
			liner.useWorldSpace = false;
			liner.positionCount = trajectories[i].Length;
			liner.SetPositions (trajectories[i]);
			liner.BakeMesh (mesh, false);
			MeshCollider col = liner.gameObject.GetComponent<MeshCollider> ();
			col.sharedMesh = mesh;
		}
	}

	//Найти уточнение положения
	public Vector3 FindPoint (int id, Vector3 point){
		int i = 0;
		int count = trajectories[id].Length;
		for (int k=0; k<count-1; k++) {
			if ((((point.x - trajectories[id][i].x) / Mathf.Abs(point.x - trajectories[id][i].x)) * ((point.x - trajectories[id][i + 1].x) / Mathf.Abs (point.x - trajectories[id][i + 1].x)))>0.0) {
				i++;
			}
		}
		if (trajectories[id][i].x < trajectories[id][i + 1].x) {
			float otn = (point.x - trajectories[id][i].x) / (trajectories[id][i + 1].x - trajectories[id][i].x);
			return new Vector3 (point.x, trajectories[id][i].y + (trajectories[id][i + 1].y - trajectories[id][i].y) * otn, trajectories[id][i].z + (trajectories[id][i + 1].z - trajectories[id][i].z) * otn);
		} else {
			float otn = (point.x - trajectories[id][i + 1].x) / (trajectories[id][i].x - trajectories[id][i + 1].x);
			return new Vector3 (point.x, trajectories[id][i + 1].y + (trajectories[id][i].y - trajectories[id][i + 1].y) * otn, trajectories[id][i + 1].z + (trajectories[id][i].z - trajectories[id][i + 1].z) * otn);
		}
	}

	public void Highlight (LineRenderer line, int id){
		line.useWorldSpace = false;
		line.positionCount = trajectories[id].Length;
		line.SetPositions (trajectories[id]);
	}
		
	public void ClearAll(){
		trajectories.Clear ();
		id = 0;
	}

	public void DrawCurrent(Vector3[] points, LineRenderer line){
		line.useWorldSpace = false;
		line.positionCount = points.Length;
		line.SetPositions (points);
	}
}
