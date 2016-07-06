using UnityEngine;
using System.Collections;

public class TrackScroller : MonoBehaviour {
	private float scrollSpeed = 3.0f;
	public float trackUnitSize;
	public float numOfTilesPerUnit;

	void OnTriggerExit(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			transform.position = new Vector3 (transform.position.x, 
				transform.position.y, 
				transform.position.z + trackUnitSize * numOfTilesPerUnit);
		}

	}
}
