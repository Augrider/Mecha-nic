using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBake : MonoBehaviour {
	public NavMeshSurface[] surfaces;

	void OnEnable(){
		for (int i=0; i<surfaces.Length; i++)
		{
			surfaces[i].BuildNavMesh();
		}
	}
}
