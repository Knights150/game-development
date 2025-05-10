using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public int maxHealth = 5;
    protected int currentHealth;
    public Transform player;
    public float speed = 2f;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected abstract void Move();
    protected abstract void Attack();

    protected virtual void Update()
    {
        Move();
        Attack();
    }
}
