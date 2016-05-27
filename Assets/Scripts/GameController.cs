using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private readonly float noteSpeed = 0.3f;

    private MusicScore musicScore = new MusicScore();
    private MusicScorePlayerBehavior player;

    private MotionDetector motionDetector;
    private ScoreController scoreController;

    private BeatPointBehavior leftBeatPoint;
    private BeatPointBehavior centerBeatPoint;
    private BeatPointBehavior rightBeatPoint;

    private bool isAutoPlayMode = false;

    public GameObject notePrefab;

    public GameObject comboText;

    public GameObject coolEffect;
    public GameObject goodEffect;
    public GameObject badEffect;

    private void OnXSlideDetected(object sender, MotionDetectEventArgs e)
    {
        CheckTiming(Note.NoteType.Slide, e.BeatPoint);
    }

    private void OnYSlideDetected(object sender, MotionDetectEventArgs e)
    {
        CheckTiming(Note.NoteType.Chop, e.BeatPoint);
    }

    private void CheckTiming(Note.NoteType noteType, int beatPoint)
    {
        switch (beatPoint)
        {
            case 0: leftBeatPoint.Pulse(); break;
            case 1: centerBeatPoint.Pulse(); break;
            case 2: rightBeatPoint.Pulse(); break;
        }

        var note = musicScore.GetNearestNote(player.Progress);
        if (note != null && noteType == note.Type && beatPoint == note.BeatPoint)
        {
            BeatNote(note);
        }
    }

    private void HandleMissedNote()
    {
        foreach (var note in musicScore.Notes)
        {
            if (!note.IsBeated && note.Time < player.Progress - 20)
            {
                note.IsBeated = true;
                Destroy(note.NoteObject);
                scoreController.ResetCombo();
                var comboTextMesh = comboText.GetComponent<TextMesh>();
                comboTextMesh.text = "";
            }
        }
    }

    private void BeatNote(Note note)
    {
        if (note.IsBeated) return; // ignore this since the note has already beaten

        var distance = player.GetDistanceFromProgress(note);
        Debug.Log("Hit with distance (" + distance + ")");

        if (distance < 5)
        {
            var effect = Instantiate(coolEffect, note.NoteObject.transform.position, transform.rotation);
            Destroy(effect, 0.25f);
            scoreController.AddCoolCount();
        }
        else if (distance < 10)
        {
            var effect = Instantiate(goodEffect, note.NoteObject.transform.position, transform.rotation);
            Destroy(effect, 0.25f);
            scoreController.AddGoodCount();
        }
        else if (distance < 15)
        {
            var effect = Instantiate(badEffect, note.NoteObject.transform.position, transform.rotation);
            Destroy(effect, 0.25f);
            scoreController.AddBadCount();
        }

        if (distance < 15)
        {
            Destroy(note.NoteObject);
            note.IsBeated = true;
            scoreController.AddCombo();
        }
        else
        {
            scoreController.ResetCombo();
        }

        // update combo text
        var comboTextMesh = comboText.GetComponent<TextMesh>();
        if (scoreController.Combo < 3)
        {
            comboTextMesh.text = "";
        }
        else
        {
            comboTextMesh.text = "COMBO\n" + scoreController.Combo;
            comboText.GetComponent<ShakeAnimationBehavior>().Play();
        }
    }

    public void Start()
    {
        // setup music score and player
        musicScore.Load("TestMusic");
        player = GetComponent<MusicScorePlayerBehavior>();
        player.SetupNotes(musicScore, notePrefab);

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

        player.Play();
    }

    public void Update()
    {
        // update notes
        player.UpdateNotes(musicScore, noteSpeed);
        HandleMissedNote();

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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            isAutoPlayMode = !isAutoPlayMode;
            Debug.Log("AutoPlay " + (isAutoPlayMode ? "enabled" : "disabled"));
        }

        // autoplay
        if (isAutoPlayMode)
        {
            var _note = musicScore.GetNearestNote(player.Progress);
            var distance = player.GetDistanceFromProgress(_note);
            if (distance < 1 && !_note.IsBeated)
            {
                Debug.Log("AutoPlay beats at prograss " + player.Progress);
                BeatNote(_note);
            }
        }

        // music has finished
		if (!player.IsPlaying)
        {
            FadeManager.Instance.LoadLevel("Result", 2.0f);
		}
    }
}
