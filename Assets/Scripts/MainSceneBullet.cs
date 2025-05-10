using UnityEngine;

public class MainSceneBullet : MonoBehaviour
{
    public float bulletSpeed = 10f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = transform.up * bulletSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("MainSceneBullet hit: " + other.name + " | Tag: " + other.tag);

        if (other.CompareTag("EnemyTank"))
        {
            EnemyTankHealth health = other.GetComponent<EnemyTankHealth>() 
                                   ?? other.GetComponentInParent<EnemyTankHealth>();

            if (health != null)
            {
                Debug.Log("✅ EnemyTankHealth found! Applying damage.");
                health.TakeDamage(1);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("❌ EnemyTankHealth NOT found.");
            }
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
