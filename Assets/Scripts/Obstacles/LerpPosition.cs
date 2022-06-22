using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPosition : MonoBehaviour
{
    [SerializeField] float travelTime;
    [SerializeField] float timebetweenMoving;

    [SerializeField] AnimationCurve curve;

    [SerializeField] Transform positionTarget_1;
    Vector3 positionTarget_2;
    
    bool moveToPosition_1;
    bool moveToPosition_2;


    // Start is called before the first frame update
    void Start()
    {
        positionTarget_2 = transform.position;
        StartCoroutine(Lerp(positionTarget_1.position, travelTime));

    }
    // Update is called once per frame
    void Update()
    {

        if (moveToPosition_1)
        {
            moveToPosition_1 = false;
            StartCoroutine(Lerp(positionTarget_1.position, travelTime));
        }


        if (moveToPosition_2)
        {
            moveToPosition_2 = false;
            StartCoroutine(Lerp(positionTarget_2, travelTime));
        }
    }

    IEnumerator Lerp(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, curve.Evaluate(time / duration) );
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        StartCoroutine(WaitAfterMoving(targetPosition));

    }


    IEnumerator WaitAfterMoving(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(timebetweenMoving);

        if(targetPosition == positionTarget_1.position)
        {
            
            moveToPosition_2 = true;
        }

        if (targetPosition == positionTarget_2)
        {
            moveToPosition_1 = true;
        }
    }
}
