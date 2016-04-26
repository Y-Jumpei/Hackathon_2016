using UnityEngine;
using System.Collections;

public class CreateNote : MonoBehaviour
{
    private Vector3 q_point = new Vector3(-5, 1, 10);
    private Vector3 w_point = new Vector3(-1, 1, 10);
    private Vector3 e_point = new Vector3(1, 1, 10);
    private Vector3 r_point = new Vector3(5, 1, 10);

    public GameObject note;
    public int index;
    public float delay;
    public float speed = 500f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (index < Beat.timing.Length && Beat.timer >= Beat.timing[index] - delay)
        {
            switch (Beat.position[index])
            {
                case 0:
                    Create_Q();
                    index++;
                    break;
                case 1:
                    Create_W();
                    index++;
                    break;
                case 2:
                    Create_E();
                    index++;
                    break;
                case 3:
                    Create_R();
                    index++;
                    break;
            }
        }
    }

    public void Create_Q()
    {
        var obj = Instantiate(note);
        obj.transform.position = q_point;
        obj.name = "Note" + index.ToString();
        GameObject ob;
        ob = GameObject.Find(obj.name);
        //ob.GetComponent<Rigidbody> ().AddForce (0, -speed, 0);
        Destroy(obj, 3.0f);
    }
    public void Create_W()
    {
        Object obj = Instantiate(note, w_point, this.transform.rotation);
        obj.name = "Note" + index.ToString();
        GameObject ob;
        ob = GameObject.Find(obj.name);
        //ob.GetComponent<Rigidbody> ().AddForce (0, -speed, 0);
        Destroy(obj, 3.0f);
    }
    public void Create_E()
    {
        Object obj = Instantiate(note, e_point, this.transform.rotation);
        obj.name = "Note" + index.ToString();
        GameObject ob;
        ob = GameObject.Find(obj.name);
        //ob.GetComponent<Rigidbody> ().AddForce (0, -speed, 0);
        Destroy(obj, 3.0f);
    }
    public void Create_R()
    {
        Object obj = Instantiate(note, r_point, this.transform.rotation);
        obj.name = "Note" + index.ToString();
        GameObject ob;
        ob = GameObject.Find(obj.name);
        //ob.GetComponent<Rigidbody> ().AddForce (0, -speed, 0);
        Destroy(obj, 3.0f);
    }
}
