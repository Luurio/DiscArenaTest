using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleTakeDamage : MonoBehaviour
{
    [SerializeField] bool isChest;
    [SerializeField] float health;
    float startHealth;

    [SerializeField] GameObject healthBar;

    GameManager gameManager;
    Slider slider;
    GameObject victoryScreen;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        startHealth = health;

        slider = healthBar.GetComponent<Slider>();
        healthBar.SetActive(false);

        if (!isChest) return;
        victoryScreen = GameObject.FindGameObjectWithTag("VictoryScreen").transform.GetChild(0).gameObject;
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {

            health = health - gameManager.activePuckStats.damage;

            slider.value = health / startHealth;

            if (startHealth > health)
            {
                healthBar.SetActive(true);
            }

            if(health <= 0)
            {
                if (isChest)
                {
                    KillChest();
                }
                else
                {
                    KillObstacle();
                }
            }
        }
    }

    void KillChest()
    {
        victoryScreen.SetActive(true);
    }

    void KillObstacle()
    {
        GameObject.Destroy(this.gameObject);
    }
}
