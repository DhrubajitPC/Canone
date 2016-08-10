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
	private GameObject currentCharacter;

	public Transform bulletSpawn;
	public float fireRate;
	public UnityEngine.UI.Text SCORE;
	public int score;

	private float nextFire;

	private int bulletsLeft = 3;
	private float bulletCountDownTimer = 3;

	public GameObject[] ObsHolder = new GameObject[60];
	public GameObject[] ObstacleTypes = new GameObject[8];
	private static readonly Random random = new Random();

	void Start(){
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		gameEnd = false;
		switchCharacter (FastShip);

		SCORE = GameObject.Find ("Canvas").GetComponent<Text>();
		score = 0;
	}

	void FixedUpdate () {
		// endless track
		if (this.transform.position.z > 30 * (tileIndexToMove + 1) + 10) {
			trackSegments [tileIndexToMove % 4].transform.Translate (0, 30*4, 0);

			//obstacles generation
			ObstacleGeneration ();

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
		if(!currentCharacter.gameObject.name.Contains ("Ghost") && 
			(collidedItem.Contains("FleshCube") || collidedItem.Contains("turret") || collidedItem.Contains("prism")))
		{
			gameEnd = true;
			//allow player to tumble and crash and shit
			this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			Vector3 collisionVector = new Vector3 (Random.Range (-100, 100), Random.Range (200, 500), -200);
			this.GetComponent<Rigidbody> ().AddRelativeForce (collisionVector);
			this.GetComponent<Rigidbody> ().AddTorque (collisionVector);
			this.transform.Find ("Fire").gameObject.SetActive (true);

			//show score board
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
		currentCharacter = character;
		if (character.gameObject.name.Contains ("Flying")) {
			playerMovingSpeed = 0.45f;
		} else {
			playerMovingSpeed = 0.3f;
		}
	}
		

	public void ObstacleGeneration(){
		float generationRange = trackSegments[tileIndexToMove % 4].transform.position.z;
		float gap = 30f;
		for (int i = (tileIndexToMove % 4) * 15; i < (tileIndexToMove % 4 + 1) * 15; i++){
			float offset =Random.Range (0f, 1f) >= 0.5 ? 0 : 180;
			float deg = Random.Range (20, 160) + offset;
			GameObject itemToPlace =  deg <= 180 ? ObstacleTypes [Random.Range (4, 8)] : ObstacleTypes [Random.Range (0,4)];

			Debug.Log (itemToPlace.gameObject.name);
			Debug.Log (offset);
//			Debug.Log (deg);

			float trackz = GameObject.Find("Track").transform.rotation.eulerAngles.z;
			deg = (deg+trackz)%360;
			float rad = deg * Mathf.Deg2Rad;
			Vector3 placement = new Vector3 (Mathf.Cos(rad)*11, Mathf.Sin(rad)*11+12, Random.Range(generationRange, generationRange +30));
			bool toPlace = true;
			for (int j = (tileIndexToMove % 4) * 15; j < i; j++) {
				if (ObsHolder [j] != null && Vector3.Distance (ObsHolder [j].transform.position, placement) <= gap) {
					toPlace = false;
					break;
				}
			}
			if (toPlace) {
				if (ObsHolder [i] == null) {
					ObsHolder[i] = Instantiate (itemToPlace, placement, Quaternion.identity) as GameObject;
					ObsHolder[i].transform.parent = trackSegments[tileIndexToMove % 4].transform;
				} else {
					ObsHolder [i].transform.position = placement;
				}
			}
		}
	}
}
