using UnityEngine;
using System.Collections;

public class TurretBulletMover : MonoBehaviour {

	float speed;
	GameObject player;
	bool metalTrack;
	Vector3 pos;
	float shootingRange;
	float distToPlayer;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		pos = transform.position;
	}
//	BulletTurretFlesh(Clone);
//	BulletTurret(Clone);
	// Update is called once per frame
	void Update () {
//		print (gameObject.name);
		distToPlayer = (player.transform.position - transform.position).magnitude;
//		print (distToPlayer);
		if (distToPlayer < 10) {
//			gameObject.GetComponent<BoxCollider> ().enabled = true;
		}
		if (gameObject.name == "BulletTurret(Clone)") {
			speed = 20;
			shootingRange = 30;
		} else if (gameObject.name == "BulletTurretFlesh(Clone)") {
			speed = 5;
			shootingRange = 15;
		}
		this.GetComponent<Rigidbody> ().velocity = ((player.transform.position - pos) / (player.transform.position - pos).magnitude) * speed;
		if (pos.z - transform.position.z > shootingRange) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.transform.parent.parent) {
			if (gameObject.name == "BulletTurret(Clone)" && other.transform.parent.parent.name == "Player") {
				Destroy (gameObject);
				PlayerMover.gameEnd = true;
			}
		}
	}

	void OnTriggerStay(Collider other){
		if (other.transform.parent.parent) {
			if (gameObject.name == "BulletTurretFlesh(Clone)" && other.transform.parent.parent.name == "Player") {
				DisableControl ();
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.transform.parent.parent) {
			if (gameObject.name == "BulletTurretFlesh(Clone)" && other.transform.parent.parent.name == "Player") {
				Destroy (gameObject);
				EnableControl ();
			}
		}
	}

	void DisableControl(){
		GameObject.Find ("Track").GetComponent<TrackRotater> ().enabled = false;
	}
	void EnableControl(){
		GameObject.Find ("Track").GetComponent<TrackRotater> ().enabled = true;
	}
}

