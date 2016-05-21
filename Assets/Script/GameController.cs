using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private MusicScorePlayer player = new MusicScorePlayer();
    private int noteSkipTime = 200;

    private MotionDetector motionDetector;
    private ScoreController scoreController;

    private BeatPointBehavior leftBeatPoint;
    private BeatPointBehavior centerBeatPoint;
    private BeatPointBehavior rightBeatPoint;

    public GameObject slidePrefab;
    public GameObject chopPrefab;

    public GameObject coolEffect;
    public GameObject goodEffect;
    public GameObject badEffect;

    private void OnNoteTiming(object sender, MusicScorePlayer.MusicScoreEventArgs e)
    {
        GameObject note;
        switch (e.Note.Type)
        {
            case MusicScorePlayer.NoteType.Chop:
                note = Instantiate(chopPrefab);
                break;
            case MusicScorePlayer.NoteType.Slide:
                note = Instantiate(slidePrefab);
                break;
            default:
                throw new InvalidOperationException("Invalid note type");
        }
        var speed = 0.05f;
        var distance = speed * noteSkipTime;
        note.transform.position = new Vector3(e.Note.X, 0, distance);
        note.transform.rotation = Quaternion.identity;
        note.name = "Note";

        var noteBehavior = note.GetComponent<NoteBehavior>();
        noteBehavior.Speed = speed;

        e.Note.NoteObject = note;
    }

    private void OnXSlideDetected(object sender, MotionDetectEventArgs e)
    {
        CheckTiming(MusicScorePlayer.NoteType.Slide, e.BeatPoint);
    }

    private void OnYSlideDetected(object sender, MotionDetectEventArgs e)
    {
        CheckTiming(MusicScorePlayer.NoteType.Chop, e.BeatPoint);
    }

    private void CheckTiming(MusicScorePlayer.NoteType noteType, int beatPoint)
    {
        switch (beatPoint)
        {
            case 0: leftBeatPoint.Pulse(); break;
            case 1: centerBeatPoint.Pulse(); break;
            case 2: rightBeatPoint.Pulse(); break;
        }

        var note = player.GetNearestNote();
        if (note != null && noteType == note.Type && beatPoint == note.BeatPoint)
        {
            if (note.IsBeated) return; // ignore this since the note has already beaten

            var distance = player.GetDistanceFromProgress(note);
            Debug.Log("Hit with distance (" + distance + ")");

            if (distance < 5)
            {
                var effect = Instantiate(coolEffect, note.NoteObject.transform.position, transform.rotation);
                Destroy(effect, 0.25f);
                Destroy(note.NoteObject);
                note.IsBeated = true;
                scoreController.AddCoolCount();
            }
            else if (distance < 10)
            {
                var effect = Instantiate(goodEffect, note.NoteObject.transform.position, transform.rotation);
                Destroy(effect, 0.25f);
                Destroy(note.NoteObject);
                note.IsBeated = true;
                scoreController.AddGoodCount();
            }
            else if (distance < 15)
            {
                var effect = Instantiate(badEffect, note.NoteObject.transform.position, transform.rotation);
                Destroy(effect, 0.25f);
                Destroy(note.NoteObject);
                note.IsBeated = true;
                scoreController.AddBadCount();
            }
        }
    }

    public void Start()
    {
        // setup music score player
        player.Load("TestMusic");
        player.NoteTiming += OnNoteTiming;
        player.Play(noteSkipTime);

        // setup motion detector
        motionDetector = FindObjectOfType<MotionDetector>();
        motionDetector.XSlide += OnXSlideDetected;
        motionDetector.YSlide += OnYSlideDetected;

        // set beat point objects
        leftBeatPoint = GameObject.Find("LeftBeatPoint").GetComponent<BeatPointBehavior>();
        centerBeatPoint = GameObject.Find("CenterBeatPoint").GetComponent<BeatPointBehavior>();
        rightBeatPoint = GameObject.Find("RightBeatPoint").GetComponent<BeatPointBehavior>();

        // etc
        scoreController = GetComponent<ScoreController>();
    }

    public void Update()
    {
        player.Update();

        // activate beat point
        leftBeatPoint.Inactivate();
        centerBeatPoint.Inactivate();
        rightBeatPoint.Inactivate();
        if (motionDetector.PalmIsInLeft)
        {
            leftBeatPoint.Activate();
        }
        if (motionDetector.PalmIsInCenter)
        {
            centerBeatPoint.Activate();
        }
        if (motionDetector.PalmIsInRight)
        {
            rightBeatPoint.Activate();
        }


        // handle key inputs
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Title");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A pressed at prograss " + player.Progress);
        }

        // autoplay
        if (false)
        {
            var _note = player.GetNearestNote();
            var distance = player.GetDistanceFromProgress(_note);
            if (distance < 1 && !_note.IsBeated)
            {
                Debug.Log("A pressed at prograss " + player.Progress);
                var effect = Instantiate(coolEffect, _note.NoteObject.transform.position, transform.rotation);
                Destroy(effect, 0.25f);
                Destroy(_note.NoteObject);
                _note.IsBeated = true;
                scoreController.AddCoolCount();
            }
        }
    }
}
