using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Planet generator. Generates planet and objects on it.
/// </summary>
public class PlanetGenerator : MonoBehaviour
{
    [Header("Settings")]
    public int SpawnsPerLoop = 50;

    public bool Deform = false;
    public bool SpawnObstacles = true;

    public GameObject RockPrefab;
    public GameObject CrystalPrefab;
    public GameObject ObjectivePrefab;

    [Header("Deformation")]
    public float Scale = 1;
    public float Speed = 1;
    public bool RecalculateNormals = true;

    private List<Vector3> baseVertices = new List<Vector3>();
    private Perlin noise = new Perlin();

    [Header("Planet")]
    public GameObject Planet;

    private MeshRenderer renderer;
    private Mesh mesh;
    private MeshCollider collider;

    private Vector3 last = Vector3.zero;

    private Transform planetObjectContainer;

    // Start.
    private void Start()
    {
        if (Planet == null)
            Planet = gameObject;

        planetObjectContainer = new GameObject("PlanetObjects").transform;

        Mesh mesh = Planet.GetComponent<MeshFilter>().mesh;
        var baseVerticesArray = mesh.vertices;
        baseVertices = baseVerticesArray.ToList();

        collider = GetComponent<MeshCollider>();

        SpawnObjectives();

        if (Deform)
            DeformMesh();
        if (SpawnObstacles)
            StartCoroutine(SpawnProps());
    }

    // Deform mesh.
    private void DeformMesh()
    {
        var vertices = new Vector3[baseVertices.Count];

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

    // Spawn objectives.
    private void SpawnObjectives()
    {
        int objectiveAmount = ObjectiveController.Instance.ObjectivePointAmount;

        for (int i = 0; i < objectiveAmount; i++)
        {
            int randomIndex = Random.Range(0, baseVertices.Count);

            var randomVertice = baseVertices[randomIndex];

            var objective = (GameObject)Instantiate(ObjectivePrefab, randomVertice * transform.localScale.x, Quaternion.identity);
            objective.transform.SetParent(ObjectiveController.Instance.transform);
            objective.transform.up = (objective.transform.position - transform.position).normalized;

            baseVertices.RemoveAt(randomIndex);

            var objectiveScript = objective.GetComponentInChildren<Objective>();
            ObjectiveController.Instance.Objectives.Add(objectiveScript);
        }
    }

    // Spawn props.
    private IEnumerator SpawnProps()
    {
        int spawnsThisLoop = 0;
        int total = 0;
        List<Vector3> distinctList = baseVertices.Distinct().ToList();

        foreach (Vector3 vertice in distinctList)
        {
            if (vertice == last)
                continue;

            last = vertice;

            int chance = Random.Range(0, 100);

            if (chance < 7)
            {
                var prop = (GameObject)Instantiate(CrystalPrefab, vertice * transform.localScale.x, Quaternion.identity);
                prop.transform.SetParent(planetObjectContainer);
                prop.transform.up = (prop.transform.position - transform.position).normalized;
            }
            else if (chance < 35)
            {
                var prop = (GameObject)Instantiate(RockPrefab, vertice * transform.localScale.x, Quaternion.identity);
                prop.transform.SetParent(planetObjectContainer);
                prop.transform.rotation = Random.rotation;
            }

            spawnsThisLoop++;
            if (spawnsThisLoop >= SpawnsPerLoop)
            {
                spawnsThisLoop = 0;
                yield return null;
            }
        }
    }
}