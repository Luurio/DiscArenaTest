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

    [HideInInspector] public bool ricocheyPuck = true;

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

            if (ricocheyPuck)
            {
                Debug.Log("Richochey");
                rb.velocity = -reflection.normalized * force;
            }
            else
            {
                Debug.Log("Force throu");
                rb.velocity = reflection.normalized * force;

                Debug.DrawRay(transform.position, reflection.normalized * force);
                ricocheyPuck = true;
            }
            
        }

    }
}
