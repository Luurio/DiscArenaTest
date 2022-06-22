using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestVictory : MonoBehaviour
{
    [SerializeField, Range(0,1)] float slowMotionTimeScale;

    [SerializeField] float camPositionLerpTime;
    [SerializeField] AnimationCurve camPositionCurve;

    [SerializeField] Transform camEndTransform;
    [SerializeField] Transform camStartTransform;
    //Transform camStartTransform;
    Transform camTransform;
    

    float startTimeScale;
    float startFixedDeltaTime;

    GameObject victoryScreen;
    Animator anim;
    Game_Manager gameManager;

    bool isInEndPosition;
    bool levelEnded;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
        victoryScreen = GameObject.FindGameObjectWithTag("VictoryScreen").transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();

        camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camStartTransform.position = camTransform.position;
        camStartTransform.rotation = camTransform.rotation;
       
        startTimeScale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    public void StartChestVictory()
    {
        if (levelEnded) return;

        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimeScale;

        anim.SetTrigger("ChestOpen");

        isInEndPosition = true;
        StartCoroutine(LerpPosition(camTransform, camEndTransform));
    }

    void MoveCameraBack()
    {
        isInEndPosition = false;

        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;

        StartCoroutine(LerpPosition(camTransform, camStartTransform));
    }
    
    void EndLevel()
    {
        victoryScreen.SetActive(true);
        gameManager.levelEnded = levelEnded = true;
    }
    

    IEnumerator LerpPosition(Transform startTransform, Transform endTransform)
    {
        float time = 0;
        Vector3 startPosition = startTransform.position;
        Vector3 endPosition = endTransform.position;

        Quaternion startRotation = startTransform.rotation;
        Quaternion endRotation = endTransform.rotation;

        // Lerp 
        while (time < camPositionLerpTime)
        {
            camTransform.position = Vector3.Lerp(startPosition, endPosition, camPositionCurve.Evaluate(time / camPositionLerpTime));
            camTransform.rotation = Quaternion.Lerp(startRotation, endRotation, camPositionCurve.Evaluate(time / camPositionLerpTime));
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        camTransform.position = endPosition;
        camTransform.rotation = endRotation;

        yield return new WaitForSecondsRealtime(1);
        

        if(isInEndPosition)
        {
            MoveCameraBack();
        }
        if (!isInEndPosition)
        {
            yield return new WaitForSecondsRealtime(1);

            EndLevel();
        }
        
    }
}
