using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour {

	// Settings

	public int spawnsPerLoop = 10;

	public bool Deform = false;
	public bool SpawnObstacles = true;

	public GameObject RockPrefab;
	public GameObject CrystalPrefab;

	// Deforming vars

	public float Scale = 1;
	public float Speed = 1;
	public bool RecalculateNormals = true;

	private Vector3[] baseVertices;
	private Perlin noise = new Perlin ();

	// Planet vars

	public GameObject Planet;

	private MeshRenderer renderer;
	private Mesh mesh;
	private MeshCollider collider;

	// Use this for initialization
	void Start () {

		if (Planet == null) Planet = gameObject;

		Mesh mesh = Planet.GetComponent<MeshFilter> ().mesh;
		baseVertices = mesh.vertices;

		collider = GetComponent<MeshCollider> ();

		if (Deform) DeformMesh ();
		if (SpawnObstacles) StartCoroutine( SpawnProps ());

	}

	void DeformMesh () {

		var vertices = new Vector3[baseVertices.Length];

		var timex = Time.time * Speed;
		var timey = Time.time * Speed;
		var timez = Time.time * Speed;
		for (var i = 0; i < vertices.Length; i++) {
			var vertex = baseVertices [i];

			vertex.x += noise.Noise (timex + vertex.x, timex + vertex.y, timex + vertex.z) * Scale;
			vertex.y += noise.Noise (timey + vertex.x, timey + vertex.y, timey + vertex.z) * Scale;
			vertex.z += noise.Noise (timez + vertex.x, timez + vertex.y, timez + vertex.z) * Scale;

			vertices [i] = vertex;
		}

		mesh.vertices = vertices;

		if (RecalculateNormals) mesh.RecalculateNormals ();
		mesh.RecalculateBounds ();
		collider.sharedMesh = mesh;

	}

	IEnumerator SpawnProps () {

		int spawnsThisLoop = 0;
		int total = 0;
		Vector3 last = Vector3.zero;

		foreach (Vector3 vert in baseVertices) {

			if (vert == last) continue;

			int chance = Random.Range (0, 100);

			if (chance < 7) {
				
				var prop = (GameObject)Instantiate (CrystalPrefab, vert * transform.localScale.x, Quaternion.identity);
				prop.transform.up = (prop.transform.position - transform.position).normalized;

			} else if (chance < 35) {

				var prop = (GameObject)Instantiate (RockPrefab, vert * transform.localScale.x, Quaternion.identity);
				//prop.transform.up = (prop.transform.position - transform.position).normalized;
				prop.transform.rotation = Random.rotation;

			}

			spawnsThisLoop++;
			if (spawnsThisLoop >= spawnsPerLoop) {
				spawnsThisLoop = 0;
				yield return null;
			}
		
		}	
	
	}
}
