using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    private bool isRegenerating = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

        else if (!isRegenerating)
        {
            isRegenerating = true;
            Invoke("RegenerateHealth", 3f);
        }
    }

    void RegenerateHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        isRegenerating = false;
    }

    void Die()
    {
        Time.timeScale = 0;

        Debug.Log("Player has died");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(15);
        }
    }
}
