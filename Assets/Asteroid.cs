using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    private Rigidbody rb;

    public GameObject DeathParticles;
    public Transform projector;

    public float DeathDamageRadius = 7f;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        AudioController.Instance.PlaySound(SoundType.Meteoroid);

    }

    void FixedUpdate()
    {
    
        projector.position = transform.position + rb.velocity.normalized * -4;
        projector.forward = rb.velocity.normalized;
    
    }

    void OnCollisionEnter(Collision coll)
    {
        var cols = Physics.OverlapSphere(transform.position, DeathDamageRadius);
        foreach (var v in cols)
            if (v.CompareTag("Player"))
                v.SendMessage("HitByMeteor");
        GameObject g = (GameObject)Instantiate(DeathParticles, coll.contacts[0].point, Quaternion.identity);
        AudioController.Instance.PlaySound(SoundType.MeteoroidExplode);
        Destroy(gameObject);

    }
}
