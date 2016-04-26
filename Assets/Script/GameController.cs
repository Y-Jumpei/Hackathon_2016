using UnityEngine;

public class GameController : MonoBehaviour
{
    private MusicScorePlayer player = new MusicScorePlayer();

    public GameObject L_hand_palm;

    private void OnNoteTiming(
        object sender,
        MusicScorePlayer.MusicScoreEventArgs e)
    {
        Debug.Log(e.Note.Time);
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
