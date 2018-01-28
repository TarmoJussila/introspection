using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LookAtCamera.
/// </summary>
public class LookAtCamera : MonoBehaviour
{
	
	// Update.
	private void Update()
	{
        transform.LookAt(Camera.main.transform);
	}
}