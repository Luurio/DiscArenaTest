using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleTakeDamage : MonoBehaviour
{
    public float health;
    float startHealth;

    [SerializeField] GameObject healthBar;

    GameManager gameManager;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        startHealth = health;

        slider = healthBar.GetComponent<Slider>();
        healthBar.SetActive(false);
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
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
