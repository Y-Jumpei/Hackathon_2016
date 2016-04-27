using UnityEngine;
using System;
using System.Collections.Generic;

public class MusicScorePlayer
{
    /// <summary>
    /// Type of note
    /// </summary>
    public enum NoteType
    {
        Slide,
        Chop,
    }

    /// <summary>
    /// Describe note position and timing
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Beat timing of the note
        /// </summary>
        public float Time { get; private set; }

        /// <summary>
        /// X coordinate of the initial position
        /// </summary>
        public float X { get; private set; }

        /// <summary>
        /// Y coordinate of the initial position
        /// </summary>
        public float Y { get; private set; }

        /// <summary>
        /// Unity object that coressponds with the note
        /// </summary>
        public GameObject NoteObject { get; set; }

        /// <summary>
        /// Represents whether the note has already beated
        /// </summary>
        public bool IsBeated { get; set; }

        /// <summary>
        /// Type of the note
        /// </summary>
        public NoteType Type { get; set; }

        public Note(float time, float x, float y, NoteType type)
        {
            Time = time;
            X = x;
            Y = y;
            Type = type;
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
            new Note(210, 0, 0, NoteType.Slide),
            new Note(250, 0, 0, NoteType.Slide),
            new Note(300, 0, 0, NoteType.Chop),
            new Note(350, 0, 0, NoteType.Chop),
            new Note(400, 0, 0, NoteType.Slide),
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
