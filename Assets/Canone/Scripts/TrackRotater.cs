using UnityEngine;
using System.Collections;

public class TrackRotater : MonoBehaviour {
	private float rotateSpeed = 0.2f;

	void FixedUpdate () {
		if (!PlayerMover.gameEnd){
			if ((GameObject.Find ("Track").transform.rotation.z > -0.65f && GameObject.Find ("Track").transform.rotation.z < 0.65f)
				||(GameObject.Find ("Track").transform.rotation.z <= -0.65f && Input.GetAxis ("Horizontal") < 0)
				||(GameObject.Find ("Track").transform.rotation.z >= 0.65f && Input.GetAxis ("Horizontal") > 0)) {
				transform.Rotate (new Vector3(0,0,-1) * Input.GetAxis ("Horizontal") * rotateSpeed);
			}
		}
	}
}
