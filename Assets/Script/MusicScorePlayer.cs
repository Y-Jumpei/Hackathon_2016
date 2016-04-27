using UnityEngine;
using System;
using System.Collections.Generic;

public class MusicScorePlayer
{
    /// <summary>
    /// Describe note position and timing
    /// </summary>
    public class Note
    {
        public float Time { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }

        public Note(float time, float x, float y)
        {
            Time = time;
            X = x;
            Y = y;
        }
    }

    public class MusicScoreEventArgs : EventArgs
    {
        public Note Note { get; private set; }

        public MusicScoreEventArgs(Note note)
        {
            Note = note;
        }
    }

    private List<Note> score;

    private bool isPlaying = false;

    private int progress = 0;
    private int currentScoreIndex = 0;

    public event EventHandler<MusicScoreEventArgs> NoteTiming;

    /// <summary>
    /// Load music score from file
    /// </summary>
    /// <param name="filename"></param>
    public void Load(string filename)
    {
        // TODO: load from file
        score = new List<Note>()
        {
            new Note(110, 0, 0),
            new Note(150, 0, 0),
            new Note(200, 0, 0),
            new Note(250, 0, 0),
            new Note(300, 0, 0),
        };
    }

    /// <summary>
    /// Start playing music score
    /// </summary>
    public void Play(int skipTime)
    {
        if (isPlaying)
        {
            throw new InvalidOperationException("Music score already played");
        }

        progress = skipTime;
        currentScoreIndex = 0;
        isPlaying = true;
    }

    public void Stop()
    {
        isPlaying = false;
    }

    public void Update()
    {
        if (!isPlaying) return;

        // TODO: make progress based on time
        progress++;

        // raise note timing event
        while (currentScoreIndex < score.Count &&
            score[currentScoreIndex].Time < progress)
        {
            var currentNote = score[currentScoreIndex];
            NoteTiming(this, new MusicScoreEventArgs(currentNote));
            currentScoreIndex++;
        }

        // check music ends
        if (score.Count <= currentScoreIndex)
        {
            Stop();
        }
    }
}
