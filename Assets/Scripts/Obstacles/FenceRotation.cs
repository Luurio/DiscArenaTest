using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceRotation : MonoBehaviour
{
    [Range(0,180)] public float rotAmount = 90;
    public float rotTime;
    public float timeBetweenRot;

    float rotStart;

    Vector3 targetRot_1;
    Vector3 targetRot_2;

    bool isLerpFinished_1;
    bool isLerpFinished_2;

    // Start is called before the first frame update
    void Start()
    {
        rotStart = transform.eulerAngles.y;

        targetRot_1 = new Vector3(transform.eulerAngles.x, rotAmount, transform.eulerAngles.z);
        targetRot_2 = new Vector3(transform.eulerAngles.x, rotStart, transform.eulerAngles.z);

        StartCoroutine(LerpFunction(Quaternion.Euler(targetRot_1), rotTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (isLerpFinished_1)
        {
            isLerpFinished_1 = false;
            StartCoroutine(LerpFunction(Quaternion.Euler(targetRot_1), rotTime));
        }

        if(isLerpFinished_2)
        {
            isLerpFinished_2 = false;
            StartCoroutine(LerpFunction(Quaternion.Euler(targetRot_2), rotTime));
        }
    }

    IEnumerator LerpFunction(Quaternion endValue, float duration)
    {
        
        float time = 0;
        Quaternion startValue = transform.rotation;
        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endValue;

        StartCoroutine(WaitAfterRotation(endValue));
    }

    IEnumerator WaitAfterRotation(Quaternion endValue)
    {
        yield return new WaitForSeconds(timeBetweenRot);

        
        if (endValue == Quaternion.Euler(targetRot_1))
        {

            isLerpFinished_2 = true;

            Debug.Log("targetRot_1");
        }

        if (endValue == Quaternion.Euler(targetRot_2))
        {
            isLerpFinished_1 = true;

            Debug.Log("targetRot_2");
        }
    }
}
