using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using UnityEngine.UI;

public class PlayerMover : MonoBehaviour {
	private float playerMovingSpeed = 0.2f;
    private float playerAccelerateFactor = 1.0f;
    private float time_in_game = 0;

	public static bool gameEnd = false;
    public static bool was_alive = true;

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

	void Start(){
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		gameEnd = false;
		switchCharacter (FastShip);

		SCORE = GameObject.Find ("Canvas").GetComponent<Text>();
		score = 0;
	}

    void Update()
    {
        time_in_game += Time.deltaTime;
        //playerAccelerateFactor += Mathf.Max(1.0f + (time_in_game / 600), 2.0f);
    }

	void FixedUpdate () {
		if (!gameEnd){
            score = (int)(time_in_game * 100);
            SCORE.text = "Score : " + score;

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
        if (gameEnd && was_alive)
        {
            //dead script
            was_alive = false;
            //allow player to tumble and crash and shit
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Vector3 collisionVector = new Vector3(Random.Range(-100, 100), Random.Range(200, 500), -200);
            this.GetComponent<Rigidbody>().AddRelativeForce(collisionVector);
            this.GetComponent<Rigidbody>().AddTorque(collisionVector);
            this.transform.Find("Fire").gameObject.SetActive(true);

            //show score board
            Social.localUser.Authenticate((bool success) => {
                if (success)
                {
                    Social.ReportScore(score, "CgkIsbPEkt4TEAIQAQ", (bool success1) => {
                        Social.ShowLeaderboardUI();
                    });
                }
            });
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
			playerMovingSpeed = 0.3f * playerAccelerateFactor;
		} else {
			playerMovingSpeed = 0.2f * playerAccelerateFactor;
		}
	}
}
