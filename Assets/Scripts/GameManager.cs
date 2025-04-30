using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private EnemyTank[] enemies;

    [Header("Player Settings")]
    public GameObject playerPrefab;               // Assign this in Inspector
    public Transform playerSpawnPoint;            // Assign this in Inspector

    private GameObject playerObj;

    void Start()
    {
        Debug.Log("📦 Build Scenes Count: " + SceneManager.sceneCountInBuildSettings);

        // Try to find existing player in scene
        playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj == null && playerPrefab != null)
        {
            // Instantiate player from prefab
            playerObj = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
            playerObj.tag = "Player";
            Debug.Log("🆕 PlayerTank instantiated from prefab.");
        }
        else if (playerObj != null)
        {
            // Reposition and reactivate if already in scene
            playerObj.transform.position = playerSpawnPoint.position;
            playerObj.transform.rotation = Quaternion.identity;
            playerObj.SetActive(true);

            TankHealth health = playerObj.GetComponent<TankHealth>();
            if (health != null) health.ResetHealth();

            Debug.Log("✅ PlayerTank found in scene and reset.");
        }
        else
        {
            Debug.LogError("❌ No PlayerTank found or prefab not assigned!");
        }
    }

    void Update()
    {
        enemies = Object.FindObjectsByType<EnemyTank>(FindObjectsSortMode.None);

        if (enemies.Length == 0)
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("🏁 All enemies destroyed! Loading next level...");
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("🎉 No more levels! Game complete.");
            // Optional: restart or go to main menu
            // SceneManager.LoadScene(0);
        }
    }
}
