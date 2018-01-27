using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deformer : MonoBehaviour
{

	[Range(0, 3)]
	public float MinRockSize;
	[Range(5, 15)]
	public float MaxRockSize;

	public float Scale = 1;
	public float Speed = 1;
	public bool RecalculateNormals = true;

	private Vector3[] baseVertices;
	private Perlin noise = new Perlin();

	public GameObject Planet;

	private MeshRenderer renderer;
	private Mesh mesh;
	private MeshCollider collider;

	// Use this for initialization
	void Start()
	{

		mesh = GetComponent<MeshFilter>().mesh;
		baseVertices = mesh.vertices;

		collider = GetComponent<MeshCollider>();

		DeformMesh();
		
	}

	void DeformMesh()
	{

		float newScale = Random.Range(MinRockSize, MaxRockSize);
		transform.localScale = new Vector3(newScale, newScale, newScale);

		var vertices = new Vector3[baseVertices.Length];

		var timex = Time.time * Speed;
		var timey = Time.time * Speed;
		var timez = Time.time * Speed;
		for (var i = 0; i < vertices.Length; i++)
		{
			var vertex = baseVertices[i];

			vertex.x += noise.Noise(timex + vertex.x, timex + vertex.y, timex + vertex.z) * Scale;
			vertex.y += noise.Noise(timey + vertex.x, timey + vertex.y, timey + vertex.z) * Scale;
			vertex.z += noise.Noise(timez + vertex.x, timez + vertex.y, timez + vertex.z) * Scale;

			vertices[i] = vertex;
		}

		mesh.vertices = vertices;

		if (RecalculateNormals)
			mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		collider.sharedMesh = mesh;

	}
}
