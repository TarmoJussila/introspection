using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Input controller.
/// </summary>
public class InputController : MonoBehaviour
{
    public Rigidbody MovableRigidbody;

    // Start.
    private void Start()
    {
    }

    // Update.
    private void Update()
    {
        if (MovableRigidbody != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                MovableRigidbody.AddForce(Vector3.one);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                MovableRigidbody.AddTorque(-Vector3.one);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MovableRigidbody.AddTorque(Vector3.one);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                MovableRigidbody.AddForce(-Vector3.one);
            }
        }
    }
}