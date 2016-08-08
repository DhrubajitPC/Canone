using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraMover : MonoBehaviour {
	private GameObject player;


	void Start () {
		player = GameObject.FindWithTag("Player");
	}

	void Update () {
		transform.position = new Vector3 (player.transform.position.x, 
			player.transform.position.y + 1.0f,
			player.transform.position.z - 2.5f);
	} 

	public void restartGame(){
		SceneManager.LoadScene("TrackScene");
	}
}
