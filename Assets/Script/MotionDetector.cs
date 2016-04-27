using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Detect hand motion
/// </summary>
public class MotionDetector : MonoBehaviour
{
    /// <summary>
    /// Component to detect slide
    /// </summary>
    private class SlideDetector
    {
        private static List<SlideDetector> detectors = new List<SlideDetector>();

        public static void UpdateAll()
        {
            foreach (var detector in detectors)
            {
                detector.Update();
            }
        }

        private GameObject target;
        private Predicate<Vector3> predicate;
        private Vector3 prevPosition;

        private int detectInterval = 10;
        private int elapsedFromDetect = 0;

        public bool IsDetected { get; private set; }

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

    private float slideThreshold = 5.0f;

    // detectors
    private SlideDetector leftXSlideDetector;
    private SlideDetector rightXSlideDetector;

    // properties
    public GameObject rightPalm;
    public GameObject leftPalm;

    // events
    public event EventHandler<EventArgs> XSlide;

    public void Start()
    {
        Predicate<Vector3> xSlidePredicate = (move) => Math.Abs(move.x) > slideThreshold;
        leftXSlideDetector = new SlideDetector(leftPalm, xSlidePredicate);
        rightXSlideDetector = new SlideDetector(rightPalm, xSlidePredicate);
    }

    public void Update()
    {
        SlideDetector.UpdateAll();
        if (leftXSlideDetector.IsDetected || rightXSlideDetector.IsDetected)
        {
            if (XSlide != null) XSlide(this, EventArgs.Empty);
        }
    }
}
