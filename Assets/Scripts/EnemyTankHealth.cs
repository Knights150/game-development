using UnityEngine;

public class EnemyTankHealth : MonoBehaviour
{
    public int maxHealth = 4;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"{gameObject.name} HP initialized to {maxHealth}");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} destroyed!");
        Destroy(gameObject); // Replace with explosion effect if needed
    }
}
