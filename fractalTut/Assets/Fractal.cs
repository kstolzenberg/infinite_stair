using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {
	// note these public variables become buttons/inputs in the GUI
	public Mesh mesh;
	public Material material;
	private int depth;
	public int maxDepth;
	public float childScale;

	private Material[,] materials;

	private void InitializeMaterials(){
		materials = new Material[maxDepth + 1, 2];
		for (int i = 0; i<=maxDepth; i++){
			float t = i / (maxDepth - 1f);
			t *= t;
			materials[i,0] = new Material(material);
			materials[i,0].color = Color.Lerp(Color.white, Color.yellow, t );
			materials[i,1] = new Material(material);
			materials[i,1].color = Color.Lerp(Color.white, Color.cyan, t );
		}
		materials [maxDepth, 0].color = Color.magenta;
		materials [maxDepth, 1].color = Color.red;
	}

	//make an array of mesh possibilties to pick from. size is size of array!
	public Mesh[] meshes;
	
	// Use this for initialization - Start is only called once before Update
	private void Start () {
		if (materials == null){
			InitializeMaterials();
		}
		//adds mesh/material input values (from abv variables) to empty game object 
		gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)]; // takes mesh and passes to mesh renderer
		gameObject.AddComponent<MeshRenderer>().material = materials[depth, Random.Range(0,2)]; // applies material to mesh
		if ( depth < maxDepth) {
			StartCoroutine(CreateChildren());
		}
	}

	// refactor with tidier arrays. note visibility, return type, name syntax. this allows more directions/more children! 
	private static Vector3[] childDirections = {
		Vector3.up,
		Vector3.right,
		Vector3.left,
		Vector3.forward,
		Vector3.back
	};

	private static Quaternion[] childOrientations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 0f, -90f),
		Quaternion.Euler(0f, 0f, 90f),
		Quaternion.Euler(90f, 0f, 0f),
		Quaternion.Euler(-90f, 0f, 0f),
	};

	public float spawnProbability;

	// add coroutines to delay the process and watch the progress
	// add local rotation to get rid of overlaps
	private IEnumerator CreateChildren(){
		// loop over vector array
		for (int i = 0; i< childDirections.Length; i++){
			if (Random.value < spawnProbability){
				yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
				new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i);
			}
		}
	}

	// method that actual creates the child
	private void Initialize (Fractal parent, int childIndex){
		spawnProbability = parent.spawnProbability;
		meshes = parent.meshes;
		materials = parent.materials;
		maxDepth = parent.maxDepth; // child will inherit parent shape, material, maxDept
		depth = parent.depth + 1; // create set local variable to parent + 1
		childScale = parent.childScale; // inherit public childscale from parent
		transform.parent = parent.transform; // nests the gameObjects as children
		transform.localScale = Vector3.one * childScale; // changes scale of each child
		transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale); // chnages z-pos of each child while considering size change
		transform.localRotation = childOrientations[childIndex]; //rotate according to Quaternion above
	}

	private void Update(){
		transform.Rotate (0f, 30f*Time.deltaTime, 0f);
	}
}
