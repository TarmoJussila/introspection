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
    public int FloatHeight = 50;

    public float FrontOffset = 30f;
    public float RearOffset = -5f;
    public float LateralOffset = 15f;

    private Transform player;
    private Player playerController;

    // Use this for initialization
    private void Start()
    {
        player = Player.Instance.transform;
        playerController = player.gameObject.GetComponent<Player>();
        Invoke("ShootAsteroid", firstAsteroidTime);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = player.position + (playerController.GroundNormal * FloatHeight);
    }

    private void ShootAsteroid()
    {
        GameObject asteroid = (GameObject)Instantiate(AsteroidPrefab, transform.position, Quaternion.identity);
        int force = (int)Random.Range(MinAsteroidForce, MaxAsteroidForce);
        print("shooting asteroid with force of " + force);
        Vector3 targetPos = player.position;
        targetPos += player.forward * Random.Range(-RearOffset, FrontOffset);
        targetPos += player.right * Random.Range(-LateralOffset, LateralOffset);
        asteroid.GetComponent<Rigidbody>().AddForce((targetPos - transform.position) * force);

        Invoke("ShootAsteroid", Random.Range(MinAsteroidFrequency, MaxAsteroidFrequency));
    }
}