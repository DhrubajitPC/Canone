using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {
	private GameObject track;
	private GameObject space;
	private int numOfLife = 3;
	private float timer;
	private string currentEnvironment = "Track";

	void start(){
		track = GameObject.Find ("Track");
		space = GameObject.Find ("Space");
		timer = 60;
	}

	void Update(){
		timer -= Time.deltaTime;
//		if (timer <= 0) {
//			timer = 60;
//			if (currentEnvironment == "Track")
//				loadSpace ();
//			else
//				loadTrack ();
//		}
	}
		
	void FixedUpdate () {
		transform.Translate (Vector3.forward * 0.1f);
	}

	void loadTrack(){
		currentEnvironment = "Track";
		GetComponent<Rigidbody> ().useGravity = true;

	}
	void loadSpace(){
		currentEnvironment = "Space";
		GetComponent<Rigidbody> ().useGravity = false;
	}

}
