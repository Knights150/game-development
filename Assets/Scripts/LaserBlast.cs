using UnityEngine;

public class LaserBlast : MonoBehaviour
{
    public float speed = 18f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TankHealth tank = other.GetComponentInParent<TankHealth>();
        if (tank != null && tank.isPlayer)
        {
            tank.TakeDamage(1);
        }

        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
