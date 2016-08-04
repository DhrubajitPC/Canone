using UnityEngine;
using System.Collections;

public class TrackScroller : MonoBehaviour {
	void OnTriggerExit(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			transform.position = new Vector3 (transform.position.x, 
				transform.position.y, 
				transform.position.z+ 30f * 4 );
		}

	}

}
