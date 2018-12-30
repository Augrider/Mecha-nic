using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
	[SerializeField]private Vector3 gravity = Physics.gravity;
	[SerializeField]private LayerMask mask;
	[SerializeField]private int pointsPerSecond;
	[SerializeField]private LayerMask _mask;
	[SerializeField]private int maxArraySize;


	public void Assign(int playerId, Vector3[] path, Vector3 speed){
		int count = path.Length - 1;
		for(int i=0; i<count+1; i++){
			path[i].y -= Managers.spawn._chars[playerId].charCon.height;
		}
		Managers.movement.AddCommand (playerId, "Jump",speed, path[count]);
		Managers.spawn._chars[playerId].charCon.pathfinder.transform.position = path[count];
		Managers.spawn._chars[playerId].charCon.pathfinder.transform.Translate(0.0f, -Managers.spawn._chars[playerId].charCon.height, 0.0f);
		for (int i = 0; i < count + 1; i++)
			path[i].y += Managers.spawn._chars[playerId].charCon.height;
		Managers.trajectory.AddCommand (playerId, path);
	}

	public void Realization(int playerId, int id){
		
		Managers.spawn._chars[playerId].charCon.rbody.velocity = Managers.spawn._chars[playerId].charCon._moves[id]._place;
	}

	public IEnumerator BuildCommand(int playerId){
		Vector3[] path=new Vector3[maxArraySize];
		Vector3 speed = Vector3.up;
		Vector3 range=Vector3.zero;
		Vector3 point = new Vector3 ();
		Vector3 startPoint = Managers.spawn._chars[playerId].charCon.pathfinder.transform.position + new Vector3 (0.0f, Managers.spawn._chars[playerId].charCon.height, 0.0f);
		float size = 1.0f;
		int clickCount = 0;
		Managers.spawn._chars[playerId].charCon.pathfinder.enabled = false;
		yield return null;
		StartCoroutine(Controllers.elements.MessageDisplay("Выберите угол прыжка"));
		while (!Input.GetMouseButton(1)) {			
			if (Input.GetAxis("Mouse X") != 0.0 || Input.GetAxis("Mouse Y") != 0.0) {
				Vector3 MPos = MouseController.MPos;

				switch (clickCount) {
					case 0:
						range = MPos - startPoint;
						range.y = 0.0f;
						Vector3 speed1 = JumpSpeed(playerId, range, size, out size);
						if (speed1!=Vector3.down){
							speed = speed1;
							path = JumpTrajectory(playerId, speed, startPoint);
							if (path.Length != 0) {
								Controllers.wElements.DrawLine(path, 0.0f, Color.cyan);
								Controllers.wElements.PointerSet(path[path.Length - 1], 0.0f);
							}
						}
						break;

					case 1:
						speed1 = JumpSpeed(playerId, range, size + (Input.GetAxis("Mouse X") + Input.GetAxis("Mouse Y"))*0.01f, out size);
						if (speed1!=Vector3.down){
							speed = speed1;
							path = JumpTrajectory(playerId, speed, startPoint);
							if (path.Length != 0) {
								Controllers.wElements.DrawLine(path, 0.0f, Color.cyan);
								Controllers.wElements.PointerSet(path[path.Length - 1], 0.0f);
							}
						}		
						break;

					case 2:
						point = path[path.Length - 1];
						RaycastHit Hit = new RaycastHit ();
						Vector3 RayPos = new Vector3 (MPos.x, MPos.y, MPos.z - 3.0f);
						if (Physics.SphereCast(RayPos, 0.5f, Vector3.forward, out Hit, 5.0f, _mask)) {
							point = Managers.trajectory.FindPointOnLine(path, Hit.point);
						}
						if (point!=Vector3.down){			
							Controllers.wElements.PointerSet(point, 0.0f);
						}
						break;
				}
			}
			if(Input.GetMouseButtonDown(0)){
				switch(clickCount){
					case 0:
						StartCoroutine(Controllers.elements.MessageDisplay("Выберите дальность прыжка"));
						clickCount++;
						break;
					case 1:
						StartCoroutine(Controllers.elements.MessageDisplay("Выберите конечную точку прыжка"));
						clickCount++;
						Controllers.wElements.MakeCol();
						break;
					case 2:
						path = CalcForPoint(path, point);
						Assign(playerId, path, speed);
						StartCoroutine(Controllers.wElements.DrawLast(playerId, Color.green));
						clickCount++;
						break;								
				}
				yield return null;
			}
			if (clickCount == 3)
				break;
			yield return null;
		}
		if(Input.GetMouseButton(1))
			Controllers.wElements.Erase();
		Controllers.wElements.RemoveCol();
		Controllers.wElements.PointerReset();
		Controllers.mouse.stateBasic();
		yield return null;
	}


	public Vector3[] JumpTrajectory(int playerId, Vector3 speed, Vector3 startPoint){
		List<Vector3> path = new List<Vector3> ();
		Vector3 start = startPoint;
		Vector3 end=startPoint + (speed + gravity * (1.0f/pointsPerSecond)/2.0f) * (1.0f/pointsPerSecond);
		int i = 1;
		path.Add(start);
		while(Try(playerId, start, end, out end)&&(i<maxArraySize)){
			path.Add(end);
			i++;
			start = end;
			end = startPoint + (speed + gravity * (1.0f/pointsPerSecond)*i/2.0f) * (1.0f/pointsPerSecond)*i;
		}
		path.Add(end);
		return path.ToArray();
	}

	private Vector3 JumpSpeed(int playerId, Vector3 range, float size, out float size1){
		Vector3 result;
		float startSpeed;
		size1 = PlaceInBounds(size);
		startSpeed = Managers.spawn._chars[playerId].charCon.maxSpeed * size1;
		float sqrt = Mathf.Pow(startSpeed, 4) - Mathf.Pow(gravity.y, 2) * Vector3.SqrMagnitude(range);
		if (sqrt>=0.0f){
			float aim = (Mathf.Pow(startSpeed, 2) + Mathf.Sqrt(sqrt))/(-gravity.y * Vector3.Magnitude(range));
			aim = Mathf.Atan(aim);
			float aim2=Vector3.Angle(Vector3.right, range);
			result = startSpeed * new Vector3 (Mathf.Cos(aim) * Mathf.Cos(aim2/180*Mathf.PI), Mathf.Sin(aim), Mathf.Cos(aim) * Mathf.Sin(aim2/180*Mathf.PI));
			float heightMax = FindHeight(result);
			result.y = Mathf.Sqrt(2.0f * gravity.y * heightMax*size1);
			Debug.DrawLine(Managers.spawn._chars[playerId].charCon.pathfinder.transform.position, Managers.spawn._chars[playerId].charCon.pathfinder.transform.position + result);
			return result;
		} else {
			return Vector3.down;
		}
	}

	private bool Try(int playerId, Vector3 start, Vector3 end, out Vector3 point){ 
		RaycastHit hit;
		if(Physics.SphereCast(start, Managers.spawn._chars[playerId].charCon.height, Vector3.ClampMagnitude(end - start, 1), out hit, Vector3.Magnitude(end - start), mask)){
			point = start + Vector3.ClampMagnitude(end - start, hit.distance);
			return false;
		}
		point = end;
		return true;
	}

	private Vector3[] CalcForPoint(Vector3[] path, Vector3 point){
		int i = path.Length;
		for(int j=0; j<i-2; j++){
			if(((point.x - path[j].x) * (point.x - path[j + 1].x)) < 0.0f){
				Vector3[] result = new Vector3[j + 2];
				for (int k = 0; k < j + 1; k++)
					result[k] = path[k];
				result[j + 1] = path[j] + Vector3.ClampMagnitude(path[j + 1] - path[j], Vector3.Distance(point, path[j]));
				return result;
			}
		}
		return path;
	}


	private float FindTime(Vector3 range, Vector3 speed){
		return range.x / speed.x;
	}

	private float FindHeight(Vector3 speed){
		return Mathf.Pow(speed.y, 2.0f) / (2.0f * gravity.y);
	}

	private float PlaceInBounds(float input){
		float size1;
		if ((input >= 0.1f) && (input <= 1.0f)){
			size1 = input;
		}			
		else if (input <= 0.1f){
			size1 = 0.1f;
		}
		else{
			size1 = 1.0f;
		}
		return size1;
	}
}
