using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnergyController.
/// </summary>
public class EnergyController : MonoBehaviour
{
    [Range(0.1f, 1.0f)]
    public float CurrentEnergyAmount = 1.0f;

    [Range(0.005f, 0.1f)]
    public float EnergyDecreaseTime = 0.1f;

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
}