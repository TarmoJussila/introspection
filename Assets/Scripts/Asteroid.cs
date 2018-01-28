using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody rb;

    public GameObject DeathParticles;
    public Transform projector;

    public float DeathDamageRadius = 7f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        projector.position = transform.position + rb.velocity.normalized * -4;
        projector.forward = rb.velocity.normalized;
    }

    private void OnCollisionEnter(Collision coll)
    {
        var cols = Physics.OverlapSphere(transform.position, DeathDamageRadius);
        foreach (var v in cols)
            if (v.CompareTag("Player"))
                v.SendMessage("HitByMeteor");
        GameObject g = (GameObject)Instantiate(DeathParticles, coll.contacts[0].point, Quaternion.identity);
        Destroy(gameObject);
    }
}