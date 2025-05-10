using UnityEngine;
using UnityEngine.SceneManagement;

public class TankHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Player Settings")]
    public bool isPlayer = false;

    [Header("UI Elements")]
    public SpriteRenderer[] healthSegments;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        Debug.Log($"{gameObject.name} HP initialized to {currentHealth}");
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthBar();

        Debug.Log($"{gameObject.name} took {amount} damage. Remaining HP: {currentHealth}");

        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        Debug.Log($"{gameObject.name} health reset to {maxHealth}");
    }

    private void Die()
    {
        if (isPlayer)
        {
            Debug.Log("☠ Player died. Reloading scene...");
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthSegments == null || healthSegments.Length == 0) return;

        for (int i = 0; i < healthSegments.Length; i++)
        {
            healthSegments[i].color = (i < currentHealth) ? Color.green : Color.red;
        }
    }
}
