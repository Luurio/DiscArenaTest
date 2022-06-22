using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (LineRenderer))]
public class ProjectileReflection : MonoBehaviour
{
    [SerializeField] int maxReflections;
    [SerializeField] float maxLength;
    [SerializeField] float offsetFromGround;

    LineRenderer lineRenderer;
    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offsetStartPostion = new Vector3(transform.position.x, offsetFromGround, transform.position.z);
        ray = new Ray(offsetStartPostion, -transform.forward);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, offsetStartPostion);
        float remainingLength = maxLength;

        for (int i = 0; i < maxReflections; i++)
        {
            if(Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }
}
