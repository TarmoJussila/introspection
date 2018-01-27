using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public GameObject DeathParticles;

    void OnCollisionEnter (Collision coll) {

        GameObject g = (GameObject)Instantiate(DeathParticles, coll.contacts[0].point, Quaternion.identity);
        Destroy(gameObject);

    }
}
