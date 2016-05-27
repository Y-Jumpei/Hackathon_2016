using System;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public void SetPosition(float z)
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            z);
    }

    public void SetPosition(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }

    public void SetNoteType(Note.NoteType type)
    {
        switch (type)
        {
            case Note.NoteType.Chop:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Note.NoteType.Slide:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            default:
                throw new InvalidOperationException("Invalid note type");
        }
    }

    public void Update()
    {
    }
}
