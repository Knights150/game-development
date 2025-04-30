using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTank : MonoBehaviour
{
    public float moveSpeed = 45f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("✅ PlayerTank Start() called on " + gameObject.name);
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("🔫 Space pressed — attempting to shoot");
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;

        if (moveDirection != Vector2.zero)
            Debug.Log("🚗 Moving in direction: " + moveDirection);
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("⚠ Missing bulletPrefab or firePoint on PlayerTank!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.shooterTag = "Player";
            Debug.Log("🎯 Bullet instantiated and tagged as Player");
        }
        else
        {
            Debug.LogWarning("⚠ Bullet prefab missing Bullet.cs script!");
        }
    }
}
