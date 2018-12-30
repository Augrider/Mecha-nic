using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechComponents{
	public class Gun : MonoBehaviour {
		[SerializeField]private GameObject muzzle;
		[SerializeField]private GameObject breech;
		[SerializeField]private GameObject bullet;
		[SerializeField]private Vector3 recoilPlace;
		[SerializeField]private float recoil;
		[SerializeField]private float derivation;

		public IEnumerator Shoot(Transform target){
			for (;;){
				if (ReturnDirection()==Vector3.Normalize(target.position-breech.transform.position)){
					Instantiate(bullet, muzzle.transform.position, Derivate());
					yield return StartCoroutine(RecoilCycle());
				}
				yield return null;
			}
		}

		private Quaternion Derivate(){
			Quaternion origin = muzzle.transform.rotation;
			Vector3 euler = origin.eulerAngles;

			euler.x = euler.x + Random.Range(-derivation, derivation);
			euler.y = euler.y + Random.Range(-derivation, derivation);
			euler.z = euler.z + Random.Range(-derivation, derivation);
			origin.eulerAngles = euler;
			return origin;
		}

		private IEnumerator RecoilCycle(){
			float i = 0.0f;
			Vector3 origin = muzzle.transform.localPosition;
			while (i<=1.0f){
				i += recoil * Time.deltaTime;
				muzzle.transform.localPosition = Vector3.Lerp(origin, recoilPlace, i);
				yield return null;
			}
			i = 0.0f;
			while (i<=1.0f){
				i += recoil * Time.deltaTime;
				muzzle.transform.localPosition = Vector3.Lerp(recoilPlace, origin, i);
				yield return null;
			}
			yield return null;
		}

		public Vector3 ReturnDirection(){
			return Vector3.Normalize(muzzle.transform.position - breech.transform.position);
		}

		public Vector3 ReturnUpDirection(){
			return Vector3.Normalize(breech.transform.localPosition);
		}
	}
}
