//this gets attached to a new empty game object. add cammera and prefabs in GUI.
using UnityEngine;
using System.Collections;

public class stair3 : MonoBehaviour {
	
	public Camera raycastCamera; // always use the same camera
	public float raycastDistance = 50.0f; // this sets the plane at 50f from the camera! vertically. eventually make random

	public GameObject landing;
	public GameObject step;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	
		if (Input.GetMouseButtonDown(0)){
		
			Vector3 screenPositionWithDistance = Input.mousePosition;
			screenPositionWithDistance.z = raycastDistance;
			// Debug.Log("screenpt:"+screenPositionWithDistance); // this ends up in correct plane but wrong units? 
			Vector3 worldPoint = raycastCamera.ScreenToWorldPoint(screenPositionWithDistance); 
			// http://answers.unity3d.com/questions/199068/problem-with-screentoworldpoint-and-touches-c.html 
			// Debug.Log("worldpt:"+worldPoint); // the changing z is actaully the surface of a sphere from the camera point!!

			landing = Instantiate (landing, worldPoint, Quaternion.identity) as GameObject;
			landing.gameObject.name = "landing";
			landing.transform.parent = transform;

			//if previous landing, build stair from prev to next.
			if (transform.childCount > 1){
				Vector3 stairStart = this.gameObject.transform.GetChild (0).transform.position;
				Vector3 stairEnd = this.gameObject.transform.GetChild (1).transform.position;
				float numSteps = 10; //explicit and size changes
				//actually want to start offset and end offset - you are drawing stairs between the first and last step that correct relative to landing
				float y1 = stairStart.y + landing.transform.localScale.y/2 + step.transform.localScale.y/2;
				float z1 = stairStart.z + landing.transform.localScale.z/2 + step.transform.localScale.z/2;
				float y2 = stairEnd.y - landing.transform.localScale.y/2 - step.transform.localScale.y/2; 
				float z2 = stairEnd.z - landing.transform.localScale.z/2 - step.transform.localScale.z/2;
				float stepHeight = (y2 - y1) / numSteps;
				float stepWidth = (z2 - z1) / numSteps;

				for (int i = 0; i <= numSteps; i++){
					step = Instantiate (step, new Vector3 (stairStart.x, y1, z1), Quaternion.identity) as GameObject;
					step.transform.localScale = new Vector3(5,stepHeight,stepWidth); //scales about center rather than reset :<
					step.gameObject.name = "step"; 
					step.transform.parent = transform;
					y1 += stepHeight;
					z1 += stepWidth;
				}
			}

			//now store new landing as prev landing and redraw
			//stairStart = stairStart = this.gameObject.transform.GetChild (i).transform.position;
			//stairEnd = this.gameObject.transform.GetChild (i++).transform.position;

		}
	
	}
}