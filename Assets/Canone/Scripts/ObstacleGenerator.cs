using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {

    public GameObject[] trackSegments;
    private int tileIndexToMove = 0;
    public static GameObject[] ObsHolder = new GameObject[60];
    private int difficulty = 1; //1 to 15
    public GameObject[] ObstacleTypes = new GameObject[8];

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        // endless track
        if (this.transform.position.z > 30 * (tileIndexToMove + 1) + 30)
        {
            trackSegments[tileIndexToMove % 4].transform.Translate(0, 30 * 4, 0);

            //obstacles generation
            int difficulty = (int) (1+ Mathf.Min(PlayerMover.time_in_game / 20.0f, 4) + Mathf.Min(PlayerMover.time_in_game / 60.0f, 5) + Mathf.Min(PlayerMover.time_in_game / 90.0f, 5));
            PlayerMover.playerAccelerateFactor = 1.0f + (difficulty/15.0f);
            ObstacleGeneration(difficulty);
            //ObstacleReposition();
            
            tileIndexToMove += 1;
        }
    }

    public void ObstacleGeneration(int difficulty)
    {
        print(difficulty);
        float generationRange = trackSegments[tileIndexToMove % 4].transform.position.z;
        float gap = 1f;
        //for (int i = (tileIndexToMove % 4) * 15; i < ((tileIndexToMove % 4) + 1) * 15; i++)
        for (int i = (tileIndexToMove % 4) * 15; i < ((tileIndexToMove % 4) * 15)+difficulty; i++)
        {
            bool toPlace = false;
            GameObject itemToPlace = null;
            float deg = 0.0f;
            Vector3 placement = Vector3.zero;
            while (!toPlace)
            {
                float offset = Random.Range(0f, 1f) >= 0.65 ? 0 : 180;
                deg = Random.Range(10, 170) + offset;
                itemToPlace = ObstacleTypes[Random.Range(0, 4)];
                if (deg < 180.0f)
                {
                    itemToPlace = ObstacleTypes[Random.Range(4, 8)];
                }

				//lucky, generate powerup
				if (i % 5 == 0) {
					int rand = Random.Range (0, 10);
					if (rand == 1) {
						itemToPlace = (GameObject) Resources.Load ("prefabs/PickupGhost");
					} else if (rand == 2) {
						itemToPlace = (GameObject) Resources.Load ("prefabs/PickupFlyingCar");
					} else if (rand == 3) {
						itemToPlace = (GameObject) Resources.Load ("prefabs/PickupFastShip");
					}
				}

                //Debug.Log(itemToPlace.gameObject.name);
                //Debug.Log(offset);

                float trackz = GameObject.Find("Track").transform.rotation.eulerAngles.z;
                deg = (trackz + deg) % 360; //make degree follow trackz
                float rad = deg * Mathf.Deg2Rad;
                placement = new Vector3(Mathf.Cos(rad) * 12, (Mathf.Sin(rad) * 12) + 12, Random.Range(generationRange, generationRange + 30f));

                toPlace = true;
                //check if this overlaps with other objects
                for (int j = (tileIndexToMove % 4) * 15; j < i; j++)
                {
                    if (ObsHolder[j] != null && Vector3.Distance(ObsHolder[j].transform.position, placement) <= gap)
                    {
                        toPlace = false;
                        break;
                    }
                }
            }
            if (ObsHolder[i] != null)
            {
                Destroy(ObsHolder[i].gameObject);
            } else
            {
                //x random rotation (facing player), y rotation based on location in track, z is following prefab
                //Quaternion quart = Quaternion.Euler(itemToPlace.transform.rotation.eulerAngles.x, Random.Range(0, 180), deg + 90);
                Quaternion quart = Quaternion.Euler(itemToPlace.transform.rotation.eulerAngles.x, Random.Range(0, 180), 0);
                ObsHolder[i] = Instantiate(itemToPlace, itemToPlace.transform.position, Quaternion.identity) as GameObject;
                //ObsHolder[i].transform.rotation = quart;
                ObsHolder[i].transform.RotateAround(Vector3.zero, Vector3.forward, deg + 90);
                ObsHolder[i].transform.position = ObsHolder[i].transform.position + placement;
                ObsHolder[i].transform.SetParent(trackSegments[tileIndexToMove % 4].transform, true);
                //ObsHolder[i].transform.localScale = itemToPlace.transform.localScale;
                //ObsHolder[i].transform.SetParent(trackSegments[tileIndexToMove % 4].transform, false);
                //ObsHolder[i].transform.parent = trackSegments[tileIndexToMove % 4].transform;
                //Vector3 centerRot = new Vector3(0, 12, placement.z);
                //ObsHolder[i].transform.RotateAround(centerRot, Vector3.forward, deg);
            }
        }
    }
}
