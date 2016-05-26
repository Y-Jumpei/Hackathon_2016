using System;
using UnityEngine;

/// <summary>
/// Controls to play music score
/// </summary>
public class MusicScorePlayerBehavior : MonoBehaviour
{
    public AudioSource audioSource;

    /// <summary>
    /// Gets current progress
    /// </summary>
    public float Progress { get { return audioSource.time * 60.0f; } }

    /// <summary>
    /// Gets music status
    /// </summary>
    public bool IsPlaying { get { return audioSource.isPlaying; } }

    public int GetDistanceFromProgress(Note note)
    {
        return (int)Math.Abs(note.Time - Progress);
    }

    /// <summary>
    /// Starts playing music score
    /// </summary>
    public void Play()
    {
        if (IsPlaying)
        {
            throw new InvalidOperationException("Music score already played");
        }
        audioSource.Play();
    }

    /// <summary>
    /// Stops playing music score
    /// </summary>
    public void Stop()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// Creates, initializes note objects
    /// </summary>
    /// <param name="musicScore"></param>
    /// <param name="notePrefab"></param>
    public void SetupNotes(MusicScore musicScore, GameObject notePrefab)
    {
        foreach (var note in musicScore.Notes)
        {
            GameObject _note = Instantiate(notePrefab);
            var noteBehavior = _note.GetComponent<NoteBehavior>();
            _note.name = "Note";
            noteBehavior.SetPosition(note.X, 0, 1000);
            noteBehavior.SetNoteType(note.Type);
            note.NoteObject = _note;
        }
    }

    /// <summary>
    /// Updates note potisitions
    /// </summary>
    /// <param name="musicScore"></param>
    /// <param name="speed"></param>
    public void UpdateNotes(MusicScore musicScore, float speed)
    {
        foreach (var note in musicScore.Notes)
        {
            if (note.NoteObject == null) continue;
            var noteBehavior = note.NoteObject.GetComponent<NoteBehavior>();
            noteBehavior.SetPosition(speed * (note.Time - Progress));
        }
    }
}
