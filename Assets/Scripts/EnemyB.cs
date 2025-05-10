using UnityEngine;

public class EnemyB : EnemyBase
{
    public GameObject arcingBulletPrefab;
    public GameObject directionBurstEffect;
    public float fireCooldown = 2f;

    public float minSpeed = 1f;
    public float maxSpeed = 4f;

    public AudioClip shootClip; 
    private AudioSource audioSource;

    private float fireTimer;
    private float currentSpeed;
    private Vector2 moveDirection;
    private float directionTimer;

    private float minX = -17f, maxX = 17f, minY = -9f, maxY = 9f;

    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        PickNewDirection();
    }

    protected override void Move()
    {
        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0f) PickNewDirection();

        Vector3 newPos = transform.position + (Vector3)(moveDirection * currentSpeed * Time.deltaTime);

        if (newPos.x < minX || newPos.x > maxX)
        {
            moveDirection.x *= -1;
            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        }
        if (newPos.y < minY || newPos.y > maxY)
        {
            moveDirection.y *= -1;
            newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
        }

        transform.position = newPos;

        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.up = direction;
        }
    }

    protected override void Attack()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f && player != null)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
            Vector3 spawnPos = transform.position + dirToPlayer * 0.5f;

            GameObject bullet = Instantiate(arcingBulletPrefab, spawnPos, Quaternion.Euler(0f, 0f, angle - 90f));

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1f;
                rb.AddForce(dirToPlayer * 7f, ForceMode2D.Impulse);
            }

            // 🎵 Play shoot sound
            if (shootClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(shootClip);
            }

            fireTimer = fireCooldown;
        }
    }

    private void PickNewDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-0.5f, 0.5f);
        moveDirection = new Vector2(x, y).normalized;

        currentSpeed = Random.Range(minSpeed, maxSpeed);
        directionTimer = Random.Range(1f, 2.5f);

        if (directionBurstEffect != null)
            Instantiate(directionBurstEffect, transform.position, Quaternion.identity);

        StartCoroutine(QuickShake());
    }

    private System.Collections.IEnumerator QuickShake()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 1.1f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalScale;
    }
}
