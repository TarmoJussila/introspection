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

    [Range(0f, 1f)]
    public float ProximityFill;

    [Range(0f, 1f)]
    public float InterferenceVolume;
}

/// <summary>
/// Objective controller.
/// </summary>
public class ObjectiveController : MonoBehaviour
{
    public static ObjectiveController Instance { get; private set; }

    public Vector3 closestPoint;

    public int ObjectivePointAmount = 5;

    public ObjectiveIndicator LowIndicator;
    public ObjectiveIndicator MediumIndicator;
    public ObjectiveIndicator HighIndicator;

    public ObjectiveIndicator DefaultIndicator;

    private ObjectiveIndicator currentIndicator;

    private bool checkDistance = true;

    public float InterferenceFillVariance = 0.1f;
    public float InterferenceTime = 0.05f;

    public float DistanceCheckWaitTime = 1.0f;
    public float InitialDistanceCheckWaitTime = 3.0f;

    public List<Objective> Objectives = new List<Objective>();
    public Objective FinalObjective;

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

    public void ClearObjectiveSurroundings()
    {
        foreach (Objective o in Objectives)
        {
            var cols = Physics.OverlapSphere(o.transform.position, 15);
            foreach (Collider c in cols)
            {
                if (c.CompareTag("Rock"))
                {
                    print(c.name);
                    Destroy(c.gameObject);
                }
            }
        }

        // Clear around final objective.
        var finalObjectiveColliders = Physics.OverlapSphere(FinalObjective.transform.position, 30f);
        foreach (Collider c in finalObjectiveColliders)
        {
            if (c.CompareTag("Rock"))
            {
                print(c.name);
                Destroy(c.gameObject);
            }
        }
    }

    private IEnumerator InitialWaitTime()
    {
        yield return new WaitForSeconds(InitialDistanceCheckWaitTime);

        StartCoroutine(CheckObjectiveDistance());
        StartCoroutine(SetIndicatorInterference());
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
                    closestPoint = objective.transform.position;
                }

                isAnyObjectiveAvailable = true;
            }
        }

        var grainSettings = PostProcessingProfile.grain.settings;

        if (closestDistance < HighIndicator.IndicatorDistance)
        {
            checkDistance = false;
            currentIndicator = HighIndicator;
        }
        else if (closestDistance < MediumIndicator.IndicatorDistance)
        {
            checkDistance = false;
            currentIndicator = MediumIndicator;
        }
        else if (closestDistance < LowIndicator.IndicatorDistance)
        {
            checkDistance = true;
            currentIndicator = LowIndicator;
        }
        else
        {
            checkDistance = true;
            currentIndicator = DefaultIndicator;
        }

        float playbackSpeed = currentIndicator.AnimatorSpeed;
        ObjectiveHandler.Instance.SetIndicatorProximity(playbackSpeed, currentIndicator.ProximityFill);
        grainSettings.intensity = currentIndicator.GrainIntensity;

        if (currentIndicator != DefaultIndicator)
        {
            AudioController.Instance.PlaySound(SoundType.Interference, false, 1.0f, false, currentIndicator.InterferenceVolume);
        }

        PostProcessingProfile.grain.settings = grainSettings;

        Debug.Log("Closest distance: " + closestDistance + " / Objectives available: " + isAnyObjectiveAvailable);

        if (checkDistance)
            Player.Instance.CheckNearestPoint();
        else
            ObjectiveHandler.Instance.ShowDirectionArrow(DirectionType.None);

        yield return new WaitForSeconds(DistanceCheckWaitTime);

        yield return CheckObjectiveDistance();
    }

    // Set objective indicator interference.
    private IEnumerator SetIndicatorInterference()
    {
        if (currentIndicator != null)
        {
            var currentFill = currentIndicator.ProximityFill;
            var randomFill = Random.Range(currentFill - InterferenceFillVariance, currentFill + InterferenceFillVariance);

            ObjectiveHandler.Instance.SetIndicatorProximity(currentIndicator.AnimatorSpeed, randomFill);
        }

        yield return new WaitForSeconds(InterferenceTime);

        yield return SetIndicatorInterference();
    }
}