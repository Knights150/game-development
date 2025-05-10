using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // [0] = PowerUp1, [1] = PowerUp2, [2] = PowerUp3
    public PlayerPowerControl playerPowerControl;
    public EndMenuManager endMenuManager;

    public float spawnRangeX = 16f;
    public float spawnRangeY = 8f;

    private bool power1Spawned = false;
    private bool power2Spawned = false;
    private bool power3Spawned = false;
    private bool victoryTriggered = false;

    void Start()
    {
        if (playerPowerControl == null)
        {
            playerPowerControl = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerPowerControl>();
        }

        TrySpawnNextPowerUp();
        InvokeRepeating(nameof(CheckConditions), 1f, 2f);
    }

    private void CheckConditions()
    {
        TrySpawnNextPowerUp();
        CheckVictoryCondition();
    }

    public void TrySpawnNextPowerUp()
    {
        if (!playerPowerControl) return;

        if (!playerPowerControl.hasPower1 && !power1Spawned)
        {
            SpawnPowerUp(1);
            power1Spawned = true;
        }
        else if (playerPowerControl.hasPower1 && !playerPowerControl.hasPower2 && !power2Spawned)
        {
            int remainingA = GameObject.FindGameObjectsWithTag("EnemyA").Length;
            Debug.Log($"EnemyA remaining: {remainingA}");
            if (remainingA == 0)
            {
                SpawnPowerUp(2);
                power2Spawned = true;
            }
        }
        else if (playerPowerControl.hasPower2 && !playerPowerControl.hasPower3 && !power3Spawned)
        {
            int remainingB = GameObject.FindGameObjectsWithTag("EnemyB").Length;
            Debug.Log($"EnemyB remaining: {remainingB}");
            if (remainingB == 0)
            {
                SpawnPowerUp(3);
                power3Spawned = true;
            }
        }
    }

    private void SpawnPowerUp(int id)
    {
        if (id < 1 || id > powerUpPrefabs.Length)
        {
            Debug.LogWarning($"❌ Invalid powerUpID: {id}");
            return;
        }

        Vector3 spawnPos = new Vector3(
            Random.Range(-spawnRangeX, spawnRangeX),
            Random.Range(-spawnRangeY, spawnRangeY),
            0f
        );

        Instantiate(powerUpPrefabs[id - 1], spawnPos, Quaternion.identity);
        Debug.Log($"✨ Spawned PowerUp {id} at {spawnPos}");
    }

    private void CheckVictoryCondition()
    {
        if (victoryTriggered || !playerPowerControl) return;

        if (playerPowerControl.hasPower3)
        {
            int remainingC = GameObject.FindGameObjectsWithTag("EnemyC").Length;
            Debug.Log($"EnemyC remaining: {remainingC}");

            if (remainingC == 0)
            {
                victoryTriggered = true;
                Debug.Log("🏆 All enemies defeated and all powerups collected!");
                if (endMenuManager != null)
                {
                    endMenuManager.ShowEndMenu();
                }
                else
                {
                    Debug.LogWarning("EndMenuManager not assigned!");
                }
            }
        }
    }
}
