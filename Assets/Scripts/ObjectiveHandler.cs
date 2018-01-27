using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Objective types.
/// </summary>
public enum ObjectiveType
{
    Collect,
    Transmission
}

/// <summary>
/// Objective handler.
/// </summary>
public class ObjectiveHandler : MonoBehaviour
{
    public static ObjectiveHandler Instance { get; private set; }

    public ObjectiveType CurrentObjective;

    public Animator ObjectiveAnimator;

    public Image ObjectiveProximityFill;
    public Image TransmissionObjective;

    public float AnimatorPlaybackSpeedMax = 10.0f;
    public float AnimatorPlaybackSpeedMin = 0.5f;

    // Awake.
    private void Awake()
    {
        Instance = this;
    }

    // Start.
    private void Start()
    {
        SetObjective(CurrentObjective);
    }

    // Update.
    private void Update()
    {
    }

    // Set objective.
    public void SetObjective(ObjectiveType objectiveType)
    {
        CurrentObjective = objectiveType;

        switch (objectiveType)
        {
            case ObjectiveType.Collect:
            {
                TransmissionObjective.enabled = false;
                break;
            }
            case ObjectiveType.Transmission:
            {
                TransmissionObjective.enabled = true;
                break;
            }
        }
    }

    // Set indicator proximity. Fill amount and playback speed.
    public void SetIndicatorProximity(float playbackSpeed, float fillAmount)
    {
        ObjectiveAnimator.speed = playbackSpeed;
        ObjectiveProximityFill.fillAmount = fillAmount;
    }
}