using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ObjectiveProgressState.
/// </summary>
public class ObjectiveProgressState : MonoBehaviour
{
    public int ObjectiveIndex = 0;

    public Image ProgressFill;

	// Start.
	private void Start()
	{
        MarkCompleted(false);
	}

    // Mark completed.
    public void MarkCompleted(bool isVisible)
    {
        ProgressFill.enabled = isVisible;
    }
}