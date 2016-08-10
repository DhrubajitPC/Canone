using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraMover : MonoBehaviour {
	private GameObject player;
	private	float learning = 0.05f;

	void Start () {
		player = GameObject.FindWithTag("Player");
	}

	void Update () {
		transform.position = new Vector3 (player.transform.position.x, 
			player.transform.position.y + 1.0f,
			player.transform.position.z - 2.5f);
//		transform.rotation = learning * player.transform.rotation  + (1 - learning) * transform.rotation;
//		float rotateFactor = relativeRotation(transform.rotation.eulerAngles.z, player.transform.rotation.eulerAngles.z);
//		transform.Rotate (0, 0, -(learning * rotateFactor));
//		print (rotateFactor);
//		transform.Ro
	} 

	//returns the relative rotation in terms of -180 and 180 from init
	float relativeRotation(float rot, float init)
	{
		float res = (rot - init) % 360;
		if (res > 180)
		{
			return (res - 360);
		}
		return res;
	}

	public void restartGame(){
		GameObject.Find ("Player").GetComponent<PlayerMover> ().restart ();
		GameObject.Find ("Track").GetComponent<PlayerMover> ().restart ();
		SceneManager.LoadScene("TrackScene");
	}
}
