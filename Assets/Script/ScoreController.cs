using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls score
/// </summary>
public class ScoreController : MonoBehaviour
{
    public int coolCount = 0;
    public int goodCount = 0;
    public int badCount = 0;
    public int MaxCombo = 0;

    public Text coolText;
    public Text goodText;
    public Text badText;

    public int Combo { get; private set; }

    /// <summary>
    /// Set counts to text UI components
    /// </summary>
    private void Render()
    {
        coolText.text = "Cool: " + coolCount;
        goodText.text = "Good: " + goodCount;
        badText.text = "Bad: " + badCount;
    }

    public void Start()
    {
        Render();
        Combo = 0;
    }

    public void OnDestroy()
    {
        // set scores to pass to score scene
        ScoreTransporter.coolCount = coolCount;
        ScoreTransporter.badCount = badCount;
        ScoreTransporter.goodCount = goodCount;
        ScoreTransporter.maxCombo = MaxCombo;
    }

    public void AddCoolCount()
    {
        coolCount += 1;
        Render();
    }

    public void AddGoodCount()
    {
        goodCount += 1;
        Render();
    }

    public void AddBadCount()
    {
        badCount += 1;
        Render();
    }

    public void ResetCombo()
    {
        Combo = 0;
    }

    public void AddCombo()
    {
        Combo += 1;
        if (MaxCombo < Combo)
        {
            MaxCombo = Combo;
        }
    }
}
