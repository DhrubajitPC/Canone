using UnityEngine;
using System.Collections;

public class TurretShooter : MonoBehaviour {

	public Transform bulletSpawn;
	public GameObject bullet;
	private GameObject player;
	bool metalTrack;
	float bulletCountDownTimer = 1;
	bool canShoot = true;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = ((transform.position - player.transform.position) / (transform.position - player.transform.position).magnitude);
		float angle = Vector3.Angle (player.transform.forward, dir);
		if (gameObject.name == "turret" && angle < 30f) {
			if (canShoot) {
				GameObject b = Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
				b.transform.parent = GameObject.Find ("Track").transform;
				b.transform.LookAt (player.transform);
				canShoot = false;
			}
			bulletCountDownTimer -= Time.deltaTime;
			if (bulletCountDownTimer < 0) {
				canShoot = true;
				bulletCountDownTimer = 1;
			}
		}
	
	}
}
