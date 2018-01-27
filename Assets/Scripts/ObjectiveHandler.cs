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

    public Image CollectObjective;
    public Image TransmissionObjective;

    private float animatorPlaybackSpeedMax = 3.0f;
    private float animatorPlaybackSpeedMin = 0.5f;

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
                CollectObjective.enabled = true;
                TransmissionObjective.enabled = false;
                break;
            }
            case ObjectiveType.Transmission:
            {
                CollectObjective.enabled = false;
                TransmissionObjective.enabled = true;
                break;
            }
        }
    }
}