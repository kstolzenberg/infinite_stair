//this gets attached to a new empty game object. add cammera and prefabs in GUI.
// note: GameObject is a type of object whereas gameObject is a local variable of an instance of the GameObject

using UnityEngine;
using System.Collections;

public class stair3 : MonoBehaviour {
	
	public Camera raycastCamera; // always use the same camera
	public float raycastDistance = 50.0f; // this sets the plane at 50f from the camera! vertically. eventually make random

	private Vector3 stairStart;
	private Vector3 stairEnd;

	public GameObject landing;
	public GameObject step;
	float numSteps = 10; //explicit and size changes

	private GameObject [] allLandings; //add by tag; tag in prefab
	private GameObject [] allSteps;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	
		if (Input.GetMouseButtonDown(0)){
			// define and create landings
			Vector3 screenPositionWithDistance = Input.mousePosition;
			screenPositionWithDistance.z = raycastDistance;
			// Debug.Log("screenpt:"+screenPositionWithDistance); // this ends up in correct plane but wrong units? 
			Vector3 worldPoint = raycastCamera.ScreenToWorldPoint(screenPositionWithDistance); 
			// http://answers.unity3d.com/questions/199068/problem-with-screentoworldpoint-and-touches-c.html 
			// Debug.Log("worldpt:"+worldPoint); // the changing z is actaully the surface of a sphere from the camera point!!
			landing = Instantiate (landing, worldPoint, Quaternion.identity) as GameObject;
			landing.gameObject.name = "landing";
			landing.transform.parent = transform;

			allLandings = GameObject.FindGameObjectsWithTag("landing");

			// build stair from prev to next landing.
			for (int j = 0; j < allLandings.Length; j++){ 
				// this is rebuilding all steps always. how to have step loop only between current indexes? is this bc of void update? coroutines?
				// scales about center rather than reset and messes with origin?
				// negative direction doesn't work?
				stairStart = allLandings[j].transform.position;
				stairEnd = allLandings[j+1].transform.position;
			
				//you are drawing stairs between the first and last step that correct relative to landing
				float y1 = stairStart.y + landing.transform.localScale.y/2 + step.transform.localScale.y/2;
				float z1 = stairStart.z + landing.transform.localScale.z/2 + step.transform.localScale.z/2;
				float y2 = stairEnd.y - landing.transform.localScale.y/2 - step.transform.localScale.y/2; 
				float z2 = stairEnd.z - landing.transform.localScale.z/2 - step.transform.localScale.z/2;
				float stepHeight = (y2 - y1) / numSteps;
				float stepWidth = (z2 - z1) / numSteps;

				for (int i = 0; i <= numSteps; i++){
					step = Instantiate (step, new Vector3 (stairStart.x, y1, z1), Quaternion.identity) as GameObject;
					//step.transform.localScale = new Vector3(5,stepHeight,stepWidth); 
					step.gameObject.name = "step"; 
					step.transform.parent = transform;
					y1 += stepHeight;
					z1 += stepWidth;
				}


			
			}

		}
	
	}
}