using UnityEngine;
using System.Collections;

public class TrackRotater : MonoBehaviour {

	//private float rotateSpeed = 0.2f;
    private float minRotateSpeed = 0.1f;
    private float maxRotateSpeed = 0.5f;

	private GameObject player;

    private float maxRot = 75.0f;
    private float initRot = 0f; //Modify this when you are changing the track from ground to ceiling!

	private static float accelerometerUpdateInterval = 1.0f / 60.0f;
	// The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current input sample (and vice versa).
	private static float lowPassKernelWidthInSeconds = 1.0f;
	// This next parameter is initialized to 2.0 per Apple's recommendation, or at least according to Brady! ;)
	private static float shakeDetectionThreshold = 2.0f;

	private static float lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
	private static Vector3 lowPassValue = Vector3.zero;
	private AudioSource [] aSources;


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

	void Start(){
		player = GameObject.Find ("Player");
		shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;
		aSources = Camera.main.GetComponents<AudioSource>();

	}

	void Update(){
		if (!PlayerMover.gameEnd) {
#if UNITY_EDITOR
			bool rotate = Input.GetKeyDown ("space");
#else
			Vector3 acceleration = Input.acceleration;
			lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
			Vector3 deltaAcceleration = acceleration - lowPassValue;
			bool rotate = deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold;
#endif
//			if (initRot == 0) {
//				//enable bg 1
//				print("zerg");
//				aSources [1].mute = true;
//				aSources [0].mute = false;
//			} else {
//				print ("protos");
//				aSources [0].mute = true;
//				aSources [1].mute = false;
//				// enable bg 2
//			}
			if (rotate) {
				initRot = initRot == 0 ? 180f : 0f;

				player.transform.position = new Vector3 (player.transform.position.x, 
					1.6f, 
					player.transform.position.z);
				transform.Rotate (Vector3.forward * 180f);
			}
		}
	}

    void FixedUpdate () {
        if (!PlayerMover.gameEnd){

#if UNITY_EDITOR
            float tilt = Input.GetAxis("Horizontal");
#else
            float tilt = Input.acceleration.x;
#endif
			tilt = Mathf.Clamp (tilt, -maxRotateSpeed * PlayerMover.playerAgilityFactor, maxRotateSpeed * PlayerMover.playerAgilityFactor);
			Quaternion currentRotation = transform.rotation;
			Quaternion userRotation = Quaternion.Euler (0, 0, -tilt);
			float rotation = relativeRotation ((currentRotation * userRotation).eulerAngles.z, initRot);
			if (Mathf.Abs (tilt) > minRotateSpeed && Mathf.Abs (rotation) <= maxRot) 
			{
				transform.Rotate (-tilt * Vector3.forward);
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
		}
	}

}
