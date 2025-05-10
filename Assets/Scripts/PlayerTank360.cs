using UnityEngine;
using System.Collections;

public class PlayerTank360 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turretRotationSpeed = 100f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform turret;
    public Rigidbody2D tankRb; // assign TankBody's Rigidbody2D here

    private Vector2 moveInput;

    // Idle damage system
    private float idleTimer = 0f;
    public float idleThreshold = 5f; // Seconds before damage

    void Start()
    {
        Time.timeScale = 1f; // Ensure time is running
        idleTimer = 0f;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        // Rotate turret
        if (Input.GetKey(KeyCode.J))
            turret.Rotate(0f, 0f, turretRotationSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.K))
            turret.Rotate(0f, 0f, -turretRotationSpeed * Time.deltaTime);

        // Fire bullet
        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();

        // ✅ Idle damage check using Rigidbody2D.linearVelocity
        if (tankRb != null && tankRb.linearVelocity.magnitude < 0.01f)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleThreshold)
            {
                TankHealth health = GetComponent<TankHealth>();
                if (health != null)
                {
                    health.TakeDamage(1);
                    Debug.Log("💥 Player took damage for being idle!");
                }
                idleTimer = 0f;
            }
        }
        else
        {
            idleTimer = 0f;
        }
    }

    void FixedUpdate()
    {
        if (tankRb != null)
        {
            tankRb.linearVelocity = moveInput * moveSpeed;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
