using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void Start()
    {
        GetComponent<MotionDetector>().YSlide += (sender, e) =>
        {
            FadeManager.Instance.LoadLevel("Game", 2.0f);
        };
    }

    public void FixedUpdate()
    {
        if (Input.anyKeyDown)
        {
            FadeManager.Instance.LoadLevel("Game", 2.0f);
        }
    }

}
