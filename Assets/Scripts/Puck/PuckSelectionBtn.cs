using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuckSelectionBtn : MonoBehaviour
{
    public GameObject puck;
    Game_Manager gameManager;
    Button btn;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();

        btn = GetComponent<Button>();
        btn.onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {
        if (gameManager.chestDestroyed) return;
        gameManager.puckMovement.DestroyPuck();
        gameManager.puckMovement.InstantiateNewPuck(puck);

        gameManager.puckMovement.isShooting = false;
    }
}
