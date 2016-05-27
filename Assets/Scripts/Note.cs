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
