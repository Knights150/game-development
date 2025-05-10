using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseVictoryManager : MonoBehaviour
{
    public House playerHouse;      // The house the enemy is trying to destroy
    public House enemyHouse;       // The house the player is trying to destroy

    private bool levelEnded = false;

    void Update()
    {
        if (levelEnded) return;

        if (enemyHouse == null)
        {
            Debug.Log("🏚️ Player's House destroyed! Restarting level...");
            levelEnded = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (playerHouse == null)
        {
            Debug.Log("🎯 Enemy House destroyed by Player! Advancing to next level...");
            levelEnded = true;
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("🎉 All levels complete!");
                // Optional: return to main menu or show win screen
            }
        }
    }
}
