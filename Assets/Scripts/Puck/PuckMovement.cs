using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuckMovement : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject puck;

    [SerializeField] Transform targeter;
    [SerializeField] LayerMask targeterLayerMask;
    [SerializeField] Transform cancelTargetingArea;

    TargetingNullArea targetingNullArea;
    Rigidbody rb;
    GameObject launchedPuck;

    float targetingNullAreaSize;
     public bool canShoot = true;
     public bool isShooting;
     public bool uiButtonPress;
    Vector3 puckRotOffset;

    // Start is called before the first frame update
    void Start()
    {
        
        targetingNullArea = transform.parent.GetComponent<TargetingNullArea>();
        targetingNullAreaSize = targetingNullArea.areaSize;

        puckRotOffset = new Vector3(0, 180, 0);

        launchedPuck = Instantiate(puck, transform.position, Quaternion.Euler(puckRotOffset), transform);
        gameManager.activePuckStats = launchedPuck.GetComponent<PuckStats>();
        rb = launchedPuck.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!stopCode)
        //{
        if (!isShooting)
        {
            PositionPuckTargeter();

            if (TargeterDistanceFromPuck() > targetingNullAreaSize)
            {
                transform.LookAt(targeter, Vector3.up);

                if (!uiButtonPress)
                {
                    if (Input.GetMouseButtonUp(0) && canShoot)
                    {
                        //!EventSystem.current.IsPointerOverGameObject()
                        Debug.Log("Puck launched");
                        LaunchPuck();

                        isShooting = true;
                        canShoot = false;
                    }
                }



            }
            else
            {
                Debug.Log("Targeter to CLOSE");
            }
        }
        //}




        /* if (!canShoot)
         {
             DestroyPuck();
         }*/

        Debuging();
    }


    float TargeterDistanceFromPuck()
    {
       return Vector3.Distance(transform.position, targeter.position);
    }

    void PositionPuckTargeter()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, targeterLayerMask))
            {
                if (hit.collider != null)
                {
                    targeter.position = hit.point;
                }
            }
        }
    }

   

    void LaunchPuck()
    {
        
         
            rb.AddForce(rb.transform.forward * 30, ForceMode.Impulse);
            
        
        
    }

    public void DestroyPuck()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        Debug.Log("DestroyPuck");

        GameObject.Destroy(launchedPuck);
        transform.eulerAngles = new Vector3(0, 0, 0);

       // isShooting = false;
        //canShoot = true;
        //}
    }

    public void InstantiateNewPuck(GameObject newPuck)
    {
        launchedPuck = Instantiate(newPuck, transform.position, Quaternion.Euler(puckRotOffset), transform);

        rb = launchedPuck.GetComponent<Rigidbody>();
        gameManager.activePuckStats = launchedPuck.GetComponent<PuckStats>();
        //uiButtonPress = false;
        Debug.Log("InstantiateNewPuck");
        
    }

    void Debuging()
    {

        if (launchedPuck == null) return;
        Debug.DrawRay(transform.position, launchedPuck.transform.forward * 10, Color.red);
    }
}
