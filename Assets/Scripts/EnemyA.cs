using UnityEngine;

public class EnemyA : EnemyBase
{
    public GameObject arcingBulletPrefab;
    public float fireRate = 1.5f;
    public float orbitSpeed = 10f;
    public float orbitRadius = 9f;

    public AudioClip shootClip; // 🎵 Assign in Inspector
    private AudioSource audioSource;

    private float angle = 0f;
    private float fireTimer;
    private Vector3 center;

    protected override void Start()
    {
        base.Start();
        center = Vector3.zero; // Orbit center
        angle = Random.Range(0f, 360f); // Random starting angle
        fireTimer = fireRate; // Initialize fire timer

        audioSource = GetComponent<AudioSource>();
    }

    protected override void Move()
    {
        angle += orbitSpeed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;
        transform.position = center + new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * orbitRadius;

        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.up = direction;
        }
    }

    protected override void Attack()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Debug.Log("EnemyA trying to fire");
            if (arcingBulletPrefab != null)
            {
                GameObject bullet = Instantiate(arcingBulletPrefab, transform.position, transform.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.gravityScale = 0f;
                    rb.AddForce(transform.up * 6f, ForceMode2D.Impulse);
                }

                // 🎯 Play shoot sound
                if (shootClip != null && audioSource != null)
                {
                    audioSource.PlayOneShot(shootClip);
                }
            }
            else
            {
                Debug.LogWarning("arcingBulletPrefab is null");
            }

            fireTimer = fireRate; // Reset timer
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, orbitRadius);
    }
}
