// the first of many iterations
using UnityEngine;
using System.Collections;

public class stair1 : MonoBehaviour {

	private float distance3;
	private Vector3 distance2;
	private Vector3 start;

	// Use this for initialization
	// unity y is up! 
	// how can you tidy this up too?
	void Start () {
		GameObject landing1 = GameObject.CreatePrimitive (PrimitiveType.Cube);
		landing1.transform.position = new Vector3 (0,15,10);
		landing1.transform.localScale = new Vector3(5,1,5);

		GameObject landing2 = GameObject.CreatePrimitive (PrimitiveType.Cube);
		//landing2.transform.position = new Vector3 (-1, 4, 5);
		landing2.transform.localScale = new Vector3(5,1,5);
	
		// you might have to split this up? in to vertical, x-horizontal, z-horizontal variables
		//float distance3 = Vector3.Distance (landing1.transform.position, landing2.transform.position); // this isn't quite what we want - this a vector of the stair run
		Debug.Log("this is the zposition: " + landing1.transform.position.z);

		Vector3 distance2 = (landing1.transform.position - landing2.transform.position); // this give us the position difference as x,y,z value!
		Debug.Log ("this is the diff bwtn landings: " + distance2); //yup you can dig in and access this!

		// do you need to do this on the different or just the points directly?
		Vector3 start = landing2.transform.position;
		//float offset = (landing2.transform.localScale.z/2); //this is a hack to make up for the centroid of the gameobject

		// build the steps
		// note if you have the steps at even 1 increments and you iterate up at 1 increments - you might not hit your upper landing!
		// also your offset might be confusing things??? maybe you need to scale by slope?
		for (float i = start.y; i<=distance2.y; i+=1){
			//height of step is related to z-offset? slope? this is still having wacky offset issues
			float k = (distance2.z / distance2.y);
			GameObject step = GameObject.CreatePrimitive(PrimitiveType.Cube);
			step.transform.position = new Vector3(start.x, i, start.z+i);
			Debug.Log (k);
			step.transform.localScale = new Vector3(5,1,k);
		}
	
	}


	// Update is called once per frame
	void Update () {
	
	}
}
