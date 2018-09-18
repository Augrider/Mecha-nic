using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour, IGameManager {

	public ManagerStatus status { get; private set; }

	public int id;

	public List<Tracers> trace;
	public bool _isChanged;
	public bool _isHighlighted;

	public void Startup() {
		Debug.Log ("Trajectory Manager starting... ");
		trace = new List<Tracers>();
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

	/*
	 * 
	 * 
	 * 
	*/
	public void AddCommand(Vector3[] path, GameObject Liner){
		Tracers Path = new Tracers ();
		Path._points = path;
		Path._liner = Liner;
		trace.Add (Path);
		Debug.Log (trace [0]._liner);
		StartCoroutine (isChange ());
	}

	public void ReDraw(){
		Mesh mesh = new Mesh ();
		foreach (Tracers tracer in trace) {
			LineRenderer liner=new LineRenderer();
			liner = tracer._liner.GetComponent<LineRenderer> ();
			liner.useWorldSpace = false;
			liner.positionCount = tracer._points.Length;
			liner.SetPositions (tracer._points);
			liner.BakeMesh (mesh, false);
			MeshCollider col = tracer._liner.GetComponent<MeshCollider> ();
			col.sharedMesh = mesh;
		}
	}

	public void Highlight (LineRenderer line, int id){
		line.useWorldSpace = false;
		line.positionCount = trace[id]._points.Length;
		line.SetPositions (trace[id]._points);
	}
		
	public void ClearAll(){
		trace.Clear ();
		trace=new List<Tracers>();
		id = 0;
	}

}
