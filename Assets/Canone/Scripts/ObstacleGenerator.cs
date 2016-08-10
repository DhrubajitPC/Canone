using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {

    public GameObject[] trackSegments;
    private int tileIndexToMove = 0;
    private GameObject[] ObsHolder = new GameObject[60];
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
        if (this.transform.position.z > 30 * (tileIndexToMove + 1) + 10)
        {
            trackSegments[tileIndexToMove % 4].transform.Translate(0, 30 * 4, 0);

            //obstacles generation
            this.GetComponent<ObstacleGenerator>().ObstacleGeneration();

            tileIndexToMove += 1;
        }
    }

    public void ObstacleGeneration()
    {
        float generationRange = trackSegments[tileIndexToMove % 4].transform.position.z;
        float gap = 30f;
        for (int i = (tileIndexToMove % 4) * 15; i < (tileIndexToMove % 4 + 1) * 15; i++)
        {
            float offset = Random.Range(0f, 1f) >= 0.5 ? 0 : 180;
            float deg = Random.Range(20, 160) + offset;
            GameObject itemToPlace = deg <= 180 ? ObstacleTypes[Random.Range(4, 8)] : ObstacleTypes[Random.Range(0, 4)];

            Debug.Log(itemToPlace.gameObject.name);
            Debug.Log(offset);
            //			Debug.Log (deg);

            float trackz = GameObject.Find("Track").transform.rotation.eulerAngles.z;
            deg = (deg + trackz) % 360;
            float rad = deg * Mathf.Deg2Rad;
            Vector3 placement = new Vector3(Mathf.Cos(rad) * 11, Mathf.Sin(rad) * 11 + 12, Random.Range(generationRange, generationRange + 30));
            bool toPlace = true;
            for (int j = (tileIndexToMove % 4) * 15; j < i; j++)
            {
                if (ObsHolder[j] != null && Vector3.Distance(ObsHolder[j].transform.position, placement) <= gap)
                {
                    toPlace = false;
                    break;
                }
            }
            if (toPlace)
            {
                if (ObsHolder[i] == null)
                {
                    ObsHolder[i] = Instantiate(itemToPlace, placement, Quaternion.identity) as GameObject;
                    ObsHolder[i].transform.parent = trackSegments[tileIndexToMove % 4].transform;
                }
                else {
                    ObsHolder[i].transform.position = placement;
                }
            }
        }
    }
}
