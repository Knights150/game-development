using UnityEngine;
using UnityEngine.SceneManagement;

public class House : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    [Header("House Type")]
    public bool isPlayerHouse = false; // Set true for player's house
    public bool isEnemyHouse = false;  // Set true for enemy's house

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount, string shooterTag)
    {
        currentHealth -= amount;
        Debug.Log($"{(isPlayerHouse ? "Player's" : "Enemy's")} House hit! HP left: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log($"{(isPlayerHouse ? "Player's" : "Enemy's")} House destroyed!");

            if (isPlayerHouse && shooterTag == "Enemy")
            {
                Debug.Log("💀 Enemy destroyed the player's house. Restarting level...");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (isEnemyHouse && shooterTag == "Player")
            {
                Debug.Log("🎉 Player destroyed the enemy's house. Advancing level...");
                int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
                if (nextIndex < SceneManager.sceneCountInBuildSettings)
                    SceneManager.LoadScene(nextIndex);
                else
                    Debug.Log("🏆 Game complete!");
            }

            Destroy(gameObject);
        }
    }
}
