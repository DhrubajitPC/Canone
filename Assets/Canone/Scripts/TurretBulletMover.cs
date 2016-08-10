using UnityEngine;
using System.Collections;

public class TurretBulletMover : MonoBehaviour {

	public float speed;
	GameObject player;
	bool metalTrack;
	Vector3 pos;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		pos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		this.GetComponent<Rigidbody> ().velocity = ((player.transform.position - pos) / (player.transform.position - pos).magnitude) * speed;

		if (pos.z - player.transform.position.z > 40) {
//			print (this.transform.position.z - player.transform.position.z > 15);
			Destroy (gameObject);
		}
	}
}
