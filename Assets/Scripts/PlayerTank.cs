using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTank : MonoBehaviour
{
    public float moveSpeed = 15f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            Debug.LogWarning("🔇 AudioSource missing on PlayerTank!");
        else if (audioSource.clip == null)
            Debug.LogWarning("🎵 AudioSource has no AudioClip assigned!");
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    void Shoot()
    {
        Debug.Log("🚀 Shoot() called");

        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("⚠ Missing bulletPrefab or firePoint");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.shooterTag = "Player";
        }

        if (audioSource != null)
        {
            Debug.Log("🔊 Playing shooting sound");
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("🔇 No AudioSource found on PlayerTank");
        }
    }
}
