using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    public Level3Controller gameController;
    private TankHealth tankHealth;

    void Start()
    {
        tankHealth = GetComponent<TankHealth>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyTank"))
        {
            // Call damage function to update health bar
            tankHealth.TakeDamage(1);

            // Still call LoseLevel to end the level (unchanged)
            gameController.LoseLevel();
        }
    }
}
