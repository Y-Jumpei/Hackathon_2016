using UnityEngine;

public class BeatPointBehavior : MonoBehaviour
{
    private float scale = 0.3f;

    public float defaultScale = 0.3f;

    public void Pulse()
    {
        scale = 0.4f;
    }

    public void Start()
    {

    }

    public void Update()
    {
        if (scale >= defaultScale)
        {
            transform.localScale = new Vector3(scale, transform.localScale.y, scale);
            scale -= 0.05f;
        }
    }
}
