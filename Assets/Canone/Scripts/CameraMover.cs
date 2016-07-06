using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {
	private GameObject player;

	void Start () {
		player = GameObject.FindWithTag("Player");
	}

	void Update () {
		transform.position = new Vector3 (player.transform.position.x, 
			player.transform.position.y + 0.8f,
			player.transform.position.z - 1.6f);
	}
}
