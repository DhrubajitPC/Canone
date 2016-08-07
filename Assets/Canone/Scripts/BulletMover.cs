using UnityEngine;
using System.Collections;

public class BulletMover : MonoBehaviour {

	public float speed = 20f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Rigidbody> ().velocity = transform.forward * speed;
		
	}
}
