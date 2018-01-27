using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    private Rigidbody rb;

    public GameObject DeathParticles;
    public Transform projector;

    void Start()
    {

        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
    
        projector.position = transform.position + rb.velocity.normalized * -4;
        projector.forward = rb.velocity.normalized;
    
    }

    void OnCollisionEnter(Collision coll)
    {

        GameObject g = (GameObject)Instantiate(DeathParticles, coll.contacts[0].point, Quaternion.identity);
        Destroy(gameObject);

    }
}
