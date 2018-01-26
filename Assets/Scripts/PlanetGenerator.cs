using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour {

	// Deforming vars

	public float Scale = 1;
	public float Speed = 1;
	public bool RecalculateNormals = true;

	private Vector3[] baseVertices;
	private Perlin noise = new Perlin ();

	// Planet vars

	public GameObject Planet;

	private MeshCollider collider;

	// Use this for initialization
	void Start () {

		collider = GetComponent<MeshCollider> ();

		Deform ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Deform () {

		Mesh mesh = Planet.GetComponent<MeshFilter> ().mesh;

		if (baseVertices == null) baseVertices = mesh.vertices;

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
}
