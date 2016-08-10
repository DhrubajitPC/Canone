using UnityEngine;
using System.Collections;

public class BulletMover : MonoBehaviour {

	private float speed = 50f;
	public GameObject[] rubbleTypes = new GameObject[2];
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}

	// Update is called once per frame
	void Update () {
		this.GetComponent<Rigidbody> ().velocity = transform.parent.transform.forward * speed;
		if (this.transform.position.z - player.transform.position.z > 15) {
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter(Collider other) {

		Vector3 placement = other.gameObject.transform.position;
		string collidedItem = other.gameObject.name;
		//		Debug.Log ("Name: " + collidedItem);
		GameObject ObjParent = other.gameObject.transform.parent.gameObject;
		//		Debug.Log ("parent: " + ObjParent.transform.name);

		Destroy(other.gameObject);
		print (collidedItem);

		for (int i = 0; i < 60; i++) {
			if (GameObject.Equals (other.gameObject, ObstacleGenerator.ObsHolder [i])) {
				if (collidedItem.Contains ("flesh") || collidedItem.Contains ("FleshCube")) {
					//			Debug.Log ("I hit flesh");
					ObstacleGenerator.ObsHolder[i] = Instantiate (rubbleTypes [0], placement, Quaternion.identity) as GameObject;
					ObstacleGenerator.ObsHolder[i].transform.parent = ObjParent.transform;
					//ObstacleGenerator.ObsHolder[i].transform.position = new Vector3 (rubbleTypes [0].transform.position.x, rubbleTypes [0].transform.position.y, rubbleTypes [0].transform.position.z);
					//			Debug.Log ("object created: " + rubble.transform.name );

				} else if (collidedItem.Contains ("metal") || collidedItem.Contains ("MetalCube") || collidedItem.Contains ("turret")) {
					//			Debug.Log ("I hit metal");
					ObstacleGenerator.ObsHolder[i] = Instantiate (rubbleTypes [1], placement, Quaternion.identity) as GameObject;
					ObstacleGenerator.ObsHolder[i].transform.parent = ObjParent.transform;
					//ObstacleGenerator.ObsHolder[i].transform.position = new Vector3 (rubbleTypes [1].transform.position.x, rubbleTypes [1].transform.position.y, rubbleTypes [1].transform.position.z);
					//print (rubble);
					//			Debug.Log ("object created: " + rubble.transform.name );

				}
				break;
			}
		}
	}
}

