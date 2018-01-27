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
    public Image PlusSign;
    public Image MinusSign;

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
        SetEnergyAmount(energyFillMax);
        ShowWarning(false);
        ShowMinusSign(false);
        ShowPlusSign(false);
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

    // Show plus sign.
    public void ShowPlusSign(bool isVisible)
    {
        PlusSign.gameObject.SetActive(isVisible);
    }

    // Show minus sign.
    public void ShowMinusSign(bool isVisible)
    {
        MinusSign.gameObject.SetActive(isVisible);
    }

    // Set energy amount.
    public void SetEnergyAmount(float energyAmount)
    {
        EnergyFill.fillAmount = Mathf.Clamp(energyAmount, energyFillMin, energyFillMax);
    }
}