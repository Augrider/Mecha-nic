using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Cached{
	public LineRenderer liner;
	public MeshRenderer point;
	public Pointer _linerPointer {get{return liner.GetComponent<Pointer>();}}
	public Pointer _pointPointer {get{return point.GetComponent<Pointer>();}}
}
