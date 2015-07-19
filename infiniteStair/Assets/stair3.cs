//this gets attached to a new empty game object. add cammera and prefabs in GUI.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stair3 : MonoBehaviour {
	
	public Camera raycastCamera; // always use the same camera
	public float raycastDistance = 50.0f; // this sets the plane at 50f from the camera! vertically. eventually make random
	private Vector3 worldPoint;
	private Plane buildPlane;


	private Vector3 stairStart;
	private Vector3 stairEnd;
	private float y1, y2, z1, z2;

	public GameObject landing;
	public GameObject step;
	float numSteps = 10; // explicit and size changes
	int incrementor = 5; // steps of randoms between planes

	private GameObject [] allLandings;
	private GameObject [] allSteps;

	// Use this for initialization
	void Start () {
		// UI text in gui, destroy here
		GameObject intro = GameObject.FindWithTag("intro");
		Destroy (intro, 4f);

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)){		
			// define and create landings
			// generate new planes?
			int vectorPosition = Random.Range(0, 20) * incrementor;
			buildPlane = new Plane(Vector3.right, new Vector3(-vectorPosition, 0, 0));
			Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
			if (buildPlane.Raycast(ray, out raycastDistance)){
				worldPoint = ray.GetPoint(raycastDistance);
				Debug.Log("RAYCAST WORLD POINT: " + worldPoint);
			}
			else {
				Debug.Log("DIDN'T HIT");
			}

			landing = Instantiate (landing, worldPoint, Quaternion.identity) as GameObject;
			landing.gameObject.name = "landing";
			landing.transform.parent = transform;

			allLandings = GameObject.FindGameObjectsWithTag("landing");

			// build stair from prev to next landing. no need to loop bc array is already expanding.1
			if (allLandings.Length >= 2) {	
				int j = allLandings.Length-2;
				stairStart = allLandings[j].transform.position;
				stairEnd = allLandings[j+1].transform.position;
			
				// check for stair direction & correct offset relative to landing
				if (stairEnd.y > stairStart.y ){
					y1 = stairStart.y + landing.transform.localScale.y/2;
					z1 = stairStart.z + landing.transform.localScale.z/2;
					y2 = stairEnd.y - landing.transform.localScale.y/2;
					z2 = stairEnd.z - landing.transform.localScale.z/2;
				} else {
					y1 = stairStart.y - landing.transform.localScale.y/2;
					z1 = stairStart.z + landing.transform.localScale.z/2;
					y2 = stairEnd.y + landing.transform.localScale.y/2; 
					z2 = stairEnd.z - landing.transform.localScale.z/2;
				}

				if (stairEnd.z < stairStart.z){
					z1 = stairStart.z - landing.transform.localScale.z/2;
					z2 = stairEnd.z + landing.transform.localScale.z/2;
				}

				float stepHeight = (y2 - y1) / numSteps;
				float stepWidth = (z2 - z1) / numSteps;

				//offset z while looping to scale
				for (int i = 0; i <= numSteps; i++){
					step = Instantiate (step, new Vector3 (stairStart.x, y1, z1 + stepWidth/2), Quaternion.identity) as GameObject;
					step.transform.localScale = new Vector3(5,stepHeight,stepWidth); 
					step.gameObject.name = "step"; 
					step.transform.parent = transform;
					y1 += stepHeight;
					z1 += stepWidth;
				}
			
			}

		}
	
		// move camera!
		if (Input.GetKey(KeyCode.W)){
			raycastCamera.transform.Translate(Vector3.forward);	
		}
		if (Input.GetKey(KeyCode.S)){
			raycastCamera.transform.Translate(Vector3.back);	
		}
		if (Input.GetKey(KeyCode.Q)){
			raycastCamera.transform.Translate(Vector3.up);	
		}
		if (Input.GetKey(KeyCode.E)){
			raycastCamera.transform.Translate(Vector3.down);	
		}
		if (Input.GetKey(KeyCode.A)){
			raycastCamera.transform.Translate(Vector3.left);	
		}
		if (Input.GetKey(KeyCode.D)){
			raycastCamera.transform.Translate(Vector3.right);	
		}
	}
}