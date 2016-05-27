using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void Start()
    {
        GetComponent<MotionDetector>().YSlide += (sender, e) =>
        {
            SceneManager.LoadScene("Game");
        };
    }

    public void FixedUpdate()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Game");
        }
    }

}
