using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldElements : MonoBehaviour {

	[SerializeField]private LineRenderer _line;
	[SerializeField]private MeshRenderer _pointer;

	// Use this for initialization
	void Start () {
		_line.useWorldSpace = false;
		_pointer.enabled = false;
	}


	public void DrawLine(Vector3[] points, float height, Color color){
		_line.transform.position = new Vector3 (0.0f, height, 0.0f);

		_line.startColor = color;
		_line.endColor = color;

		int count = points.Length;
		_line.positionCount = count;
		_line.SetPositions(points);
	}

	public void Erase(){
		_line.positionCount = 0;
	}

	public IEnumerator DrawLast(int playerId, Color color){
		Managers.trajectory.ReDraw (playerId);
		int last = Managers.spawn._chars[playerId].charCon.trajectories.Count-1;
		DrawLine(Managers.spawn._chars[playerId].charCon.trajectories[last], 0.0f, color);
		yield return new WaitForSeconds (1.5f);
		Erase();
	}


	public void PointerReset(){
		_pointer.enabled = false;
	}

	public void PointerSet(Vector3 place, float add){
		_pointer.transform.position = new Vector3(place.x, place.y + add, place.z);
		_pointer.enabled = true;
	}

	public void MakeCol(){
		Mesh mesh = new Mesh ();
		_line.BakeMesh (mesh, false);
		MeshCollider col = _line.gameObject.GetComponent<MeshCollider> ();
		col.sharedMesh = mesh;
	}

	public void RemoveCol(){
		MeshCollider col = _line.gameObject.GetComponent<MeshCollider> ();
		col.sharedMesh = null;
	}
}
