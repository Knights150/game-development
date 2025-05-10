using UnityEngine;

public class WarningEffectSelfDestruct : MonoBehaviour
{
    public float duration = 0.5f;

    void Start()
    {
        Destroy(gameObject, duration);
    }
}
