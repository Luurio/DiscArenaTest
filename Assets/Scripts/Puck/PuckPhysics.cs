using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckPhysics : MonoBehaviour
{
    float force;
    float bounceDamp;

    PuckStats puckStats;
    Rigidbody rb;

    Vector3 reflection;

    private void Start()
    {
        puckStats = GetComponent<PuckStats>();
        rb = GetComponent<Rigidbody>();

        force = puckStats.launchForce;
        bounceDamp = puckStats.bounceDamp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6) return;

        reflection = Vector3.Reflect(collision.relativeVelocity, collision.GetContact(0).normal);
        
        force = collision.relativeVelocity.magnitude * bounceDamp;

        rb.velocity = -reflection.normalized * force;
    }
}
