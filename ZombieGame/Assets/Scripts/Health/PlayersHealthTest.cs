using UnityEngine;
using UnityEngine.Assertions;
using NUnit.Framework;
using UnityEngine.TestTools;

public class PlayersHealthTest
{
    [UnityTest]
    public IEnumerator TakeDamage_ReduceCurrentHealth()
    {
        // Arrange
        GameObject gameObject = new GameObject();
        PlayersHealth playersHealth = gameObject.AddComponent<PlayersHealth>();
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        playersHealth.healthBar = healthBar;
        playersHealth.currentHealth = 100;
        int damage = 20;

        // Act
        playersHealth.TakeDamage(damage);
        yield return null; // Wait for one frame to update the health value

        // Assert
        Assert.AreEqual(80, playersHealth.currentHealth);

        // Clean up
        Object.DestroyImmediate(gameObject);
    }

    [UnityTest]
    public IEnumerator TakeDamage_DieWhenHealthReachesZero()
    {
        // Arrange
        GameObject gameObject = new GameObject();
        PlayersHealth playersHealth = gameObject.AddComponent<PlayersHealth>();
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        playersHealth.healthBar = healthBar;
        playersHealth.currentHealth = 10;
        int damage = 10;

        // Act
        playersHealth.TakeDamage(damage);
        yield return null; // Wait for one frame to update the health value

        // Assert
        Assert.AreEqual(0, playersHealth.currentHealth);

        // Clean up
        Object.DestroyImmediate(gameObject);
    }

    [UnityTest]
    public IEnumerator TakeDamage_RegenerateHealthAfterDelay()
    {
        // Arrange
        GameObject gameObject = new GameObject();
        PlayersHealth playersHealth = gameObject.AddComponent<PlayersHealth>();
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        playersHealth.healthBar = healthBar;
        playersHealth.currentHealth = 30;
        int damage = 15;

        // Act
        playersHealth.TakeDamage(damage);
        yield return null; // Wait for one frame to update the health value

        // Assert
        Assert.AreEqual(30, playersHealth.currentHealth); // Health should remain the same immediately after taking damage
        Assert.IsTrue(playersHealth.isRegenerating); // Regeneration flag should be set to true

        // Clean up
        Object.DestroyImmediate(gameObject);
    }

    [UnityTest]
    public IEnumerator RegenerateHealth_RestoreHealthToMax()
    {
        // Arrange
        GameObject gameObject = new GameObject();
        PlayersHealth playersHealth = gameObject.AddComponent<PlayersHealth>();
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        playersHealth.healthBar = healthBar;
        playersHealth.currentHealth = 50;
        playersHealth.maxHealth = 100;

        // Act
        playersHealth.RegenerateHealth();
        yield return null; // Wait for one frame to update the health value

        // Assert
        Assert.AreEqual(100, playersHealth.currentHealth); // Health should be restored to the maximum value
        Assert.IsFalse(playersHealth.isRegenerating); // Regeneration flag should be set to false

        // Clean up
        Object.DestroyImmediate(gameObject);
    }
}
