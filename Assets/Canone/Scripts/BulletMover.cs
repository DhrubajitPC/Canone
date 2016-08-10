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
        Quaternion rotation = other.gameObject.transform.rotation;
		string collidedItem = other.gameObject.name;
		//		Debug.Log ("Name: " + collidedItem);
		GameObject ObjParent = other.gameObject.transform.parent.gameObject;
		//		Debug.Log ("parent: " + ObjParent.transform.name);

		Destroy(other.gameObject);
		print (collidedItem);
        GameObject explosion_smoke = (GameObject)Instantiate(Resources.Load("prefabs/ExplosionSmoke"), placement, rotation);
        Destroy(explosion_smoke, 1.0f);

        for (int i = 0; i < 60; i++) {
			if (GameObject.Equals (other.gameObject, ObstacleGenerator.ObsHolder [i])) {
				if (collidedItem.Contains ("flesh") || collidedItem.Contains ("FleshCube") || collidedItem.Contains("turret_flesh")) {
                    GameObject explosion = (GameObject)Instantiate(Resources.Load("prefabs/ExplosionFlesh"), placement, rotation);
					GameObject.Find ("DestructionSoundManager").GetComponents<AudioSource> () [0].Play();
                    Destroy(explosion, 1.0f);
                    ObstacleGenerator.ObsHolder[i] = Instantiate (rubbleTypes [0], placement, rotation) as GameObject;
				} else if (collidedItem.Contains ("metal") || collidedItem.Contains ("MetalCube") || collidedItem.Contains ("turret_metal")) {
                    GameObject explosion = (GameObject)Instantiate(Resources.Load("prefabs/ExplosionMetal"), placement, rotation);
					GameObject.Find ("DestructionSoundManager").GetComponents<AudioSource> () [1].Play();
                    Destroy(explosion, 1.0f);
                    ObstacleGenerator.ObsHolder[i] = Instantiate (rubbleTypes [1], placement, rotation) as GameObject;
				}
                ObstacleGenerator.ObsHolder[i].transform.parent = ObjParent.transform;
                if (collidedItem.Contains("Cube"))
                {
                    ObstacleGenerator.ObsHolder[i].transform.Translate(0f, 1.5f, 0f);
                }
				break;
			}
		}
	}
}

