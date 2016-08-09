using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using UnityEngine.UI;

public class PlayerMover : MonoBehaviour {
	public GameObject[] trackSegments;
	private float playerMovingSpeed = 0.1f;
	private int tileIndexToMove = 0;
	public static bool gameEnd = false;

	public GameObject bullet;
	public GameObject FlyingCar;
	public GameObject Ghost;
	public GameObject FastShip;
	public Transform bulletSpawn;
	public float fireRate;
	public UnityEngine.UI.Text SCORE;
	public int score;

	private float nextFire;

	private int bulletsLeft = 3;
	private float bulletCountDownTimer = 3;

	void Start(){
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		gameEnd = false;
//		GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = false;
//		trackSegments = new GameObject[] {
//			GameObject.Find ("TrackSegment1"),
//			GameObject.Find ("TrackSegment2"),
//			GameObject.Find ("TrackSegment3"),
//			GameObject.Find ("TrackSegment4")};
		
//		Ghost = GameObject.Find ("Ghost");
//		FastShip = GameObject.Find ("FastShip");
//		FlyingCar = GameObject.Find ("FlyingCar");
		switchCharacter (FastShip);

		SCORE = GameObject.Find ("Canvas").GetComponent<Text>();
		score = 0;
	}

	void Update(){
		
	}

	void FixedUpdate () {
		// endless track
		if (this.transform.position.z > 30 * (tileIndexToMove + 1) + 10) {
			trackSegments [tileIndexToMove % 4].transform.Translate (0, 30*4, 0);
			tileIndexToMove += 1;
			//score display - cross 1 more tile get 10 more pts
			score += 10;
			SCORE.text = "Score : "+ score;
		}
		if (!gameEnd){
			transform.Translate (Vector3.forward * playerMovingSpeed);
			if (Input.GetButton ("Fire1") && Time.time > nextFire && bulletsLeft > 0) {
				bulletsLeft -= 1;
				nextFire = Time.time + fireRate;
				GameObject b = Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
				b.transform.parent = GameObject.Find ("Track").transform;
	
//				bullets.Add (b);
			}
			if (bulletsLeft == 0) {
				bulletCountDownTimer -= Time.deltaTime;
			}
			if (bulletCountDownTimer < 0) {
				bulletsLeft = 3;
				bulletCountDownTimer = 3;
			}
		}	
	}

	void OnCollisionEnter (Collision other)
	{
		string collidedItem = other.gameObject.name;
		//end game if obstacles are hit
		if(collidedItem.Contains("FleshCube") || collidedItem.Contains("turret") || collidedItem.Contains("prism"))
		{
			gameEnd = true;
//			Destroy (gameObject);
//			GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = true;
			//TODO : show score board
//			Social.ReportScore (12345, "CgkIsbPEkt4TEAIQAQ" ,(bool success)=>{
//				Social.ShowLeaderboardUI();
//			});
		}

		// pick up powerups 
		if (collidedItem.Contains ("Pickup")) {
			Destroy (other.gameObject);
			if (collidedItem.Contains ("Fast")) {
				switchCharacter (FastShip);
			} else if (collidedItem.Contains ("Flying")) {
				switchCharacter (FlyingCar);
			} else {
				switchCharacter (Ghost);
			}
		}
	}

	void switchCharacter(GameObject character){
		Ghost.SetActive (false);
		FlyingCar.SetActive (false);
		FastShip.SetActive (false);
		character.SetActive (true);
	}

}
