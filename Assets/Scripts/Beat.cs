using UnityEngine;
using System.Collections;

public class Beat : MonoBehaviour
{

    public static float timer;
    public static float[] timing;
    public static int[] position;
    public int indexTiming;
    public int cool; public int good; public int bad;
    public GameObject coolEffect;
    public GameObject goodEffect;
    public GameObject badEffect;
    public float cool_timing;
    public float good_timing;
    public float bad_timing;

    public GameObject note;
    public UnityEngine.UI.Text coolText;
    public UnityEngine.UI.Text goodText;
    public UnityEngine.UI.Text badText;

    // Use this for initialization
    void Start()
    {
        timing = new float[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
        position = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0 };
        cool = 0; good = 0; bad = 0; indexTiming = 0; timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBeat() >= 0)
        {
            checkTiming();
        }
        else if (indexTiming < timing.Length && timer - timing[indexTiming] >= 0.50)
        {
            addBad();
            indexTiming++;
        }
        timer += Time.deltaTime;
    }

    public int IsBeat()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            return 0;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            return 3;
        }
        else {
            return -1;
        }

    }

    public void checkTiming()
    {
        if (IsBeat() == position[indexTiming])
        {
            if (timer - timing[indexTiming] >= -cool_timing && timer - timing[indexTiming] <= cool_timing)
            {
                addCool();
                note = GameObject.Find("Note" + indexTiming.ToString());
                var obj = Instantiate(coolEffect, note.transform.position, transform.rotation);
                Destroy(obj, 0.25f);
                Destroy(note);
                indexTiming++;
            }
            else if (timer - timing[indexTiming] >= -good_timing && timer - timing[indexTiming] <= good_timing)
            {
                addGood();
                note = GameObject.Find("Note" + indexTiming.ToString());
                var obj = Instantiate(goodEffect, note.transform.position, transform.rotation);
                Destroy(obj, 0.25f);
                Destroy(note);
                indexTiming++;
            }
            else if (timer - timing[indexTiming] >= -bad_timing && timer - timing[indexTiming] <= bad_timing)
            {
                addBad();
                note = GameObject.Find("Note" + indexTiming.ToString());
                var obj = Instantiate(badEffect, note.transform.position, transform.rotation);
                Destroy(obj, 0.25f);
                Destroy(note);
                indexTiming++;
            }
            else {

            }
        }
        else {
            addBad();
            note = GameObject.Find("Note" + indexTiming.ToString());
            var obj = Instantiate(badEffect, note.transform.position, transform.rotation);
            Destroy(obj, 0.25f);
            Destroy(note);
            indexTiming++;
        }

    }



    public void addCool()
    {
        cool++;
        coolText.text = "Cool:" + cool.ToString();
    }
    public void addGood()
    {
        good++;
        goodText.text = "Good:" + good.ToString();
    }
    public void addBad()
    {
        bad++;
        badText.text = "Bad:" + bad.ToString();
    }
}
