//this gets attached to a new empty game object. add cammera and prefabs in GUI.
//random placement instead of a plane

using UnityEngine;
using System.Collections;

public class stair4 : MonoBehaviour {
	
	public Camera raycastCamera; // always use the same camera
	public float raycastDistance = 50.0f; // this sets the plane at 50f from the camera! vertically. eventually make random
	private Vector3 worldPoint;
	private Plane buildPlane;
	
	
	private Vector3 stairStart;
	private Vector3 stairEnd;
	private float y1, y2, z1, z2;
	
	public GameObject landing;
	public GameObject step;
	float numSteps = 10; //explicit and size changes
	
	private GameObject [] allLandings; //arrray of all gamedObjects added by tag; tag in prefab
	private GameObject [] allSteps;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (Input.GetMouseButtonDown(0)){
			worldPoint = new Vector3(Random.Range(1,25),Random.Range(1,25),Random.Range(1,25) );
			
			landing = Instantiate (landing, worldPoint, Quaternion.identity) as GameObject;
			landing.gameObject.name = "landing";
			landing.transform.parent = transform;
			
			allLandings = GameObject.FindGameObjectsWithTag("landing");
			
			// build stair from prev to next landing.
			for (int j = 0; j < allLandings.Length; j++){ 
				// this is rebuilding all steps always. how to have step loop only between current indexes? is this bc of void update? coroutines?
				// scales about center rather than reset and messes with origin?
				stairStart = allLandings[j].transform.position;
				stairEnd = allLandings[j+1].transform.position;
				
				// check for stair direction & correct offset relative to landing
				if (stairEnd.y > stairStart.y ){
					y1 = stairStart.y + landing.transform.localScale.y/2 + step.transform.localScale.y/2;
					z1 = stairStart.z + landing.transform.localScale.z/2 + step.transform.localScale.z/2;
					y2 = stairEnd.y - landing.transform.localScale.y/2 - step.transform.localScale.y/2; 
					z2 = stairEnd.z - landing.transform.localScale.z/2 - step.transform.localScale.z/2;
				} else {
					y1 = stairStart.y - landing.transform.localScale.y/2 - step.transform.localScale.y/2;
					z1 = stairStart.z + landing.transform.localScale.z/2 + step.transform.localScale.z/2;
					y2 = stairEnd.y + landing.transform.localScale.y/2 + step.transform.localScale.y/2; 
					z2 = stairEnd.z - landing.transform.localScale.z/2 - step.transform.localScale.z/2;
				}
				
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