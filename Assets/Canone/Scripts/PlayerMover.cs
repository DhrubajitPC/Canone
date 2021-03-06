﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using UnityEngine.UI;

public class PlayerMover : MonoBehaviour {
	private float playerMovingSpeed = 0.2f;
    public static float playerAccelerateFactor = 1.0f;
    public static float time_in_game = 0;
    public static float playerAgilityFactor = 1.0f;

	public static bool gameEnd = false;
    public static bool was_alive = true;

	public GameObject bullet;
	public GameObject FlyingCar;
	public GameObject Ghost;
	private GhostBehaviour ghostBehaviour = null;
	private AgilityBehaviour agilityBehaviour = null;
	public GameObject FastShip;
	private GameObject currentCharacter;

	public GameObject RestartButton;

	public Transform bulletSpawn;
	public float fireRate;
	public UnityEngine.UI.Text SCORE;
	public int score;

	private float nextFire;

	private int bulletsLeft = 3;
	private float bulletCountDownTimer = 3;

	public void restart(){
		gameEnd = false;
		was_alive = true;
		switchCharacter (FastShip);
		RestartButton.SetActive (false);

		SCORE = GameObject.Find ("Canvas").GetComponent<Text>();
		score = 0;
		time_in_game = 0;
	}

	void Start(){
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		restart ();
	}

    void Update()
    {
        time_in_game += Time.deltaTime;
		if (ghostBehaviour != null) {
			ghostBehaviour.UpdateTimeLeft (Time.deltaTime);
		}
		playerAgilityFactor = 1.0f;
		if (agilityBehaviour != null) {
			agilityBehaviour.UpdateTimeLeft (Time.deltaTime);
			if (agilityBehaviour.on){playerAgilityFactor = 2f;}
		}
    }

	void FixedUpdate () {
		if (!gameEnd){
            score = (int)(time_in_game * 100);
            SCORE.text = ""+score;

			if (ghostBehaviour != null && Input.GetButton ("Fire1")) {
				ghostBehaviour.TurnOnInvisibleMode ();
			}

			if (agilityBehaviour != null && Input.GetButton ("Fire1")) {
				agilityBehaviour.TurnOnAgileMode ();
			}

            transform.Translate (Vector3.forward * playerMovingSpeed);
			if(currentCharacter.gameObject.name.Contains("Fast")){
			if (Input.GetButton ("Fire1") && Time.time > nextFire && bulletsLeft > 0) {
				bulletsLeft -= 1;
				nextFire = Time.time + fireRate;
				GameObject b = Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
				b.transform.parent = GameObject.Find ("Track").transform;
				GameObject.Find ("FastShip").GetComponent<AudioSource> ().Play ();
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
			GameObject.Find ("DestructionSoundManager").GetComponents<AudioSource> () [1].Play ();

			StartCoroutine (showLeaderboard ());
        }
	}

	IEnumerator showLeaderboard(){
		yield return new WaitForSeconds(3.0f);
		//show score board
		Social.localUser.Authenticate((bool success) => {
			if (success)
			{
				Social.ReportScore(score, "CgkIsbPEkt4TEAIQAQ", (bool success1) => {
					PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIsbPEkt4TEAIQAQ");
				});
			}
		});
		RestartButton.SetActive (true);
		yield return 0;
	}

	void OnCollisionEnter (Collision other)
	{
		string collidedItem = other.gameObject.name;
        //end game if obstacles are hit
        if (other.gameObject.tag == "Obstacle")
        {
            gameEnd = true;
            if (ghostBehaviour != null)
            {
                if (ghostBehaviour.on) { gameEnd = false; }
            }
        }

		// pick up powerups 
		if (collidedItem.Contains ("Pickup")) {
			transform.GetComponent<AudioSource> ().Play ();
			Destroy (other.gameObject);
			if (collidedItem.Contains ("Fast")) {
				if (ghostBehaviour != null) {
					ghostBehaviour.cancel ();
				}
				if (agilityBehaviour != null) {
					agilityBehaviour.cancel ();
				}
				switchCharacter (FastShip);
			} else if (collidedItem.Contains ("Flying")) {
				if (ghostBehaviour != null) {
					ghostBehaviour.cancel ();
				}
				switchCharacter (FlyingCar);
			} else if (collidedItem.Contains ("Ghost")){
				if (agilityBehaviour != null) {
					agilityBehaviour.cancel ();
				}
				switchCharacter (Ghost);
			}
		}
	}

	void switchCharacter(GameObject character){
		GameObject effect = (GameObject)Instantiate(Resources.Load("prefabs/PowerupGain"), transform.position, Quaternion.identity);
		effect.transform.SetParent (transform);
		Destroy(effect, 1.98f);

		Ghost.SetActive (false);
		FlyingCar.SetActive (false);
		FastShip.SetActive (false);
		character.SetActive (true);
		currentCharacter = character;
		ghostBehaviour = null;
		agilityBehaviour = null;
        if (character.gameObject.name.Contains("Ghost"))
        {
            ghostBehaviour = (GhostBehaviour)character.gameObject.GetComponent<GhostBehaviour>();
        }

        if (character.gameObject.name.Contains("FlyingCar"))
        {
			agilityBehaviour = (AgilityBehaviour)character.gameObject.GetComponent<AgilityBehaviour> ();
        } 
		playerAgilityFactor = 1.0f;
    }
}
