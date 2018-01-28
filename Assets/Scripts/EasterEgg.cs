using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    public Transform Planet;
    public float Speed;

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.RotateAround(Planet.position, -Vector3.forward, Speed);
    }
}