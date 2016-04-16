using UnityEngine;
using System.Collections;

public class CreateNote : MonoBehaviour {
	public GameObject note;
	public Vector3 q_point = new Vector3 (-3, 3, 0);
	public Vector3 w_point = new Vector3 (-1, 3, 0);
	public Vector3 e_point = new Vector3 (1, 3, 0);
	public Vector3 r_point = new Vector3 (3, 3, 0);
	public float timer;
	public float[] timing = new float[] {5,6,7,8,9,10};
	public int[] position = new int[] {1,2,3,4};
	public int index;

	// Use this for initialization
	void Start () {
		timer = 0;
		index = 0;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= timing [index] - 1.5) {
			switch (position [index]) {
			case 1:
				Create_Q ();
				break;
			case 2:
				Create_W ();
				break;
			case 3:
				Create_E ();
				break;
			case 4:
				Create_R ();
				break;
			default:
				break;
			}
			index++;
		}
	}

	public void Create_Q(){
		Instantiate (note, q_point, this.transform.rotation);
	}
	public void Create_W(){
		Instantiate (note, w_point, this.transform.rotation);
	}
	public void Create_E(){
		Instantiate (note, e_point, this.transform.rotation);
	}
	public void Create_R(){
		Instantiate (note, r_point, this.transform.rotation);
	}
}