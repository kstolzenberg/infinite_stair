// to use, create empty gameobject. place this script there and then in the gui apply a prefab to the gameobject variables.
// this builds a generic stair of individual blocks from the origin. you can scale the prefab to use less objects.
// i don't know how you access stuff in here?
// consider to build between landings or runs then landings?

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

	private Transform lastChild;
	private Vector3 lastStep;
	private int lastIndex;
	private Vector3 newStart;
	
	// Use this for initialization
	void Start () {
		// first stair run
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				for (int k = y * tread; k < (y+1) * tread; k++){
				step = Instantiate(step, new Vector3(x, y, k), Quaternion.identity) as GameObject;
				step.gameObject.name = "step";
				step.transform.parent = transform;
				}
			}
		}
		// landing at the top
		lastIndex = (transform.childCount) - 1; // index of the last child
		lastChild = this.gameObject.transform.GetChild(lastIndex);
		lastStep = lastChild.transform.position;
		offset = 3; //offset = (5f / 2f); //should be landing width variable / 2
		landing = Instantiate (landing, new Vector3(lastStep.x, lastStep.y, lastStep.z + offset), Quaternion.identity) as GameObject;
		landing.gameObject.name = "landing";
		landing.transform.parent = transform;

		newStart = landing.transform.position; //need to convert this to an int?


		//build the next flight. pass a rotation through a function?
		// height needs to reset
		float height2 = height + 10f; // this is from zero - also problemmatic?
		Debug.Log (height);

		for (float y = newStart.y; y < height2; y++) {
			for (float x = newStart.x; x < width; x++) {
				for (float k = (y * tread); k < (y+1) * tread; k++){
					step = Instantiate(step, new Vector3(x, y, k+7), Quaternion.identity) as GameObject;
					step.gameObject.name = "step";
					step.transform.parent = transform;
				}
			}
		}


		// make an array and stash each direction then pull a random to decide where to go?
		// (1) on click - rain staircases. wrap in a function where origin and size changes?
		// i think we need a recursive function that calls itself to connect them???




	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
