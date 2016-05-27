using UnityEngine;
using System;
using System.Collections.Generic;

public class MotionDetectEventArgs : EventArgs
{
    public int BeatPoint { get; private set; }

    public MotionDetectEventArgs(int beatPoint)
    {
        BeatPoint = beatPoint;
    }
}

/// <summary>
/// Detect hand motion
/// </summary>
public class MotionDetector : MonoBehaviour
{
    /// <summary>
    /// Base class of detectors
    /// </summary>
    private class DetectorBase
    {
        protected static List<SlideDetector> detectors = new List<SlideDetector>();

        public static void UpdateAll()
        {
            foreach (var detector in detectors)
            {
                detector.Update();
            }
        }

        public bool IsDetected { get; protected set; }
    }

    /// <summary>
    /// Component to detect slide
    /// </summary>
    private class SlideDetector : DetectorBase
    {
        private GameObject target;
        private Predicate<Vector3> predicate;
        private Vector3 prevPosition;

        private int detectInterval = 3;
        private int elapsedFromDetect = 0;

        public SlideDetector(GameObject target, Predicate<Vector3> predicate)
        {
            this.target = target;
            this.predicate = predicate;
            prevPosition = target.transform.position;
            detectors.Add(this);
        }

        public void Update()
        {
            IsDetected = false;
            if (elapsedFromDetect > detectInterval)
            {
                IsDetected = predicate(prevPosition - target.transform.position);
                prevPosition = target.transform.position;
                elapsedFromDetect = 0;
            }
            elapsedFromDetect += 1;
        }
    }

    /// <summary>
    /// Threshold of finger movement distance, to detect slide motion
    /// </summary>
    private float slideThreshold = 0.8f;

    // detectors
    private SlideDetector leftXSlideDetector;
    private SlideDetector rightXSlideDetector;
    private SlideDetector leftYSlideDetector;
    private SlideDetector rightYSlideDetector;

    // game objects
    public GameObject leftFinger;
    public GameObject rightFinger;
    public GameObject leftPalm;
    public GameObject rightPalm;

    // events
    public event EventHandler<MotionDetectEventArgs> XSlide;
    public event EventHandler<MotionDetectEventArgs> YSlide;

    public bool PalmIsInLeft { get; set; }
    public bool PalmIsInCenter { get; set; }
    public bool PalmIsInRight { get; set; }

    public float LeftPalmPosition { get { return leftPalm.transform.position.x; } }
    public float RightPalmPosition { get { return rightPalm.transform.position.x; } }

    private const float outerThreshold = 3.0f;

    private bool GetPalmIsInLeft(GameObject palm)
    {
        var p = palm.transform.position;
        return -outerThreshold < p.z && p.z < outerThreshold && -outerThreshold < p.x && p.x < -0.75;
    }

    private bool GetPalmIsInCenter(GameObject palm)
    {
        var p = palm.transform.position;
        return -outerThreshold < p.z && p.z < outerThreshold && -0.75 < p.x && p.x < 0.75;
    }

    private bool GetPalmIsInRight(GameObject palm)
    {
        var p = palm.transform.position;
        return -outerThreshold < p.z && p.z < outerThreshold && 0.75 < p.x && p.x < outerThreshold;
    }

    private int GetBeatPointFromPalm(GameObject palm)
    {
        if (GetPalmIsInLeft(palm))
            return 0;
        else if (GetPalmIsInCenter(palm))
            return 1;
        else if (GetPalmIsInRight(palm))
            return 2;
        else
            return 3;
    }

    public void Start()
    {
        Predicate<Vector3> xSlidePredicate = (move) => Math.Abs(move.x) > slideThreshold;
        leftXSlideDetector = new SlideDetector(leftFinger, xSlidePredicate);
        rightXSlideDetector = new SlideDetector(rightFinger, xSlidePredicate);

        Predicate<Vector3> ySlidePredicate = (move) => move.y > slideThreshold;
        leftYSlideDetector = new SlideDetector(leftFinger, ySlidePredicate);
        rightYSlideDetector = new SlideDetector(rightFinger, ySlidePredicate);
    }

    public void Update()
    {
        DetectorBase.UpdateAll();

        if (XSlide != null)
        {
            if (leftXSlideDetector.IsDetected)
            {
                XSlide(this, new MotionDetectEventArgs(GetBeatPointFromPalm(leftPalm)));
            }
            if (rightXSlideDetector.IsDetected)
            {
                XSlide(this, new MotionDetectEventArgs(GetBeatPointFromPalm(rightPalm)));
            }
        }

        if (YSlide != null)
        {
            if (leftYSlideDetector.IsDetected)
            {
                YSlide(this, new MotionDetectEventArgs(GetBeatPointFromPalm(leftPalm)));
            }
            if (rightYSlideDetector.IsDetected)
            {
                YSlide(this, new MotionDetectEventArgs(GetBeatPointFromPalm(rightPalm)));
            }
        }

        // detect position
        PalmIsInLeft = GetPalmIsInLeft(leftPalm) || GetPalmIsInLeft(rightPalm);
        PalmIsInCenter = GetPalmIsInCenter(leftPalm) || GetPalmIsInCenter(rightPalm);
        PalmIsInRight = GetPalmIsInRight(leftPalm) || GetPalmIsInRight(rightPalm);
    }
}
