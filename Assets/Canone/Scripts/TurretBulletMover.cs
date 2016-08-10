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
//		print (transform.name);
	}
	// Update is called once per frame
	void Update () {
		distToPlayer = (player.transform.position - transform.position).magnitude;
		if (distToPlayer < 10) {
		}
		if (gameObject.name == "BulletTurret(Clone)") {
			speed = 30;
			shootingRange = 30;
		} else if (gameObject.name == "BulletTurretFlesh(Clone)") {
			speed = 25;
			shootingRange = 20;
		}
//		print (player.transform.rotation.x);
//		this.GetComponent<Rigidbody> ().velocity = transform.forward * speed;
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

