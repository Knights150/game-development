using UnityEngine;

public class BulletArc : MonoBehaviour
{
    void Start()
    {
        // Force applied in EnemyA.cs
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
