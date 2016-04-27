using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private MusicScorePlayer player = new MusicScorePlayer();
    private int noteSkipTime = 200;

    private ScoreController scoreController;

    private BeatPointBehavior beatPoint;

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

    private void OnXSlideDetected(object sender, EventArgs e)
    {
        CheckTiming(MusicScorePlayer.NoteType.Slide);
    }

    private void OnYSlideDetected(object sender, EventArgs e)
    {
        CheckTiming(MusicScorePlayer.NoteType.Chop);
    }

    private void CheckTiming(MusicScorePlayer.NoteType noteType)
    {
        beatPoint.Pulse();

        var note = player.GetNearestNote();
        if (note != null && noteType == note.Type)
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
        player.Load(@"note.xml");
        player.NoteTiming += OnNoteTiming;
        player.Play(noteSkipTime);

        // setup motion detector
        var motionDetector = FindObjectOfType<MotionDetector>();
        motionDetector.XSlide += OnXSlideDetected;
        motionDetector.YSlide += OnYSlideDetected;

        // etc
        scoreController = GetComponent<ScoreController>();
        beatPoint = GameObject.Find("BeatPoint").GetComponent<BeatPointBehavior>();
    }

    public void Update()
    {
        player.Update();

        // handle key inputs
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel("Title");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            OnXSlideDetected(this, EventArgs.Empty);
        }
    }
}
