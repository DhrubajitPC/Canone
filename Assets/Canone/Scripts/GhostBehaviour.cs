using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GhostBehaviour : MonoBehaviour {
	public float timer;
	public bool on;
	public GameObject TimeLeftDisplay;

	public void TurnOnInvisibleMode(){
		on = true;
		timer = 5f;
	}
	
	// Update is called once per frame
	public float UpdateTimeLeft (float deltaTime) {
		if (on){
			if (timer <= 0) {
				TimeLeftDisplay.GetComponent<Text> ().text = "";
				TimeLeftDisplay.SetActive (false);
				on = false;
			} else {
				TimeLeftDisplay.SetActive (true);
				timer -= deltaTime;
				TimeLeftDisplay.GetComponent<Text>().text = "Time Left : "+timer;
			}
		}
		return timer;
	}
}
