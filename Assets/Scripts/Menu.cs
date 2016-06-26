using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour {

	private Text oldScore;
	// Use this for initialization
	void Start () {
		oldScore = GameObject.Find ("score").GetComponent<Text> ();
		oldScore.text = "Last Score:" + PlayerPrefs.GetInt ("Score");

	}

	public void loadGame(){
		SceneManager.LoadScene ("GameScene");
	}
}
