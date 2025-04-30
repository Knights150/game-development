using UnityEngine;

public class EnemyTank : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float stopDistance = 3.5f;
    public float shootCooldown = 2f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform turret;

    private Transform player;
    private float shootTimer;

    // Movement wobble
    public float strafeFrequency = 2f;
    public float strafeAmplitude = 1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        shootTimer = shootCooldown;
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            MoveTowardPlayerWithStrafe();
        }

        RotateTurret();

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootCooldown;
        }
    }

    void MoveTowardPlayerWithStrafe()
    {
        Vector2 toPlayer = (player.position - transform.position).normalized;

        // Add strafe using sine wave
        Vector2 strafe = Vector2.Perpendicular(toPlayer) * Mathf.Sin(Time.time * strafeFrequency) * strafeAmplitude;
        Vector2 desiredDirection = (toPlayer + strafe).normalized;

        // Obstacle avoidance
        RaycastHit2D hit = Physics2D.Raycast(transform.position, desiredDirection, 1f);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Wall"))
        {
            // Avoid by turning slightly left
            desiredDirection = Quaternion.Euler(0, 0, 45) * desiredDirection;
        }

        transform.position += (Vector3)(desiredDirection * moveSpeed * Time.deltaTime);
    }

    void RotateTurret()
    {
        if (turret != null && player != null)
        {
            Vector2 aimDir = (player.position - turret.position).normalized;
            float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90f;
            turret.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.shooterTag = "Enemy";
        }

        Debug.Log("💥 EnemyTank fired!");
    }
}
