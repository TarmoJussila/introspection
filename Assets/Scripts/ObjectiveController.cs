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

    public float DistanceCheckWaitTime = 1.0f;
    public float InitialDistanceCheckWaitTime = 3.0f;

    public float LowIndicatorDistance = 75f;
    public float MediumIndicatorDistance = 30f;
    public float HighIndicatorDistance = 10f;

    public List<GameObject> Objectives = new List<GameObject>();

    // Awake.
    private void Awake()
    {
        Instance = this;
    }

    // Start.
    private void Start()
	{
        StartCoroutine(InitialWaitTime());
	}

    private IEnumerator InitialWaitTime()
    {
        yield return new WaitForSeconds(InitialDistanceCheckWaitTime);

        StartCoroutine(CheckObjectiveDistance());
    }

    // Check objective distance.
    private IEnumerator CheckObjectiveDistance()
    {
        float closestDistance = float.MaxValue;

        foreach (var objective in Objectives)
        {
            if (objective.activeSelf)
            {
                float distance = Vector3.Distance(Player.Instance.transform.position, objective.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                }
            }
        }

        if (closestDistance < HighIndicatorDistance)
        {
            float playbackSpeed = ObjectiveHandler.Instance.AnimatorPlaybackSpeedMax;
            ObjectiveHandler.Instance.SetIndicatorSpeed(playbackSpeed);
        }
        else if (closestDistance < MediumIndicatorDistance)
        {
            float playbackSpeed = (ObjectiveHandler.Instance.AnimatorPlaybackSpeedMin + ObjectiveHandler.Instance.AnimatorPlaybackSpeedMax) / 2f;
            ObjectiveHandler.Instance.SetIndicatorSpeed(playbackSpeed);
        }
        else if (closestDistance < LowIndicatorDistance)
        {
            float playbackSpeed = ObjectiveHandler.Instance.AnimatorPlaybackSpeedMin * 2f;
            ObjectiveHandler.Instance.SetIndicatorSpeed(playbackSpeed);
        }
        else
        {
            float playbackSpeed = ObjectiveHandler.Instance.AnimatorPlaybackSpeedMin;
            ObjectiveHandler.Instance.SetIndicatorSpeed(playbackSpeed);
        }

        Debug.Log("Closest distance: " + closestDistance);

        yield return new WaitForSeconds(DistanceCheckWaitTime);

        yield return CheckObjectiveDistance();
    }
}