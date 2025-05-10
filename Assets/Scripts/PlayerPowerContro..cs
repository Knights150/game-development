using UnityEngine;

public class PlayerPowerControl : MonoBehaviour
{
    public bool hasPower1 = false;
    public bool hasPower2 = false;
    public bool hasPower3 = false;

    public PowerUpManager powerUpManager; // Assign this in the Inspector

    public void CollectPowerUp(int id)
    {
        switch (id)
        {
            case 1:
                hasPower1 = true;
                Debug.Log("✅ PowerUp 1 collected!");
                break;

            case 2:
                hasPower2 = true;
                Debug.Log("✅ PowerUp 2 collected!");
                break;

            case 3:
                hasPower3 = true;
                Debug.Log("✅ PowerUp 3 collected!");
                break;

            default:
                Debug.LogWarning($"⚠️ Unknown PowerUp ID: {id}");
                break;
        }

        // Try to spawn next power-up
        if (powerUpManager != null)
        {
            powerUpManager.TrySpawnNextPowerUp();
        }
        else
        {
            Debug.LogWarning("⚠️ PowerUpManager not assigned in PlayerPowerControl!");
        }
    }

    public bool CanDamageEnemy(string enemyTag)
    {
        if (enemyTag == "EnemyA") return hasPower1;
        if (enemyTag == "EnemyB") return hasPower2;
        if (enemyTag == "EnemyC") return hasPower3;

        Debug.LogWarning($"⚠️ Unknown enemy tag: {enemyTag}");
        return false;
    }
}
