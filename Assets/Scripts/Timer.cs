using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour {

	[SerializeField] private Text timer;
	[SerializeField] private Text timerTotal;
	[SerializeField] private Text Turn;

	public bool isActive;
	public float tim;
	public float tim2;
	private int turn;
	// Use this for initialization
	void Start () {
		isActive = false;
		tim = 0;
		tim2 = 0;
		turn = 1;
	}

	void Awake () {
		Messenger.AddListener (GameEvent.Start_Turn, TimerStart);
	}
		
	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.Start_Turn, TimerStart);
	}

	// Update is called once per frame
	void Update () {
		if (isActive) {
			tim += Time.deltaTime;
			tim2 += Time.deltaTime;
			timer.text = tim.ToString ();
			timerTotal.text = tim2.ToString ();
			if (tim >= 5.0f) {
				Messenger.Broadcast (GameEvent.End_Turn);
				tim = 0;
				timer.text = tim.ToString () + ".00";
				tim2 = 5.0f * turn;
				timerTotal.text = tim2.ToString () + ".00";
				isActive = false;
				turn += 1;
				Turn.text = "Turn " + turn.ToString ();
				Managers.movement.ClearAll();
			}
		}
	}

	void TimerStart() {
		isActive = true;
	}
		
}
