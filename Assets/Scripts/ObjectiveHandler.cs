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
/// Direction types.
/// </summary>
public enum DirectionType
{
    None,
    Up,
    Down,
    Left,
    Right
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

    public Image DirectionUp;
    public Image DirectionDown;
    public Image DirectionLeft;
    public Image DirectionRight;

    public float AnimatorPlaybackSpeedMax = 10.0f;
    public float AnimatorPlaybackSpeedMin = 0.5f;

    [Header("Objective Progress")]
    public RectTransform ObjectiveProgressContainer;

    public ObjectiveProgressState ObjectiveProgressStatePrefab;

    public Dictionary<int, ObjectiveProgressState> ObjectiveProgressIndicators = new Dictionary<int, ObjectiveProgressState>();

    private int currentObjectiveIndex = 0;

    // Awake.
    private void Awake()
    {
        Instance = this;
    }

    // Start.
    private void Start()
    {
        SetObjective(CurrentObjective);
        ShowDirectionArrow(DirectionType.None);
        CreateObjectiveProgressIndicators(ObjectiveController.Instance.ObjectivePointAmount);
    }

    // Set objective.
    public void SetObjective(ObjectiveType objectiveType)
    {
        CurrentObjective = objectiveType;

        switch (objectiveType)
        {
            case ObjectiveType.Collect:
            {
                //TransmissionObjective.enabled = false;
                break;
            }
            case ObjectiveType.Transmission:
            {
                //TransmissionObjective.enabled = true;
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

    // Show direction arrow of given type.
    public void ShowDirectionArrow(DirectionType directionType)
    {
        switch (directionType)
        {
            case DirectionType.None:
            {
                DirectionUp.gameObject.SetActive(false);
                DirectionDown.gameObject.SetActive(false);
                DirectionLeft.gameObject.SetActive(false);
                DirectionRight.gameObject.SetActive(false);
                break;
            }
            case DirectionType.Up:
            {
                DirectionUp.gameObject.SetActive(true);
                DirectionDown.gameObject.SetActive(false);
                DirectionLeft.gameObject.SetActive(false);
                DirectionRight.gameObject.SetActive(false);
                break;
            }
            case DirectionType.Down:
            {
                DirectionUp.gameObject.SetActive(false);
                DirectionDown.gameObject.SetActive(true);
                DirectionLeft.gameObject.SetActive(false);
                DirectionRight.gameObject.SetActive(false);
                break;
            }
            case DirectionType.Left:
            {
                DirectionUp.gameObject.SetActive(false);
                DirectionDown.gameObject.SetActive(false);
                DirectionLeft.gameObject.SetActive(true);
                DirectionRight.gameObject.SetActive(false);
                break;
            }
            case DirectionType.Right:
            {
                DirectionUp.gameObject.SetActive(false);
                DirectionDown.gameObject.SetActive(false);
                DirectionLeft.gameObject.SetActive(false);
                DirectionRight.gameObject.SetActive(true);
                break;
            }
        }
    }

    // Create objective progress indicators.
    public void CreateObjectiveProgressIndicators(int objectiveProgressAmount)
    {
        for (int i = 0; i < objectiveProgressAmount; i++)
        {
            var objectiveProgressIndicator = Instantiate(ObjectiveProgressStatePrefab, ObjectiveProgressContainer);
            objectiveProgressIndicator.ObjectiveIndex = i;

            ObjectiveProgressIndicators.Add(i, objectiveProgressIndicator);
        }
    }

    // Mark objective completed.
    public void MarkObjectiveCompleted()
    {
        if (currentObjectiveIndex <= ObjectiveController.Instance.ObjectivePointAmount)
        {
            ObjectiveProgressIndicators[currentObjectiveIndex].MarkCompleted(true);

            currentObjectiveIndex++;
        }
    }
}