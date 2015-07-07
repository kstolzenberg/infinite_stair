using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {
	// note these public variables become buttons/inputs in the GUI
	public Mesh mesh;
	public Material material;
	private int depth;
	public int maxDepth;
	public float childScale;
	
	// Use this for initialization - Start is only called once before Update
	private void Start () {
		//adds mesh/material input values (from abv variables) to empty game object 
		gameObject.AddComponent<MeshFilter>().mesh = mesh; // takes mesh and passes to mesh renderer
		gameObject.AddComponent<MeshRenderer>().material = material; // applies material to mesh
		// create 2 children GameObjects per parent & change location with the vector parameter
		// new == new instance
		if ( depth < maxDepth) {
			new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.up);
			new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.right);
		}
	}

	
	// method that actual creates the child
	private void Initialize (Fractal parent, Vector3 direction, Quaternion orientation){
		mesh = parent.mesh;
		material = parent.material;
		maxDepth = parent.maxDepth; // child will inherit parent shape, material, maxDept
		depth = parent.depth + 1; // create set local variable to parent + 1
		childScale = parent.childScale; // inherit public childscale from parent
		transform.parent = parent.transform; // nests the gameObjects as children
		transform.localScale = Vector3.one * childScale; // changes scale of each child
		transform.localPosition = direction * (0.5f + 0.5f * childScale); // chnages z-pos of each child while considering size change
	}
	
}

