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
        // Gets called when hiting an Obstacle
        if (collision.gameObject.layer == 7)
        {
            reflection = Vector3.Reflect(collision.relativeVelocity, collision.GetContact(0).normal);

            force = collision.relativeVelocity.magnitude * bounceDamp;
            
            rb.velocity = -reflection.normalized * force;
        }

        if(collision.gameObject.layer != 6)
        {
            rb.AddTorque(transform.up * Random.Range(-10f, 10f), ForceMode.Impulse);
        }
    }
}
