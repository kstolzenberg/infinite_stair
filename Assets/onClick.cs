using UnityEngine;
using System.Collections;

public class onClick : MonoBehaviour {
//trial for click behavior
// this doesn't work out of the box?
//	void OnMouseDown(){
//		GameObject ball = GameObject.CreatePrimitive (PrimitiveType.Sphere);
//		ball.transform.position = new Vector3 (Random.Range (-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
//		Debug.Log ("A click");
//	}

	// Use this for initialization
	void Start () {
	
	}

	private Vector3 newPos;

	// Update is called once per frame
	// this method works to create new game objects!
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			GameObject ball = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			ball.transform.position = new Vector3 (Random.Range (-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
			ball.transform.localScale = new Vector3(Random.Range (-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
			ball.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value); 
			ball.transform.localRotation = Quaternion.Euler(0f, 30f*Time.deltaTime, 0f); //doesn't work
			//Debug.Log (ball.transform.position);
		}

		if (Input.GetMouseButtonDown(0)){
			// this runs but you can't find the ball? there doesn't seem to be a z-position? 
			//they are getting placed vertically and only varying by x/y. the scale is crazy too - how to control?
			Debug.Log("current mouse:"+Input.mousePosition); 
			Vector3 newPos = Input.mousePosition;
			//Vector3 newPosCam = Camera.main.ScreenToWorldPoint(Input.mousePosition); //this doesn't work any better!
			GameObject ball = GameObject.CreatePrimitive (PrimitiveType.Cube);
			ball.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);//still no z?
			ball.transform.localScale = new Vector3(30f,30f,30f);

		}
	
	}
}
