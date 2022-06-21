using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuckSelectionBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject puck;
    GameManager gameManager;
    Button btn;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        btn = GetComponent<Button>();
       // btn.onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {
        
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gameManager.puckMovement.DestroyPuck();
        gameManager.puckMovement.InstantiateNewPuck(puck);

        gameManager.puckMovement.uiButtonPress = true;
        gameManager.puckMovement.isShooting = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //gameManager.puckMovement.uiButtonPress = false;
        gameManager.puckMovement.canShoot = true;
        
    }


}
