using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnergyController.
/// </summary>
public class EnergyController : MonoBehaviour
{
    public static EnergyController Instance { get; private set; }

    public static bool IsJumping { get; set; }

    public static bool IsSprinting { get; set; }

    [Range(0.1f, 1.0f)]
    public float CurrentEnergyAmount = 1.0f;

    [Range(0.5f, 1.5f)]
    public float MaxEnergyAmount = 1.0f;

    [Range(0.005f, 0.1f)]
    public float EnergyDecreaseTime = 0.1f;

    [Range(0.005f, 0.1f)]
    public float JumpingEnergyDecreaseTime = 0.17f;

    [Range(0.005f, 0.1f)]
    public float SprintingEnergyDecreaseTime = 0.17f;

    [Range(0.005f, 0.1f)]
    public float MaximumEnergyDecreaseTime = 0.17f;

    [Range(0.01f, 0.5f)]
    public float CrystalEnergyAmount = 0.1f;

    [Range(0.01f, 1.0f)]
    public float MeteoroidDamageAmount = 0.2f;

    // Awake.
    private void Awake()
    {
        Instance = this;
    }

    // Start.
    private void Start()
    {
		
    }
	
    // Update.
    private void Update()
    {
        if (IsJumping && IsSprinting)
        {
            CurrentEnergyAmount -= Time.deltaTime * MaximumEnergyDecreaseTime;
        }
        else if (IsSprinting)
        {
            CurrentEnergyAmount -= Time.deltaTime * SprintingEnergyDecreaseTime;
        }
        else if (IsJumping)
        {
            CurrentEnergyAmount -= Time.deltaTime * JumpingEnergyDecreaseTime;
        }
        else
        {
            CurrentEnergyAmount -= Time.deltaTime * EnergyDecreaseTime;
        }

        EnergyHandler.Instance.ShowMinusSign((IsJumping || IsSprinting));

        EnergyHandler.Instance.SetEnergyAmount(CurrentEnergyAmount);
    }

    // Add energy (crystal).
    public void AddEnergy()
    {
        CurrentEnergyAmount = Mathf.Min(CurrentEnergyAmount + CrystalEnergyAmount, MaxEnergyAmount);
    }

    // Remove energy (meteoroid).
    public void RemoveEnergy()
    {
        CurrentEnergyAmount -= MeteoroidDamageAmount;
    }
}