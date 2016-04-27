﻿using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private MusicScorePlayer player = new MusicScorePlayer();
    private int noteSkipTime = 200;

    public GameObject notePrefab;
    public GameObject coolEffect;
    public GameObject goodEffect;
    public GameObject badEffect;

    private void OnNoteTiming(object sender,
        MusicScorePlayer.MusicScoreEventArgs e)
    {
        var note = Instantiate(notePrefab);
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
        var note = player.GetNearestNote();
        if (note != null)
        {
            if (note.IsBeated) return;
            var distance = player.GetDistanceFromProgress(note);
            Debug.Log("Hit with distance (" + distance + ")");
  
            if (distance < 3)
            {
                var effect = Instantiate(coolEffect, note.NoteObject.transform.position, transform.rotation);
                Destroy(effect, 0.25f);
                Destroy(note.NoteObject);
                note.IsBeated = true;
            }
            else if (distance < 10)
            {
                var effect = Instantiate(goodEffect, note.NoteObject.transform.position, transform.rotation);
                Destroy(effect, 0.25f);
                Destroy(note.NoteObject);
                note.IsBeated = true;
            }
            else if (distance < 15)
            {
                var effect = Instantiate(badEffect, note.NoteObject.transform.position, transform.rotation);
                Destroy(effect, 0.25f);
                Destroy(note.NoteObject);
                note.IsBeated = true;
            }
        }
    }

    public void Start()
    {
        // setup music score player
        player.Load(@"note.xml");
        player.NoteTiming += OnNoteTiming;
        player.Play(noteSkipTime);

        var motionDetector = FindObjectOfType<MotionDetector>();
        motionDetector.XSlide += OnXSlideDetected;
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
