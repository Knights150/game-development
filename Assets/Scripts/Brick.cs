using UnityEngine;

public class Brick : MonoBehaviour
{
    public void TakeDamage(int amount)
    {
        Destroy(gameObject); // One hit and it breaks
    }
}
