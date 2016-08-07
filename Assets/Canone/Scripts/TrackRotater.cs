using UnityEngine;
using System.Collections;

public class TrackRotater : MonoBehaviour {
	private float rotateSpeed = 0.2f;

	void FixedUpdate () {
		if (!PlayerMover.gameEnd){
			transform.Rotate (new Vector3(0,0,-1) * Input.GetAxis ("Horizontal") * rotateSpeed);
		}
	}
}
