using UnityEngine;

public class MovingHouse : MonoBehaviour
{
    public Transform leftLimit;
    public Transform rightLimit;
    public float speed = 2f;

    private bool movingRight = true;

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightLimit.position.x)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= leftLimit.position.x)
                movingRight = true;
        }
    }
}
