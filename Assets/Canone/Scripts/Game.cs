using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	public Text timer;
	private Text score;
	private float startTime;
	private int t;

	private GameObject clicker;
	private GameObject replay;
	private GameObject menu;

	private int intialTime = 10;
	private int cnt = 0;

	// Use this for initialization

	void Start(){
		Screen.orientation = ScreenOrientation.Landscape;

		clicker = GameObject.Find ("ClickerButton");
		replay = GameObject.Find ("ReplayButton");
		menu = GameObject.Find ("MenuButton");
		replay.SetActive(false);
		menu.SetActive (false);

		score = this.transform.gameObject.GetComponent<Text>();
		startTime = Time.time;

	}

	void FixedUpdate(){
		score.text = cnt.ToString ();
		t = (int)(Time.time - startTime);
		if (intialTime - t > -1) {
			timer.text = (intialTime - t).ToString ();
		} else {
			clicker.SetActive(false);
			replay.SetActive(true);
			menu.SetActive (true);
			PlayerPrefs.SetInt ("Score", cnt);
		}
	}

	public void updateScore(){
		if (timer.text != "0") {
			cnt += 1;
		}
	}

	public void restart(){
		SceneManager.LoadScene ("GameScene");
	}

	public void menuScene(){
		SceneManager.LoadScene ("MenuScene");
	}
}
