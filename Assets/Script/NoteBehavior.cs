using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public float Speed { get; set; }

    private void Move(float distance)
    {
        transform.Translate(0, 0, -Speed);
    }

    void Start()
    {
    }

    void Update()
    {
        Move(Speed);
    }
}
