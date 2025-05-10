using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed = 10f;
    public string shooterTag; // Prevent bullet from hitting the shooter

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 vector3 = transform.up * speed;
        rb.linearVelocity = vector3;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Get the root object’s tag (useful when hitting child colliders)
        string otherRootTag = other.transform.root.tag;

        // Prevent bullet from hitting the shooter
        if (!string.IsNullOrEmpty(shooterTag) && otherRootTag == shooterTag)
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
