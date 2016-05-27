using UnityEngine;

/// <summary>
/// Describes note position and timing
/// </summary>
public class Note
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
    /// Beat timing of the note
    /// </summary>
    public float Time { get; private set; }

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

    public Note(float time, NoteType type, int beatPoint)
    {
        Time = time;
        Type = type;
        BeatPoint = beatPoint;
        IsBeated = false;
    }
}
