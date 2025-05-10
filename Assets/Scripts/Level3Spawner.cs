using UnityEngine;

public class Level3Spawner : MonoBehaviour
{
    public GameObject enemyAPrefab;
    public GameObject enemyBPrefab;
    public GameObject enemyCPrefab;

    public Vector3 spawnPosA = new Vector3(-4f, 4f, 0f);
    public Vector3 spawnPosB = new Vector3(0f, 5f, 0f);
    public Vector3 spawnPosC = new Vector3(4f, 4f, 0f);

    void Start()
    {
        Instantiate(enemyAPrefab, spawnPosA, Quaternion.identity);
        Instantiate(enemyBPrefab, spawnPosB, Quaternion.identity);
        Instantiate(enemyCPrefab, spawnPosC, Quaternion.identity);
    }
}
