using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player.
/// </summary>
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public string HorizontalAxis;
    public string VerticalAxis;
    public string RotateAxis;

    public float MoveSpeed;
    public float RotateSpeed;
    public float FloatHeight;

    private Vector2 movementVector;
    private Vector3 groundNormal;

    private Collider collider;

    public Transform CurrentPlanet;
    public bool InAtmosphere;

    private bool grounded;

    // Awake.
    private void Awake()
    {
        Instance = this;
    }

    // Start.
    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update.
    private void Update()
    {
        movementVector.x = Input.GetAxisRaw(HorizontalAxis);
        movementVector.y = Input.GetAxisRaw(VerticalAxis);

        FloatHeight = Mathf.Sin(Time.time) + 3;
    }

    private void FixedUpdate()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, CurrentPlanet.position - transform.position, out hit))
        {
            groundNormal = (transform.position - hit.point).normalized;
        }
        Debug.DrawLine(transform.position, hit.point);

        Vector3 targetPos = hit.point + (groundNormal * FloatHeight);
        targetPos += transform.forward * movementVector.y;
        targetPos += transform.right * movementVector.x;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, MoveSpeed);

        Quaternion rot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 0.1f);
        transform.Rotate(new Vector3(0, Input.GetAxisRaw(RotateAxis) * RotateSpeed, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("atmosphere"))
        {
            InAtmosphere = true;
            CurrentPlanet = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("atmosphere")) InAtmosphere = false;
    }
}