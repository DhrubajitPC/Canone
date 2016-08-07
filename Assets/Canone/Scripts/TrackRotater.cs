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
            GameObject track = GameObject.Find("Track");
#if UNITY_EDITOR
            float tilt = Input.GetAxis("Horizontal");
#else
            float tilt = Input.acceleration.x;
#endif
            float tiltfactor = tilt;
            if (tilt > maxRotateSpeed) { tiltfactor = maxRotateSpeed; };
            if (tilt < -maxRotateSpeed) { tiltfactor = -maxRotateSpeed; };

            float z_rot = relativeRotation(track.transform.rotation.eulerAngles.z, initRot);
            if (Mathf.Abs(tiltfactor) > minRotateSpeed)
            {
                Quaternion test = track.transform.rotation;
                test *= Quaternion.Euler(0, 0, -tiltfactor);
                float test_z_rot = relativeRotation(test.eulerAngles.z, initRot);
                if (test_z_rot > maxRot || test_z_rot < -maxRot)
                {
                    //do nothing
                } else
                {
                    transform.Rotate(new Vector3(0, 0, -1) * tiltfactor);
                }
            }

            //sanity check
            if (z_rot > maxRot)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, initRot + maxRot));
            }
            else if (z_rot < -maxRot) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, initRot - maxRot));
            }
			/*if ((GameObject.Find ("Track").transform.rotation.z > -0.65f && GameObject.Find ("Track").transform.rotation.z < 0.65f)
				||(GameObject.Find ("Track").transform.rotation.z <= -0.65f && Input.GetAxis ("Horizontal") < 0)
				||(GameObject.Find ("Track").transform.rotation.z >= 0.65f && Input.GetAxis ("Horizontal") > 0)) {
				transform.Rotate (new Vector3(0,0,-1) * Input.GetAxis ("Horizontal") * rotateSpeed);
			}*/
		}
	}
}
