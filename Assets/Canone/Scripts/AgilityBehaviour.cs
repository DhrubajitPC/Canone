using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AgilityBehaviour : MonoBehaviour {
	public float timer;
	private float cooling = 0f;
	public bool on;
	public GameObject TimeLeftDisplay;
	private bool firstTime = true;

	public void TurnOnAgileMode(){
		if (firstTime || cooling >= 10) {
			if (firstTime) {firstTime = false;}
			on = true;
			timer = 3f;
			cooling = 0f;
		}
	}

	public void cancel(){
		timer = 0f;
		TimeLeftDisplay.GetComponent<UIBarScript> ().UpdateValue (0,3);
		TimeLeftDisplay.SetActive (false);
		on = false;
	}

	public float UpdateTimeLeft (float deltaTime) {
		if (on) {
			if (timer <= 0) {
				TimeLeftDisplay.GetComponent<UIBarScript> ().UpdateValue (0,3);
				TimeLeftDisplay.SetActive (false);
				on = false;
			}  else {
				TimeLeftDisplay.SetActive (true);
				timer -= deltaTime;
				TimeLeftDisplay.GetComponent<UIBarScript> ().UpdateValue ((int)timer*10,30);
			}
		}  else {
			if(!firstTime){cooling += deltaTime;}
		}
		return timer;
	}

}
