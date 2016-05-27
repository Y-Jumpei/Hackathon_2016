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
        maxcomboText.text = "Max Combo: " + ScoreController.MaxCombo;
        coolText.text = "Cool: " + ScoreController.coolCount;
        goodText.text = "Good: " + ScoreController.goodCount;
        badText.text = "Bad: " + ScoreController.badCount;

        GetComponent<MotionDetector>().YSlide += OnSlideDetected;
    }

    public void OnDisable()
    {
        GetComponent<MotionDetector>().YSlide -= OnSlideDetected;
    }
}
