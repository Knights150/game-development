using UnityEngine;

public class EnemyC : EnemyBase
{
    public GameObject laserPrefab;
    public GameObject warningEffect;

    private float teleportCooldown = 3f;
    private float teleportTimer;

    private float burstDuration = 3f;
    private float burstTimer;

    private float pauseDuration = 2f;
    private float pauseTimer;

    private float fireInterval = 0.1f;
    private float fireTimer;

    private bool isBursting = false;
    private bool warned = false;

    private float minX = -17f, maxX = 17f;
    private float minY = -9f, maxY = 9f;

    protected override void Start()
    {
        base.Start();
        teleportTimer = teleportCooldown;
        pauseTimer = 0f;
        fireTimer = fireInterval;
    }

    protected override void Move()
    {
        teleportTimer -= Time.deltaTime;
        if (teleportTimer <= 0f && player != null)
        {
            Vector3 offset = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
            Vector3 newPos = player.position + offset;
            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
            newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

            transform.position = newPos;
            teleportTimer = teleportCooldown;
        }

        if (player != null)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }

    protected override void Attack()
    {
        if (isBursting)
        {
            burstTimer -= Time.deltaTime;
            fireTimer -= Time.deltaTime;

            if (fireTimer <= 0f && player != null)
            {
                FireLaser();
                fireTimer = fireInterval;
            }

            if (burstTimer <= 0f)
            {
                isBursting = false;
                pauseTimer = pauseDuration;
                warned = false;
            }
        }
        else
        {
            pauseTimer -= Time.deltaTime;

            if (!warned && pauseTimer <= 0.3f && warningEffect != null)
            {
                Instantiate(warningEffect, transform.position, Quaternion.identity);
                warned = true;
            }

            if (pauseTimer <= 0f)
            {
                isBursting = true;
                burstTimer = burstDuration;
                fireTimer = 0f;
            }
        }
    }

    private void FireLaser()
    {
        if (player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);

        Vector3 sideways = Vector3.Cross(dir, Vector3.forward) * Random.Range(-0.3f, 0.3f);
        Vector3 spawnPos = transform.position + dir * 0.5f + sideways;

        Instantiate(laserPrefab, spawnPos, rotation);
    }
}
