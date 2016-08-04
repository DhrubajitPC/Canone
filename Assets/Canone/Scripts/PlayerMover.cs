using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {
	private GameObject track;
	private GameObject space;
//	private int numOfLife = 3;
	private float timer;
	private string currentEnvironment = "Track";

	void Start(){
		track = GameObject.FindWithTag ("Track");
		space = GameObject.FindWithTag ("Space");
		loadTrack ();
		timer = 30;
	}

	void Update(){
		Debug.Log (currentEnvironment);
		timer -= Time.deltaTime;
		if (timer <= 0) {
			timer = 30;
			if (currentEnvironment == "Space")
				loadTrack ();
			else
				loadSpace ();
		}
	}
		
	void FixedUpdate () {
		transform.Translate (Vector3.forward * 0.1f);
	}

	void loadTrack(){
		GetComponent<Rigidbody> ().useGravity = true;
		currentEnvironment = "Track";
		transform.position = new Vector3 (transform.position.x, 1.0f, transform.position.z);
		GameObject[] trackSegments = GameObject.FindGameObjectsWithTag ("TrackSegment");
		float z = 0;
		foreach (GameObject trackSegment in trackSegments){
			trackSegment.transform.position = new Vector3 (trackSegment.transform.position.x, trackSegment.transform.position.y, z);
			z += 30;
		}
		track.transform.position = new Vector3 (transform.position.x, this.transform.position.y + 11.0f, transform.position.z);
		space.transform.position = new Vector3 (transform.position.x, -200f, transform.position.z);

	}
	void loadSpace(){
		currentEnvironment = "Space";
		space.transform.position = new Vector3 (transform.position.x, transform.position.y + 11.0f, transform.position.z);
		track.transform.position = new Vector3 (transform.position.x, -200f, transform.position.z);
		GetComponent<Rigidbody> ().useGravity = false;
	}

}
