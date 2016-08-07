using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class PlayerMover : MonoBehaviour {
	private GameObject[] trackSegments;
	private float playerMovingSpeed = 0.1f;
	private int tileIndexToMove = 0;
	public static bool gameEnd = false;

	public GameObject bullet;
	public Transform bulletSpawn;
	public float fireRate;

	private float nextFire;
	private float bulletCoolDown = 3;

	

	void Start(){
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
//		Debug.Log (this.transform.position.z);
//		if (!gameEnd){transform.Translate (Vector3.forward * playerMovingSpeed);}	
	}

	void Update(){
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			GameObject b = Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
			b.transform.parent = GameObject.Find ("Track").transform;
		}
	}
			

	void OnCollisionEnter (Collision other)
	{
		string collidedItem = other.gameObject.name;
		if(collidedItem.Contains("FleshCube") || collidedItem.Contains("turret") || collidedItem.Contains("prism"))
		{
			gameEnd = true;
//			Social.ReportScore (12345, Social.localUser.id ,(bool success)=>{
//				Social.ShowLeaderboardUI();
//			});
		}
	}


}
