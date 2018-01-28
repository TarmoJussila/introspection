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
        transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z), transform.position);
	}
}