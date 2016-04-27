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
        public GameObject NoteObject { get; set; }
        public bool IsBeated { get; set; }

        public Note(float time, float x, float y)
        {
            Time = time;
            X = x;
            Y = y;
            IsBeated = false;
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
    private int skipTime = 0;
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
            new Note(210, 0, 0),
            new Note(250, 0, 0),
            new Note(300, 0, 0),
            new Note(350, 0, 0),
            new Note(400, 0, 0),
        };

    }

    public Note GetNearestNote()
    {
        // TODO: take faster algorithm
        Note result = null;
        float min = float.MaxValue;
        foreach (var note in score)
        {
            var distance = Math.Abs(note.Time - progress);
            if (distance < min)
            {
                min = distance;
                result = note;
            }
        }
        return result;
    }

    public int GetDistanceFromProgress(Note note)
    {
        return (int)Math.Abs(note.Time - progress);
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

        progress = 0;
        this.skipTime = skipTime;
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
            score[currentScoreIndex].Time < progress + skipTime)
        {
            var currentNote = score[currentScoreIndex];
            NoteTiming(this, new MusicScoreEventArgs(currentNote));
            currentScoreIndex++;
        }
    }
}
