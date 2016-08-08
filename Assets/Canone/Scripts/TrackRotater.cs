using UnityEngine;
using System.Collections;

public class TrackRotater : MonoBehaviour {
	//private float rotateSpeed = 0.2f;
    private float minRotateSpeed = 0.1f;
    private float maxRotateSpeed = 0.5f;

    private float maxRot = 75.0f;
    private float initRot = -4.0f; //Modify this when you are changing the track from ground to ceiling!

    //returns the relative rotation in terms of -180 and 180 from init
    float relativeRotation(float rot, float init)
    {
        float res = (rot - init) % 360;
        if (res > 180)
        {
            return (res - 360);
        }
        return res;
    }

    void FixedUpdate () {
        if (!PlayerMover.gameEnd){
#if UNITY_EDITOR
            float tilt = Input.GetAxis("Horizontal");
#else
            float tilt = Input.acceleration.x;
#endif
			tilt = Mathf.Clamp (tilt, -maxRotateSpeed, maxRotateSpeed);
			Quaternion currentRotation = transform.rotation;
			Quaternion userRotation = Quaternion.Euler (0, 0, -tilt);
			float rotation = relativeRotation ((currentRotation * userRotation).eulerAngles.z, initRot);
			if (Mathf.Abs (tilt) > minRotateSpeed && Mathf.Abs (rotation) <= maxRot) 
			{
				transform.Rotate (new Vector3 (0, 0, -1) * tilt);
			}

			float z_rot = relativeRotation (currentRotation.eulerAngles.z, initRot);
			//sanity check
			if (z_rot > maxRot)
			{
				transform.rotation = Quaternion.Euler(new Vector3(0, 0, initRot + maxRot));
			}
			else if (z_rot < -maxRot) {
				transform.rotation = Quaternion.Euler(new Vector3(0, 0, initRot - maxRot));
			}

			if (Input.GetKeyDown ("space")) {
				initRot = initRot >0? -4.0f: 176f;
//				GameObject.Find ("Player").transform.Translate (new Vector3(0, 0.3f ,0));
				transform.Rotate (new Vector3 (0, 0, 180f));
			}
		}
	}

}
