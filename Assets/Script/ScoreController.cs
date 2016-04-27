﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls score
/// </summary>
public class ScoreController : MonoBehaviour
{
    private int coolCount = 0;
    private int goodCount = 0;
    private int badCount = 0;

    public Text coolText;
    public Text goodText;
    public Text badText;

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
}