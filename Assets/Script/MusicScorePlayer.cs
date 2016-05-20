using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

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
        /// Beat position
        /// </summary>
        public int BeatPoint { get; set; }

        /// <summary>
        /// Type of the note
        /// </summary>
        public NoteType Type { get; set; }

        public Note(float time, float x, float y, NoteType type, int beatPoint)
        {
            Time = time;
            X = x;
            Y = y;
            Type = type;
            BeatPoint = beatPoint;
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

    /// <summary>
    /// Gets current progress
    /// </summary>
    public int Progress { get { return progress; } }

    /// <summary>
    /// Raised on note creation timing
    /// </summary>
    public event EventHandler<MusicScoreEventArgs> NoteTiming;

    /// <summary>
    /// Load music score from file
    /// </summary>
    /// <param name="filename"></param>
    public void Load(string filename)
    {
        score = new List<Note>();

        score.Add(new Note(227, -2, 0, NoteType.Chop, 0));
        score.Add(new Note(275, -2, 0, NoteType.Chop, 0));
        score.Add(new Note(327, -2, 0, NoteType.Chop, 0));
        score.Add(new Note(350, 0, 0, NoteType.Chop, 1));
        score.Add(new Note(375, 0, 0, NoteType.Chop, 1));
        score.Add(new Note(426, 2, 0, NoteType.Chop, 2));
        score.Add(new Note(475, -2, 0, NoteType.Chop, 0));
        score.Add(new Note(525, -2, 0, NoteType.Chop, 0));
        score.Add(new Note(576, -2, 0, NoteType.Chop, 0));
        score.Add(new Note(625, 0, 0, NoteType.Chop, 1));
        score.Add(new Note(674, 0, 0, NoteType.Chop, 1));
        score.Add(new Note(722, 2, 0, NoteType.Slide, 2));
        score.Add(new Note(746, 0, 0, NoteType.Slide, 1));
        score.Add(new Note(769, 2, 0, NoteType.Slide, 2));

        return;

        try
        {
            // parse xml document
            var document = XDocument.Load(filename);
            var notesElement = document.Root.Descendants("notes");

            foreach (var noteElement in notesElement.Descendants("note"))
            {
                var note = new Note(
                    int.Parse(noteElement.Attribute("time").Value),
                    int.Parse(noteElement.Attribute("x").Value),
                    int.Parse(noteElement.Attribute("y").Value),
                    (NoteType)Enum.Parse(typeof(NoteType), noteElement.Attribute("type").Value),
                    int.Parse(noteElement.Attribute("point").Value));
                score.Add(note);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed to parse score file. (" + e.Message + ")");
        }
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
