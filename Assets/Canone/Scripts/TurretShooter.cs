using UnityEngine;
using System.Collections;

public class TurretShooter : MonoBehaviour {

	public Transform bulletSpawn;
	public GameObject bullet;
	private GameObject player;
	bool metalTrack;
	float bulletCountDownTimer = 0.25f;
	bool canShoot = true;
	Vector3 pos;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float dist = (transform.position - player.transform.position).magnitude;
		Vector3 dir = ((transform.position - player.transform.position) / dist);
		float angle = Vector3.Angle (player.transform.forward, dir);
		if (dist > 4) {
			transform.LookAt (player.transform);
		}
		print (gameObject.name);
		if (gameObject.name == "turret_metal(Clone)" && angle < 30f && dist < 30) {
			if (canShoot) {
				GameObject b = Instantiate (bullet, bulletSpawn.position, transform.rotation) as GameObject;
				b.transform.parent = GameObject.Find ("Track").transform;
//				b.transform.LookAt (player.transform);
				canShoot = false;
			}
			bulletCountDownTimer -= Time.deltaTime;
			if (bulletCountDownTimer < 0) {
				canShoot = true;
				bulletCountDownTimer = 0.25f;
			}
		} else if (gameObject.name == "turret_flesh(Clone)" && angle < 100 && dist < 25) {
			GameObject b = Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
			b.transform.parent = GameObject.Find ("Track").transform;
			b.transform.LookAt (player.transform);
		}
	
	}
}
