using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Look at camera billboard-style.
/// </summary>
public class LookAtCamera : MonoBehaviour
{
    // Update.
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}