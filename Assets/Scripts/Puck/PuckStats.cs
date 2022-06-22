using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckStats : MonoBehaviour
{
    public int damage;
    public float launchForce;
    [Range(0,2)]public float bounceDamp = 0.9f;
}
