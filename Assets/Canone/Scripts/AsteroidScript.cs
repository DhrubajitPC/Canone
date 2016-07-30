using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Find("default").transform.Rotate(Vector3.right * 360 * Time.deltaTime);
    }
}
