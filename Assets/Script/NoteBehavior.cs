using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public float Speed { get; set; }

    private void Move(float distance)
    {
        transform.Translate(0, 0, -Speed, Space.World);
    }

    void Start()
    {
    }

    void Update()
    {
        Move(Speed);

        // Destroy itself
        if (transform.position.z < -10)
        {
            Destroy(gameObject);
        }
    }
}
