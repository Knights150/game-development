using UnityEngine;

public class BulletStraight : MonoBehaviour
{
    public float speed = 10f;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.up * speed;
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
