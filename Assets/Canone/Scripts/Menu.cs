using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class Menu : MonoBehaviour {

	public Text text;
	public const string leaderboard_awesomeleaderboard = "CgkIsbPEkt4TEAIQAQ"; // <GPGSID>
	private string mStatusText = "Ready.";

	private Text oldScore;
	private GameObject play;
	private GameObject sIn;
	private GameObject sOut;
	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Landscape;

		GooglePlayGames.PlayGamesPlatform.Activate();


		play = GameObject.Find ("PlayButton");
		sIn = GameObject.Find ("sInButton");
		sOut = GameObject.Find ("sOutButton");

		play.GetComponent<Button> ().interactable = false;

		oldScore = GameObject.Find ("score").GetComponent<Text> ();
		oldScore.text = "Last Score:" + PlayerPrefs.GetInt ("Score");

	}

	public void loadGame(){
//		SceneManager.LoadScene ("GameScene");
		SceneManager.LoadScene("TrackScene");
	}

	public void signIn(){
		if (!Social.localUser.authenticated) {
			// Authenticate
			mStatusText = "Authenticating...";
			Social.localUser.Authenticate ((bool success) => {
				if (success) {
					mStatusText = "Welcome " + Social.localUser.userName;
					string token = GooglePlayGames.PlayGamesPlatform.Instance.GetToken ();
					Debug.Log (token);
				} else {
					mStatusText = "Authentication failed.";
				}
			});
		}
	}
	public void signOut(){
		mStatusText = "Signing Out...";
		((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut ();
		mStatusText = "SignIn to Play";
	}

	public void Update(){
		if (true){
//		if (Social.localUser.authenticated) {
			play.GetComponent<Button> ().interactable = true;
			sOut.SetActive (true);
			sIn.SetActive (false);
		} else {
			play.GetComponent<Button> ().interactable = false;
			sIn.SetActive (true);
			sOut.SetActive (false);
		}
		text.text = mStatusText;
	}
}
