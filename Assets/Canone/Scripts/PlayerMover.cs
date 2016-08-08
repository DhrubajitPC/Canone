using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using UnityEngine.UI;

public class PlayerMover : MonoBehaviour {
	private GameObject[] trackSegments;
	private float playerMovingSpeed = 0.1f;
	private int tileIndexToMove = 0;
	public static bool gameEnd = false;

	public GameObject bullet;
	public Transform bulletSpawn;
	public float fireRate;

	private float nextFire;

	private int bulletsLeft = 3;
	private float bulletCountDownTimer = 3;

	List<GameObject> bullets = new List<GameObject>();
	

	void Start(){
		gameEnd = false;
		GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = false;
		trackSegments = new GameObject[] {
			GameObject.Find ("TrackSegment1"),
			GameObject.Find ("TrackSegment2"),
			GameObject.Find ("TrackSegment3"),
			GameObject.Find ("TrackSegment4")};
	}

	void FixedUpdate () {
		if (this.transform.position.z > 30 * (tileIndexToMove + 1)) {
			trackSegments [tileIndexToMove % 4].transform.Translate (0, 30*4, 0);
			tileIndexToMove += 1;
		}
		if (!gameEnd){
			transform.Translate (Vector3.forward * playerMovingSpeed);
			if (Input.GetButton ("Fire1") && Time.time > nextFire && bulletsLeft > 0) {
				bulletsLeft -= 1;
				nextFire = Time.time + fireRate;
				GameObject b = Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
				b.transform.parent = GameObject.Find ("Track").transform;
	
				bullets.Add (b);
			}
			if (bulletsLeft == 0) {
				bulletCountDownTimer -= Time.deltaTime;
			}
			if (bulletCountDownTimer < 0) {
				bulletsLeft = 3;
				bulletCountDownTimer = 3;
			}

			if (bullets.Count > 0) {
				foreach (GameObject b in bullets) {
					if (b.transform.position.z - this.transform.position.z > 15) {
						bullets.Remove (b);
						Destroy (b);
					}
				}
			}
		}	
	}

	void OnCollisionEnter (Collision other)
	{
		string collidedItem = other.gameObject.name;
		if(collidedItem.Contains("FleshCube") || collidedItem.Contains("turret") || collidedItem.Contains("prism"))
		{
			gameEnd = true;
			GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = true;
//			Social.ReportScore (12345, Social.localUser.id ,(bool success)=>{
//				Social.ShowLeaderboardUI();
//			});
		}
	}

}
