using UnityEngine;
using System.Collections;

public class BulletMover : MonoBehaviour {

	public float speed = 20f;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Rigidbody> ().velocity = transform.forward * speed;
		if (this.transform.position.z - player.transform.position.z > 15) {
			print (this.transform.position.z - player.transform.position.z > 15);
			Destroy (gameObject);
		}
	}
}
