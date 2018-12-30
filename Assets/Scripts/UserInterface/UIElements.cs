using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIElements : MonoBehaviour {

	[SerializeField]private Vector2 resetPlace;
	[SerializeField]private float radius;
	[SerializeField]private Vector2 placement;
	[SerializeField]private float speed;
	[SerializeField]private Color uiColor;

	[SerializeField]private RectTransform timeWindow;
	[SerializeField]private RectTransform startButton;
	[SerializeField]private RectTransform messageWindow;
	[SerializeField]private Text messageText;
	[SerializeField]private RectTransform wheel;

	public Button[] buttons;

	// Use this for initialization
	public void MoveUp(){
		startButton.gameObject.SetActive(false);
		WheelReset();
		wheel.gameObject.SetActive(false);
		StartCoroutine(Up());
	}

	public void MoveDown(){
		startButton.gameObject.SetActive(true);
		wheel.gameObject.SetActive(true);
		StartCoroutine(Down());
	}


	public void WheelSet(Vector3 place){
		WheelSort();
		wheel.anchoredPosition3D = place;
	}

	public void WheelReset(){
		wheel.anchoredPosition = resetPlace;
	}

	private void WheelSort(){
		int count = buttons.Length;
		int place = 0;
		foreach (Button button in buttons) {
			if (button.IsActive()){
				button.transform.localPosition = new Vector3 (radius * Mathf.Sin(place * 2 * Mathf.PI / count), radius * Mathf.Cos(place * 2 * Mathf.PI / count), 0);
				place++;
			}
		}
	}


	private IEnumerator Down(){
		float dt = speed;
		while (timeWindow.anchoredPosition.y > 0.01f){
			dt += speed;
			timeWindow.anchoredPosition = Vector2.Lerp(timeWindow.anchoredPosition, Vector2.zero, dt*Time.deltaTime);
			yield return null;
		}
		timeWindow.anchoredPosition = Vector2.zero;
		yield return null;
	}

	private IEnumerator Up(){
		float dt = speed;
		while (timeWindow.anchoredPosition.y < placement.y-0.01f){
			dt += speed;
			timeWindow.anchoredPosition = Vector2.Lerp(timeWindow.anchoredPosition, placement, dt*Time.deltaTime);
			yield return null;
		}
		timeWindow.anchoredPosition = placement;
		yield return null;
	}


	public IEnumerator MessageDisplay(string text){
		messageText.text = text;
		messageWindow.position = new Vector3 (Screen.width / 2, Screen.height * 3/4, 0);
		messageWindow.sizeDelta = new Vector2 ((Screen.width - 400)/0.3f, Screen.height / 2);
		yield return new WaitForSeconds (1.0f);
		Image image = messageWindow.GetComponent<Image>();
		while (image.color.a>0.05f){
			image.color = Color.Lerp(image.color, Color.clear, Mathf.SmoothStep(0f, 2f, 0.1f));
			messageText.color = Color.Lerp(image.color, Color.clear, Mathf.SmoothStep(0f, 2f, 0.1f));
			yield return null;
		}
		messageWindow.position = new Vector3 (Screen.width / 2, -Screen.height, 0);
		image.color = uiColor;
		messageText.color = Color.white;
		yield return null;
	}
}
