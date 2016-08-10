using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GhostBehaviour : MonoBehaviour {
	public float timer;
	private float cooling= 0f;
	public bool on;
	public GameObject TimeLeftDisplay;
	private bool firstTime = true;

	public void TurnOnInvisibleMode(){
		if (firstTime || cooling >= 10) {
			if (firstTime) {firstTime = false;}
			on = true;
			timer = 3f;
			cooling = 0f;
		}
	}
	
	// Update is called once per frame
	public float UpdateTimeLeft (float deltaTime) {
		if (on){
			if (timer <= 0) {
				TimeLeftDisplay.GetComponent<Text> ().text = "";
				TimeLeftDisplay.SetActive (false);
				on = false;
			}  else {
				TimeLeftDisplay.SetActive (true);
				timer -= deltaTime;
				TimeLeftDisplay.GetComponent<Text>().text = "Time Left : "+timer;
			}
		}else {
			if(!firstTime){cooling += deltaTime;}
		}
		return timer;
	}
}
