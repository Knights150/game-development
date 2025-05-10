using UnityEngine;

public class EnemyTank : EnemyBase
{
    public float stopDistance = 3.5f;
    public float shootCooldown = 2f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform turret;

    private float shootTimer;

    protected override void Start()
    {
        base.Start(); // Call base EnemyBase.Start()
        
        switch (DifficultyManager.CurrentDifficulty)
        {
            case Difficulty.Easy:
                speed = 0.5f;
                shootCooldown = 2f;
                break;
            case Difficulty.Normal:
                speed = 2f;
                shootCooldown = 2f;
                break;
            case Difficulty.Hard:
                speed = 3f;
                shootCooldown = 1f;
                break;
        }

        shootTimer = shootCooldown;
    }

    protected override void Move()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }

        RotateTurret();
    }

    protected override void Attack()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootCooldown;
        }
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
        MainSceneBullet bulletScript = bullet.GetComponent<MainSceneBullet>();

        if (bulletScript != null)
        {
            switch (DifficultyManager.CurrentDifficulty)
            {
                case Difficulty.Easy:
                    bulletScript.bulletSpeed = 3f;
                    break;
                case Difficulty.Normal:
                    bulletScript.bulletSpeed = 5f;
                    break;
                case Difficulty.Hard:
                    bulletScript.bulletSpeed = 7f;
                    break;
            }
        }

        Debug.Log("💥 EnemyTank fired!");
    }
}
