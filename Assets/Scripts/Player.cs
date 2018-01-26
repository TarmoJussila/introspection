using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public string HorizontalAxis;
	public string VerticalAxis;

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

		if (inAtmosphere) {

			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (transform.position - transform.up * 2, currentPlanet.position - transform.position, out hit)) {
				groundNormal = hit.normal;
			}

			Vector3 targetPos = hit.point + (hit.normal * FloatHeight);
			targetPos += transform.forward * movementVector.y;

			transform.position = Vector3.MoveTowards (transform.position, targetPos, MoveSpeed);

			Quaternion rot = Quaternion.FromToRotation (transform.up, hit.normal) * transform.rotation;
			transform.rotation = rot;
			transform.Rotate (new Vector3 (0, movementVector.x * RotateSpeed, 0));
		
		}

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
