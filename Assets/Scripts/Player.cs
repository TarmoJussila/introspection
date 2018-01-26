using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public string HorizontalAxis;
	public string VerticalAxis;

	public float MoveSpeed;

	private Vector2 movementVector;

	private Rigidbody rb;
	private Collider collider;

	private bool InAtmosphere;

	// Use this for initialization
	void Start () {
	
		rb = GetComponent<Rigidbody> ();
		collider = GetComponent<Collider> ();

	}
	
	// Update is called once per frame
	void Update () {

		movementVector.x = Input.GetAxisRaw (HorizontalAxis);
		movementVector.y = Input.GetAxisRaw (VerticalAxis);

	}

	void FixedUpdate () {




	}
}
