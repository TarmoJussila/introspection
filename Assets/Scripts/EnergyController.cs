using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnergyController.
/// </summary>
public class EnergyController : MonoBehaviour
{
    public static EnergyController Instance { get; private set; }

    [Range(0.1f, 1.0f)]
    public float CurrentEnergyAmount = 1.0f;

    [Range(0.005f, 0.1f)]
    public float EnergyDecreaseTime = 0.1f;

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
        CurrentEnergyAmount -= Time.deltaTime * EnergyDecreaseTime;

        EnergyHandler.Instance.SetEnergyAmount(CurrentEnergyAmount);
	}

    // Add energy (crystal).
    public void AddEnergy()
    {
        CurrentEnergyAmount += CrystalEnergyAmount;
    }

    // Remove energy (meteoroid).
    public void RemoveEnergy()
    {
        CurrentEnergyAmount -= MeteoroidDamageAmount;
    }
}