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
    public float MoveSpeedSprinting;
    public float RotateSpeed;
    public float FloatHeight;
    public float JumpFloatHeight;

    public Transform FrontMeasurer;
    public Transform BackMeasurer;
    public Transform LeftMeasurer;
    public Transform RightMeasurer;

    private Transform[] measurers;

    private float currentMoveSpeed;

    private Vector2 movementVector;
    private float rotateAmount;

    public Vector3 GroundNormal;

    public Transform CurrentPlanet;
    public bool InAtmosphere;

    public Transform LeftRaycast;
    public Transform RightRaycast;

    private bool grounded;
    private bool dead = false;

    // Awake.
    private void Awake()
    {
        Instance = this;
        measurers = Vector3[FrontMeasurer, BackMeasurer, LeftMeasurer, RightMeasurer];
    }

    // Update.
    private void Update()
    {
        if (dead)
            return;

        if (EnergyController.Instance.CurrentEnergyAmount <= 0)
        {
            dead = true;
            Dead();
            movementVector = Vector3.zero;
            rotateAmount = 0;
            FloatHeight = 1;
            return;
        }

        movementVector.x = Input.GetAxisRaw(HorizontalAxis);
        movementVector.y = Input.GetAxisRaw(VerticalAxis);
        rotateAmount = Input.GetAxisRaw(RotateAxis);

        FloatHeight = Mathf.Sin(Time.time) + 3;
    }

    // Fixed update.
    private void FixedUpdate()
    {
        if (dead)
            return;
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position, CurrentPlanet.position - transform.position, out hit))
        {
            GroundNormal = (transform.position - hit.point).normalized;
        }

        Quaternion rot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 0.1f);
        transform.Rotate(new Vector3(0, rotateAmount * RotateSpeed, 0));

        Vector3 targetPos;
        if (Input.GetAxisRaw("LT") > 0)
        {
            EnergyController.Instance.IsJumping = true;
            targetPos = hit.point + (GroundNormal * JumpFloatHeight);
        }
        else
        {
            EnergyController.Instance.IsJumping = false;
            targetPos = hit.point + (GroundNormal * FloatHeight);
        }

        if (Input.GetAxisRaw("RT") > 0)
        {
            EnergyController.Instance.IsSprinting = true;
            currentMoveSpeed = MoveSpeedSprinting;
        }
        else
        {
            EnergyController.Instance.IsSprinting = false;
            currentMoveSpeed = MoveSpeed;
        }

        hit = new RaycastHit();

        if (Physics.Raycast(LeftRaycast.position, transform.forward, out hit, 3))
        {
            Debug.DrawLine(transform.position, hit.point);
        }
        else if (Physics.Raycast(RightRaycast.position, transform.forward, out hit, 3))
        {
            Debug.DrawLine(transform.position, hit.point);
        }
        else if (movementVector.y > 0)
        {
            targetPos += transform.forward * movementVector.y;
        }

        if (Physics.Raycast(transform.position, -transform.forward, out hit, 3))
        {
            Debug.DrawLine(transform.position, hit.point);
        }
        else if (movementVector.y < 0)
        {
            targetPos += transform.forward * movementVector.y;
        }

        if (Physics.Raycast(transform.position, transform.right, out hit, 3))
        {
            Debug.DrawLine(transform.position, hit.point);
        }
        else if (movementVector.x > 0)
        {
            targetPos += transform.right * movementVector.x;
        }

        if (Physics.Raycast(transform.position, -transform.right, out hit, 3))
        {
            Debug.DrawLine(transform.position, hit.point);
        }
        else if (movementVector.x < 0)
        {
            targetPos += transform.right * movementVector.x;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, currentMoveSpeed);
    }

    void CheckNearestPoint () {

        Vector3 point = ObjectiveController.Instance.closestPoint;
        string direction;
        float closestDistance = 100000;

        foreach (Transform t in measurers)
        {
            float dist = Vector3.Distance(t.position, point);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                direction = t.name;
            }
        }

        switch (direction)
        {
            case "Front":
                break;
            case "Back":
                break;
            case "Left":
                break;
            case "Right":
                break;
            default: 
                break;
        }

    }

    // On collision
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Meteor"))
            HitByMeteor();
    }

    private void HitByMeteor()
    {
        EnergyController.Instance.RemoveEnergy();
    }

    private void Dead()
    {
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Stop();
        }
        foreach (Light l in GetComponentsInChildren<Light>())
        {
            l.enabled = false;
        }
    }
}