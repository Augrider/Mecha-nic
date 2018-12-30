using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMethod : MonoBehaviour {
	public virtual void Assign(){}
	public virtual void Realization(){}
	public virtual IEnumerator BuildCommand(){yield return null;}
}
