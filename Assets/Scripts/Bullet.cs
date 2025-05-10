using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public string shooterTag;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * bulletSpeed;
    }

    void Update()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet hit: " + other.name + " | Tag: " + other.tag);

        string currentScene = SceneManager.GetActiveScene().name;

        // ✅ Damage Enemy
        EnemyBase enemy = other.GetComponent<EnemyBase>() ?? other.GetComponentInParent<EnemyBase>();
        if (enemy != null)
        {
            if (currentScene == "MainScene" && other.CompareTag("EnemyTank"))
            {
                enemy.TakeDamage(1);
                Destroy(gameObject);
                return;
            }

            if (currentScene == "LevelTwo" && other.CompareTag("Target"))
            {
                enemy.TakeDamage(1);
                Destroy(gameObject);
                return;
            }

            if (currentScene == "LevelThree")
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                PlayerPowerControl powerControl = player ? player.GetComponent<PlayerPowerControl>() : null;

                if (powerControl != null && powerControl.CanDamageEnemy(enemy.tag))
                {
                    enemy.TakeDamage(1);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Bullet ignored by " + enemy.tag);
                }

                return;
            }
        }

        // ✅ Damage House
        House house = other.GetComponent<House>() ?? other.GetComponentInParent<House>();
        if (house != null)
        {
            house.TakeDamage(1, shooterTag);
            Destroy(gameObject);
            return;
        }

        // ✅ Default behavior
        Destroy(gameObject);
    }
}
