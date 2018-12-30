using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechComponents{
	public class Turret : MonoBehaviour {
		[SerializeField]private float rotationSpeed;
		[SerializeField]private Vector2 rotationBounds;
		[SerializeField]private float yawSpeed;
		[SerializeField]private Vector2 yawBounds;
		[SerializeField]public Gun gun;
		private float turretRot{
			get {return this.transform.localEulerAngles.y;}
			set {this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, value, this.transform.localEulerAngles.z);}
		}
		private float turretYaw{
			get {return gun.transform.localEulerAngles.y;}
			set {gun.transform.localEulerAngles = new Vector3(gun.transform.localEulerAngles.x, gun.transform.localEulerAngles.y, value);}
		}
		private Quaternion defaultRotation = Quaternion.identity;

		public IEnumerator Aim(Transform target){
			Vector3 targetPos = Vector3.Normalize(target.position - gun.transform.position);
			Vector3 gunPos = gun.transform.right;
			Quaternion q = new Quaternion ();
			yield return null;
			for(;;){
				targetPos = Vector3.Normalize(target.position - gun.transform.position);
				gunPos = gun.transform.right;
				q = Quaternion.FromToRotation(gunPos, targetPos);
				Quaternion f = gun.transform.rotation;
				Debug.Log("f "+f.eulerAngles);
//				f = Quaternion.Inverse(f);
				q = f * q;	
				Debug.Log("q "+q.eulerAngles);
				gun.transform.rotation = q;

//				this.transform.localEulerAngles = CalculateNegative(this.transform.localEulerAngles);
//				gun.transform.localEulerAngles = CalculateNegative(gun.transform.localEulerAngles);
//
//				if ((q.eulerAngles.x - this.transform.rotation.y> 0.0f)&&(turretRot<=rotationBounds.y))
//					turretRot += rotationSpeed * Time.deltaTime;
//				else if((q.eulerAngles.x - this.transform.rotation.y< 0.0f)&&(turretRot>=rotationBounds.x))
//					turretRot -= rotationSpeed * Time.deltaTime;
//				if ((q.eulerAngles.z - gun.transform.rotation.z> 0.0f)&&(turretYaw<=yawBounds.y))
//					turretYaw += yawSpeed * Time.deltaTime;
//				else if((q.eulerAngles.z - gun.transform.rotation.z< 0.0f)&&(turretYaw>=yawBounds.x))
//					turretYaw -= yawSpeed * Time.deltaTime;		
//				Debug.DrawRay(gun.transform.position, gunPos);
//				Debug.DrawRay(gun.transform.position, targetPos);
				yield return null;
			}
		}

		private Vector3 CalculateNegative(Vector3 eulerAngles){
			eulerAngles.y = (eulerAngles.y > 180) ? eulerAngles.y - 360 : eulerAngles.y;
			eulerAngles.x = (eulerAngles.x > 180) ? eulerAngles.x - 360 : eulerAngles.x;
			eulerAngles.z = (eulerAngles.z > 180) ? eulerAngles.z - 360 : eulerAngles.z;
			return eulerAngles;
		} 
	}
}