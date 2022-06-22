using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public PuckMovement puckMovement;
    [HideInInspector] public PuckStats activePuckStats;

    [HideInInspector] public bool levelEnded;
    [HideInInspector] public bool chestDestroyed;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
