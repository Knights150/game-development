using UnityEngine;


public class BulletOne : MonoBehaviour
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

        // Check if bullet collides with a valid enemy and if the power-up can damage it
        EnemyBase enemy = other.GetComponent<EnemyBase>() ?? other.GetComponentInParent<EnemyBase>();
        if (enemy != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerPowerControl playerPowerControl = player != null ? player.GetComponent<PlayerPowerControl>() : null;

            if (playerPowerControl != null && playerPowerControl.CanDamageEnemy(enemy.tag))
            {
                // Apply damage only if power-up allows
                enemy.TakeDamage(1);
                Destroy(gameObject); // Destroy bullet after collision
            }
            else
            {
                Debug.Log("Bullet ignored by " + enemy.tag); // Log that the bullet was ignored
            }
        }
        else
        {
            Destroy(gameObject); // Destroy bullet if it doesn't hit a valid enemy
        }
        
    }
    
    

}
