using UnityEngine;
using UnityEngine.SceneManagement;

public class TankHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public bool isPlayer = false;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"{gameObject.name} HP: {maxHealth}");
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took damage. HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        Debug.Log($"{gameObject.name} health reset to {maxHealth}");
    }

    void Die()
    {
        if (isPlayer)
        {
            Debug.Log("☠ Player died. Reloading scene...");
            
            Time.timeScale = 1f; // Make absolutely sure game isn't paused
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
