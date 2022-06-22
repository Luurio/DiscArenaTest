using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuckSelectionBtn : MonoBehaviour
{
    public GameObject puck;
    GameManager gameManager;
    Button btn;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        btn = GetComponent<Button>();
        btn.onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {

        gameManager.puckMovement.DestroyPuck();
        gameManager.puckMovement.InstantiateNewPuck(puck);

        gameManager.puckMovement.isShooting = false;
    }
}
