using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMove : MonoBehaviour {
	[SerializeField]private float place;
	[SerializeField]private float speed;
	private float range;

	void Awake () {
		Messenger.AddListener (GameEvent.Start_Turn, MoveUp);
		Messenger.AddListener (GameEvent.End_Turn, MoveDown);
	}

	void OnDestroy () {
		Messenger.RemoveListener (GameEvent.Start_Turn, MoveUp);
		Messenger.RemoveListener (GameEvent.End_Turn, MoveDown);
	}

	private void MoveUp(){
		StartCoroutine(Up());
	}

	private void MoveDown(){
		StartCoroutine(Down());
	}

	private IEnumerator Down(){
		range = place;
		while (range > 0)
		{
			this.transform.Translate(0, -speed * Time.deltaTime, 0);
			range -= speed * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		yield return null;
	}

	private IEnumerator Up(){
		range = 0;
		while (range < place){
			this.transform.Translate(0, speed * Time.deltaTime, 0);
			range += speed * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		yield return null;
	}
}
