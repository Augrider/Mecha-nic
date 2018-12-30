using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechComponents{
	public class Bullet : MonoBehaviour {
		[SerializeField]private float speed;
		[SerializeField]private float damage;
		[SerializeField]private float maxDistance;
		private float distance;

		// Use this for initialization
		void Start () {
				distance = 0.0f;
		}
		
		// Update is called once per frame
		void Update () {
			if (distance<=maxDistance){
				this.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
				distance += speed * Time.deltaTime;
			} else {
				Destroy(this.gameObject);
			}
		}
	}
}
