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
		metalTrack = GameObject.Find ("Track").GetComponent<TrackRotater> ().metalTrack;
		print ("metalTrack:"+metalTrack);
		if (gameObject.name == "turret" && metalTrack) {
			print (1);
			if (canShoot) {
				GameObject b = Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
				b.transform.parent = GameObject.Find ("Track").transform;
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
