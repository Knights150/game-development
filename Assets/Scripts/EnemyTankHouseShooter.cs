using UnityEngine;

public class EnemyTankHouseShooter : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform leftLimit;
    public Transform rightLimit;
    public Transform turret;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shootCooldown = 2f;

    private bool movingRight = true;
    private float shootTimer;
    private Transform house;

    void Start()
    {
        shootTimer = shootCooldown;

        GameObject houseObj = GameObject.FindGameObjectWithTag("House");
        if (houseObj != null)
        {
            house = houseObj.transform;
        }
        else
        {
            Debug.LogError("❌ House not found! Make sure it's tagged correctly.");
        }
    }

    void Update()
    {
        MoveSideToSide();

        if (house == null) return;

        AimAtHouse();

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            ShootAtHouse();
            shootTimer = shootCooldown;
        }
    }

    void MoveSideToSide()
    {
        float step = moveSpeed * Time.deltaTime;

        if (movingRight)
        {
            transform.position += Vector3.right * step;
            if (transform.position.x >= rightLimit.position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * step;
            if (transform.position.x <= leftLimit.position.x)
            {
                movingRight = true;
            }
        }
    }

    void AimAtHouse()
    {
        if (turret != null && house != null)
        {
            Vector2 direction = (house.position - turret.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            turret.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void ShootAtHouse()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.shooterTag = "Enemy";
            }
        }
    }
}
