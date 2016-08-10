using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void onCollisionEnter(Collision other){
		print (other.gameObject.name);
		print (1);
	}

	void onCollisionStay(Collision x){
		print (3);
	}

	void onCollisionExit(Collision y){
		print (2);
	}
}
