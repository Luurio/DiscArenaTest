using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleTakeDamage : MonoBehaviour
{
    public PuckPhysics puck;
    [SerializeField] bool isChest;
    public float health;
    float startHealth;

    [SerializeField] GameObject healthBar;

    Game_Manager gameManager;
    ChestVictory chestVictory;
    Slider slider;

    bool isChestKilled;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
        startHealth = health;

        slider = healthBar.GetComponent<Slider>();
        healthBar.SetActive(false);
    }

   

    public void OnCollisionEnter(Collision collision)
    {
        if (gameManager.levelEnded) return;

        if(collision.gameObject.layer == 8)
        {

            health = health - gameManager.activePuckStats.damage;

            slider.value = health / startHealth;

            if (startHealth > health)
            {
                healthBar.SetActive(true);
            }

            if(!gameManager.chestDestroyed)Vibrator.Vibrate();
            

            if (health <= 0)
            {
                if (isChest)
                {
                    if (!isChestKilled)
                    {
                        gameManager.chestDestroyed = true;

                        chestVictory = GetComponent<ChestVictory>();
                        chestVictory.StartChestVictory();

                        healthBar.SetActive(false);
                        isChestKilled = true;

                    }
                }
                else
                {
                    puck = collision.gameObject.GetComponent<PuckPhysics>();
                    puck.ricocheyPuck = false;

                    GameObject.Destroy(this.gameObject);

                   // LaunchPuck(collision);
                  //  KillObstacle();
                    
                }
            }
        }
    }

    void KillObstacle()
    {
        GameObject.Destroy(this.gameObject);
    }

    void LaunchPuck(Collision collision)
    {
        puck = collision.gameObject.GetComponent<PuckPhysics>();
        puck.ricocheyPuck = false;
    }
}
