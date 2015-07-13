// to use, create empty gameobject. place this script there and then in the gui apply a prefab to the gameobject variables.
// wrap in functions and build randomly. difficulty connecting 
// note !! there is building in reality and building in gamelandia - reality isn't always how you get there!

using UnityEngine;
using System.Collections;



public class stair2 : MonoBehaviour {
	
	public GameObject step;
	public GameObject landing;
	public int width;
	public int height;
	public int tread; // make height and location a random? add if else to deal with 90 angles and Quaternion.Eulers?
	private float offset; // correct for centroid positioning

	private Transform lastStep;
	private Vector3 lastStepPosition;
	private int lastIndex;
	private Vector3 newStart;
	
	// Use this for initialization
	void Start () {
		//these functions are impossible !!! too much fussy syntax :<
		//make this a struct - capture info and access it 
//			int height = Random.Range(5, 20);
//			int width = Random.Range(1, 3);
//			int tread = Random.Range(1, 3);
//			int startX = Random.Range(-50, 50);
//			int startY = Random.Range(0, 75);

		// first stair run
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				for (int k = y * tread; k < (y+1) * tread; k++) {
					step = Instantiate (step, new Vector3 (x, y, k), Quaternion.identity) as GameObject;
					step.gameObject.name = "step";
					step.transform.parent = transform;
				}
			}
		}
		// landing at the top
		lastIndex = (transform.childCount) - 1; // index of the last child
		lastStep = this.gameObject.transform.GetChild (lastIndex);
		lastStepPosition = lastStep.transform.position;
		landing = Instantiate (landing) as GameObject;
		// this will correctly center the landing!!!!
		landing.transform.position = new Vector3 (lastStepPosition.x, 
		                                          lastStepPosition.y, 
		                                          lastStepPosition.z + lastStep.transform.localScale.z / 2 + landing.transform.localScale.z / 2);
		landing.transform.rotation = Quaternion.identity;
		//offset = ; //offset = (5f / 2f); //should be landing width variable / 2
		landing.gameObject.name = "landing";
		landing.transform.parent = transform;
		newStart = landing.transform.position; //need to convert this to an int?

		Debug.Log ("Rolled once!");

	
//		 build the next flight. pass a rotation through a function?
//		 make an array and stash each direction then pull a random to decide where to go
//		 i think we need a recursive function that calls itself to connect them???
//		 height needs to reset
		float height2 = height + 10f; // this is from zero - also problemmatic?
		Debug.Log (height);

		for (float y = newStart.y; y < height2; y++) {
			for (float x = newStart.x; x < width; x++) {
				for (float k = (y * tread); k < (y+1) * tread; k++) {
					step = Instantiate (step, new Vector3 (x, y, k + 7), Quaternion.identity) as GameObject;
					step.gameObject.name = "step";
					step.transform.parent = transform;
				}
			}
		}
	}


	// Update is called once per frame
	void Update () {
	
	}

}
