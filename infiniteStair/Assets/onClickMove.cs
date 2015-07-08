//this gets attached to the game object that you want to move. works with a camera as well - so long as there is an intersection
//this only works if you have something for the raycast to hit - like a plane below!
// if you keep triple-clickiing the object will move upward!
// seems that only works with the main camera too?
// how to do this out in space? spherecast?

using UnityEngine;
using System.Collections;

public class onClickMove : MonoBehaviour {
	
	Vector3 newPosition;

	// Use this for initialization
	void Start () {
		newPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		// this will move the original object
		if (Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit)){
				newPosition = hit.point;
				transform.position = newPosition;
				Debug.Log ("this is the new spot:"+newPosition);
			}
		}

		// this will create new shapes!
		if (Input.GetMouseButtonDown(1)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out hit)){
				newPosition = hit.point;
				GameObject newCube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				newCube.transform.position = new Vector3 (hit.point.x, hit.point.y, hit.point.z);
		}
	}
}
}