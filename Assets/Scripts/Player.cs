using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public string HorizontalAxis;
	public string VerticalAxis;
	public string RotateAxis;

	public float MoveSpeed;
	public float RotateSpeed;
	public float FloatHeight;

	private Vector2 movementVector;
	private Vector3 groundNormal;

	private Collider collider;

	public Transform currentPlanet;
	public bool inAtmosphere;
	private bool grounded;

	// Use this for initialization
	void Start () {

		collider = GetComponent<Collider> ();

	}
	
	// Update is called once per frame
	void Update () {

		movementVector.x = Input.GetAxisRaw (HorizontalAxis);
		movementVector.y = Input.GetAxisRaw (VerticalAxis);

		FloatHeight = Mathf.Sin (Time.time) + 3;

	}

	void FixedUpdate () {

		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast (transform.position, currentPlanet.position - transform.position, out hit)) {
			groundNormal = (transform.position - hit.point).normalized;
		}
		Debug.DrawLine (transform.position, hit.point);

		Vector3 targetPos = hit.point + (groundNormal * FloatHeight);
		targetPos += transform.forward * movementVector.y;
		targetPos += transform.right * movementVector.x;

		transform.position = Vector3.MoveTowards (transform.position, targetPos, MoveSpeed);

		Quaternion rot = Quaternion.FromToRotation (transform.up, hit.normal) * transform.rotation;
		transform.rotation = Quaternion.RotateTowards (transform.rotation, rot, 0.1f);
		transform.Rotate (new Vector3 (0, Input.GetAxisRaw(RotateAxis) * RotateSpeed, 0));

	}

	void OnTriggerEnter (Collider other) {

		if (other.CompareTag ("atmosphere")) {
			inAtmosphere = true;
			currentPlanet = other.transform;
		}

	}

	void OnTriggerExit (Collider other) {

		if (other.CompareTag ("atmosphere")) inAtmosphere = false;

	}
}
