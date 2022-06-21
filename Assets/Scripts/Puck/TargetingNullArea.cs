using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TargetingNullArea : MonoBehaviour
{
    public float areaSize = 1;

    [SerializeField] Transform nullArea;

    // Update is called once per frame
    void LateUpdate()
    {
        nullArea.localScale = new Vector3(areaSize, areaSize, areaSize);
    }
}
