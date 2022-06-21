using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Transform cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        
    }

    private void Update()
    {
        transform.LookAt(cameraPos, Vector3.up);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,180,0);
    }

}
