using UnityEngine;
using System;

/// <summary>
/// Detect hand motion
/// </summary>
public class MotionDetector : MonoBehaviour
{
    private float slideThreshold = 1f;

    // hand objects
    private Transform[] palms;

    // previous positions
    private Vector3 leftPalmPosition = Vector3.zero;

    // properties
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject palm;

    // events
    public event EventHandler<EventArgs> slideLeftToRight;

    private bool DetectSlide()
    {
        var position = palm.transform.position;
        var move = position - leftPalmPosition;
        if (Math.Abs(move.x) > slideThreshold)
        {
            Debug.Log("X SLIDE DETECTED");
        }
        leftPalmPosition = position;
        return false;
    }

    public void Start()
    {
        leftPalmPosition = palm.transform.position;
    }

    public void Update()
    {
        DetectSlide();
    }
}
