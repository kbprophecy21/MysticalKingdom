using UnityEngine;

public class CastleTower : MonoBehaviour
{
    [Header("Castle Settings")]
    public int maxHealth = 200;
    private int currentHealth;

    public static CastleTower Instance;

    [Header("Game Over Trigger")]
    public GameObject gameOverPanel; // Optional: Drag in from UI if you have one

    private void Awake()
    {
        // Singleton pattern for global access (optional)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Castle took {damage} damage. Current HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            HandleDestruction();
        }
    }

    private void HandleDestruction()
    {
        Debug.Log("💀 The Castle has fallen! Game Over!");

        // Optionally show game over UI
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Optionally disable all gameplay here
        Time.timeScale = 0f;

        // Destroy or disable the castle object
        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }
}
