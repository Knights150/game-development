using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public string shooterTag; // 🛡 Prevent hitting the shooter

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 vector3 = transform.up * speed;
        rb.linearVelocity = vector3;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!string.IsNullOrEmpty(shooterTag) && other.CompareTag(shooterTag))
            return;

        // Check for Tank
        TankHealth tank = other.GetComponentInParent<TankHealth>();
        if (tank != null)
        {
            tank.TakeDamage(1);
        }

       // Check for House
        House house = other.GetComponentInParent<House>();
    if (house != null)
    {
    house.TakeDamage(1, shooterTag);
    }


        // Check for Brick
        Brick brick = other.GetComponentInParent<Brick>();
        if (brick != null)
        {
            brick.TakeDamage(1);
        }

        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
