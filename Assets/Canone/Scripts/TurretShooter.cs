﻿using UnityEngine;
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
		transform.LookAt (player.transform);
		if (gameObject.name == "turret" && angle < 30f && dist < 30) {
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
		} else if (gameObject.name == "turret_flesh" && angle < 135 && dist < 15) {
			GameObject b = Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
			b.transform.parent = GameObject.Find ("Track").transform;
			b.transform.LookAt (player.transform);
		}
	
	}
}
