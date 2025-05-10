using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int powerUpID = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        Transform root = other.transform.root;
        string tagName = root.tag;
        Debug.Log($"PowerUp Triggered by: {other.name}, root: {root.name}, tag: {tagName}");

        if (tagName == "Player")
        {
            PlayerPowerControl powerControl = root.GetComponent<PlayerPowerControl>();
            if (powerControl != null)
            {
                powerControl.CollectPowerUp(powerUpID);
                Debug.Log($"✔ PowerUp {powerUpID} collected by Player");
            }
            else
            {
                Debug.LogWarning("⚠ PlayerPowerControl missing on Player root object!");
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"Ignored collision with: {other.name} (tag: {tagName})");
        }
    }
}
