using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

	public GameObject AsteroidPrefab;
	public Transform planet;

	public int firstAsteroidTime = 10;
	public float MinAsteroidFrequency = 3;
	public float MaxAsteroidFrequency = 15;
	public float MinAsteroidForce = 20;
	public float MaxAsteroidForce = 50;
	public int FloatHeight;

	private Transform player;
	private Player playerController;

	// Use this for initialization
	void Start()
	{
		player = Player.Instance.transform;
		playerController = player.gameObject.GetComponent<Player>();
		Invoke("ShootAsteroid", firstAsteroidTime);
	}
	
	// Update is called once per frame
	void Update()
	{

		transform.position = player.position + (playerController.GroundNormal * FloatHeight);
		
	}

	void ShootAsteroid()
	{
	
		GameObject asteroid = (GameObject)Instantiate(AsteroidPrefab, transform.position, Quaternion.identity);
		int force = (int)Random.Range(MinAsteroidForce, MaxAsteroidForce);
		print("shooting asteroid with force of " + force);
		Vector3 targetPos = player.position;
		targetPos += player.forward * Random.Range(-5, 15);
		targetPos += player.right * Random.Range(-5, 15);
		asteroid.GetComponent<Rigidbody>().AddForce((targetPos - transform.position) * force);
	
		Invoke("ShootAsteroid", Random.Range(MinAsteroidFrequency, MaxAsteroidFrequency));

	}
}
