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

	public GameObject[] obstacles;
	private int obsCount = 1;
	public float obsRadius = 2.0f;

	private float nextFire;

	private int bulletsLeft = 3;
	private float bulletCountDownTimer = 3;

	void Start(){
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		gameEnd = false;
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
			obsCount ++ ;
			randomeObstacles(trackSegments [(tileIndexToMove+1) % 4],obsCount);
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
			//allow player to tumble and crash and shit
			this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			Vector3 collisionVector = new Vector3 (Random.Range (-100, 100), Random.Range (200, 500), -200);
			this.GetComponent<Rigidbody> ().AddRelativeForce (collisionVector);
			this.GetComponent<Rigidbody> ().AddTorque (collisionVector);
			this.transform.Find ("Fire").gameObject.SetActive (true);
//			Destroy (gameObject);
//			GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = true;
			//TODO : show score board
			Social.localUser.Authenticate ((bool success) => {
				if (success) {
					Social.ReportScore (score, "CgkIsbPEkt4TEAIQAQ" ,(bool success1)=>{
						Social.ShowLeaderboardUI();
					});
				}
			});

		}

		// pick up powerups 
		if (collidedItem.Contains ("Pickup")) {
			Destroy (other.gameObject);
			if (collidedItem.Contains ("Fast")) {
				switchCharacter (FastShip);
			} else if (collidedItem.Contains ("Flying")) {
				switchCharacter (FlyingCar);
			} else if (collidedItem.Contains ("Ghost")){
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

	void randomeObstacles(GameObject segment, int count) {

		//        foreach (Transform child in segment.transform) {
		//            GameObject.Destroy(child.gameObject);
		//        }

		Vector3 randPos = Vector3.zero;
		int myCheck = 0; //count overlap colliders
		for(int i = 0; i < count; i++) {
			do {
				myCheck = 0;
				randPos = new Vector3(Random.Range(25.0f, 25.0f), Random.Range(0.0f, 200.0f), Random.Range(-25.0f,25.0f));
				Collider[] hitColliders = Physics.OverlapSphere(randPos, obsRadius);
				for(int j = 0; j < hitColliders.Length; j++) {
					if (hitColliders[j].tag == "mob") {
						myCheck++;
					}
				}
			} while (myCheck > 0);
			GameObject obstacle = obstacles [Random.Range (0, 3)];

			GameObject clone = Instantiate(obstacle,randPos, Quaternion.identity) as GameObject;  
			clone.transform.parent = segment.transform;
			clone.transform.localPosition = randPos;
		}
	} 



}
