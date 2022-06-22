using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuckMovement : MonoBehaviour
{
    Game_Manager gameManager;
    public GameObject puck;

    [SerializeField] Transform targeter;
    [SerializeField] LayerMask targeterLayerMask;
    [SerializeField] Transform cancelTargetingArea;

    TargetingNullArea targetingNullArea;
    Rigidbody rb;
    GameObject launchedPuck;

    float targetingNullAreaSize;

    [HideInInspector] public bool isShooting;

    Vector3 puckRotOffset;

    LineRenderer projectileReflection;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();

        targetingNullArea = transform.parent.GetComponent<TargetingNullArea>();
        targetingNullAreaSize = targetingNullArea.areaSize;

        puckRotOffset = new Vector3(0, 180, 0);

        launchedPuck = Instantiate(puck, transform.position, Quaternion.Euler(puckRotOffset), transform);
        gameManager.activePuckStats = launchedPuck.GetComponent<PuckStats>();
        rb = launchedPuck.GetComponent<Rigidbody>();

        projectileReflection = GetComponent<LineRenderer>();
        projectileReflection.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.levelEnded) return;
        if (EventSystem.current.currentSelectedGameObject != null) return;

        if (!isShooting)
        {
            
            PositionPuckTargeter();

            if (TargeterDistanceFromPuck() > targetingNullAreaSize)
            {
                transform.LookAt(targeter, Vector3.up);

                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("Puck launched");
                    LaunchPuck();

                    isShooting = true;
                    projectileReflection.enabled = false;
                }
            }
            else
            {
                Debug.Log("Targeter to CLOSE");
                projectileReflection.enabled = false;
            }
        }
    }


    float TargeterDistanceFromPuck()
    {
       return Vector3.Distance(transform.position, targeter.position);
    }

    void PositionPuckTargeter()
    {
        if (Input.GetMouseButton(0))
        {
            projectileReflection.enabled = true;

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
        rb.AddForce(rb.transform.forward * gameManager.activePuckStats.launchForce, ForceMode.Impulse);
    }

    public void DestroyPuck()
    {
        Debug.Log("DestroyPuck");

        GameObject.Destroy(launchedPuck);
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void InstantiateNewPuck(GameObject newPuck)
    {
        launchedPuck = Instantiate(newPuck, transform.position, Quaternion.Euler(puckRotOffset), transform);

        rb = launchedPuck.GetComponent<Rigidbody>();
        gameManager.activePuckStats = launchedPuck.GetComponent<PuckStats>();

        Debug.Log("InstantiateNewPuck");
    }
}
