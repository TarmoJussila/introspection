using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Objective controller.
/// </summary>
public class ObjectiveController : MonoBehaviour
{
    public static ObjectiveController Instance { get; private set; }

    public int ObjectivePointAmount = 5;

    public List<GameObject> Objectives = new List<GameObject>();

    // Awake.
    private void Awake()
    {
        Instance = this;
    }

    // Start.
    private void Start()
	{
        StartCoroutine(CheckObjectiveDistance());
	}

    // Check objective distance.
    private IEnumerator CheckObjectiveDistance()
    {
        yield return null;
    }
}