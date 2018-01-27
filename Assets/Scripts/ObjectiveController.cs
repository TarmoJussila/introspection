using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

/// <summary>
/// Objective indicator state.
/// </summary>
[System.Serializable]
public class ObjectiveIndicator
{
    [Range(0, 200)]
    public float IndicatorDistance;
    [Range(0f, 1f)]
    public float GrainIntensity;
    [Range(0, 10)]
    public float AnimatorSpeed;
}

/// <summary>
/// Objective controller.
/// </summary>
public class ObjectiveController : MonoBehaviour
{
    public static ObjectiveController Instance { get; private set; }

    public int ObjectivePointAmount = 5;
    
    public ObjectiveIndicator LowIndicator;
    public ObjectiveIndicator MediumIndicator;
    public ObjectiveIndicator HighIndicator;

    public ObjectiveIndicator DefaultIndicator;

    public float DistanceCheckWaitTime = 1.0f;
    public float InitialDistanceCheckWaitTime = 3.0f;

    public List<Objective> Objectives = new List<Objective>();

    [Header("Post Processing")]
    public PostProcessingProfile PostProcessingProfile;

    // Awake.
    private void Awake()
    {
        Instance = this;
    }

    // Start.
    private void Start()
	{
        // Initial grain settings.
        var grainSettings = PostProcessingProfile.grain.settings;
        grainSettings.intensity = DefaultIndicator.GrainIntensity;
        PostProcessingProfile.grain.settings = grainSettings;

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

        bool isAnyObjectiveAvailable = false;

        foreach (var objective in Objectives)
        {
            if (!objective.IsActivated)
            {
                float distance = Vector3.Distance(Player.Instance.transform.position, objective.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                }

                isAnyObjectiveAvailable = true;
            }
        }

        var grainSettings = PostProcessingProfile.grain.settings;

        if (closestDistance < HighIndicator.IndicatorDistance)
        {
            float playbackSpeed = HighIndicator.AnimatorSpeed;
            ObjectiveHandler.Instance.SetIndicatorSpeed(playbackSpeed);
            grainSettings.intensity = HighIndicator.GrainIntensity;
        }
        else if (closestDistance < MediumIndicator.IndicatorDistance)
        {
            float playbackSpeed = MediumIndicator.AnimatorSpeed;
            ObjectiveHandler.Instance.SetIndicatorSpeed(playbackSpeed);
            grainSettings.intensity = MediumIndicator.GrainIntensity;
        }
        else if (closestDistance < LowIndicator.IndicatorDistance)
        {
            float playbackSpeed = LowIndicator.AnimatorSpeed;
            ObjectiveHandler.Instance.SetIndicatorSpeed(playbackSpeed);
            grainSettings.intensity = LowIndicator.GrainIntensity;
        }
        else
        {
            float playbackSpeed = DefaultIndicator.AnimatorSpeed;
            ObjectiveHandler.Instance.SetIndicatorSpeed(playbackSpeed);
            grainSettings.intensity = DefaultIndicator.GrainIntensity;
        }

        PostProcessingProfile.grain.settings = grainSettings;

        Debug.Log("Closest distance: " + closestDistance + " / Objectives available: " + isAnyObjectiveAvailable);

        yield return new WaitForSeconds(DistanceCheckWaitTime);

        yield return CheckObjectiveDistance();
    }
}