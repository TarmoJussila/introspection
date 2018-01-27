using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

	public GameObject AsteroidPrefab;
	public Transform planet;

	private Transform player;

	// Use this for initialization
	void Start () {
		player = Player.Instance.transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		
	}
}
