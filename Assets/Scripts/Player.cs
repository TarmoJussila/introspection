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

	public Vector3 GroundNormal;

	public Transform CurrentPlanet;
	public bool InAtmosphere;

	public Transform LeftRaycast;
	public Transform RightRaycast;

	private bool grounded;

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
		movementVector.x = Input.GetAxisRaw(HorizontalAxis);
		movementVector.y = Input.GetAxisRaw(VerticalAxis);

		FloatHeight = Mathf.Sin(Time.time) + 3;
	}

	// Fixed update.
	private void FixedUpdate()
	{
		RaycastHit hit = new RaycastHit();

		if (Physics.Raycast(transform.position, CurrentPlanet.position - transform.position, out hit))
		{
			GroundNormal = (transform.position - hit.point).normalized;
		}

		Quaternion rot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 0.1f);
		transform.Rotate(new Vector3(0, Input.GetAxisRaw(RotateAxis) * RotateSpeed, 0));

		Vector3 targetPos = hit.point + (GroundNormal * FloatHeight);

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


		//targetPos += transform.right * movementVector.x;

		transform.position = Vector3.MoveTowards(transform.position, targetPos, MoveSpeed);

        
	}

    void OnCollisionEnter (Collision coll) {

        //if (coll.gameObject.CompareTag("Meteor")) 

    }
}