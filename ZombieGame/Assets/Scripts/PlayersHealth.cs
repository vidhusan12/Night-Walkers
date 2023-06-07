using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    private bool isRegenerating = false;
    private bool isBeingHit = false;

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

        else if (!isRegenerating && !isBeingHit)
        {
            isRegenerating = true;
            Invoke("RegenerateHealth", 3f);
        }
        isBeingHit = true;
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

    void StopBeingHit()
    {
        isBeingHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(15);
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            StopBeingHit();
        }
    }
}
