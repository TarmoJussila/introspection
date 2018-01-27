using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// EnergyHandler.
/// </summary>
public class EnergyHandler : MonoBehaviour
{
    public static EnergyHandler Instance { get; private set; }

    public Image EnergyFill;
    public Image EnergyWarning;

    private float energyFillMax = 1.0f;
    private float energyFillMin = 0.0f;

    // Awake.
    private void Awake()
    {
        Instance = this;
    }

    // Start.
    private void Start()
	{
        ShowWarning(false);
	}

    // Update.
    private void Update()
    {
        if (EnergyFill.fillAmount < energyFillMax / 2f)
        {
            if (!EnergyWarning.gameObject.activeSelf)
            {
                ShowWarning(true);
            }
        }
        else
        {
            if (EnergyWarning.gameObject.activeSelf)
            {
                ShowWarning(false);
            }
        }
    }

    // Show warning.
    public void ShowWarning(bool isVisible)
    {
        EnergyWarning.gameObject.SetActive(isVisible);
    }
}