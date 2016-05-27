using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    public Text coolText;
    public Text goodText;
    public Text badText;
    public Text maxcomboText;

    private void OnSlideDetected(object sender, MotionDetectEventArgs e)
    {
        SceneManager.LoadScene("Title_CHURHYTHM");
    }

    public void Start()
    {
        maxcomboText.text = "Max Combo: " + ScoreTransporter.maxCombo;
        coolText.text = "Cool: " + ScoreTransporter.coolCount;
        goodText.text = "Good: " + ScoreTransporter.goodCount;
        badText.text = "Bad: " + ScoreTransporter.badCount;

        GetComponent<MotionDetector>().YSlide += OnSlideDetected;
    }

    public void OnDisable()
    {
        GetComponent<MotionDetector>().YSlide -= OnSlideDetected;
    }
}
