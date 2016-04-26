using UnityEngine;

public class GameController : MonoBehaviour
{
    private MusicScorePlayer player = new MusicScorePlayer();

    public GameObject notePrefab;

    private void OnNoteTiming(object sender,
        MusicScorePlayer.MusicScoreEventArgs e)
    {
        var note = Instantiate(notePrefab);
        note.transform.position = new Vector3(e.Note.X, e.Note.Y, 50);
        note.name = "Note";
    }

    public void Start()
    {
        // setup music score player
        player.Load(@"note.xml");
        player.NoteTiming += OnNoteTiming;
        player.Play(10);
    }

    public void FixedUpdate()
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
    }
}
