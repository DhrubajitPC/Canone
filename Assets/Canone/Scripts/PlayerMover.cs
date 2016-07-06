using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {

	public float playerMoveSpeed;
	public float leftBoundary;
	public float rightBoundary;

	void FixedUpdate () {
		transform.Translate (Vector3.forward * 0.2f);
		float currentX = transform.position.x;
		transform.position = new Vector3 ( Mathf.Clamp(currentX + Input.acceleration.x * playerMoveSpeed, 
			leftBoundary, rightBoundary),
			transform.position.y, transform.position.z);
	}

}
