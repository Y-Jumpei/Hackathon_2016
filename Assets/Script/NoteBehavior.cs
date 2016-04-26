using UnityEngine;
using System.Collections;

public class NoteBehavior : MonoBehaviour
{
    public float Speed { get; set; }

    private void Move(float distance)
    {
        transform.Translate(0, 0, -Speed);
    }

    void Start()
    {
        Speed = 0.5f;
    }

    void Update()
    {
        Move(Speed);
    }
}
