using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	public GameObject coolEffect;
	public GameObject goodEffect;
	public GameObject badEffect;
	public GameObject note;
	public int cool; public int good; public int bad;
	public float cool_timing;
	public float good_timing;
	public float bad_timing;
	public int indexTiming;
	public UnityEngine.UI.Text coolText;
	public UnityEngine.UI.Text goodText;
	public UnityEngine.UI.Text badText;


	// Use this for initialization
	void Start () {
		indexTiming = 0;
		cool = 0;good = 0;bad = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){


		if (Beat.timer - Beat.timing [indexTiming] >= -cool_timing && Beat.timer - Beat.timing [indexTiming] <= cool_timing) {
			addCool ();
			note = GameObject.Find ("Note" + indexTiming.ToString ());
			var obj = Instantiate (coolEffect, note.transform.position, transform.rotation);
			Destroy (obj, 0.25f);
			Destroy (note);
			indexTiming++;
		} else if (Beat.timer - Beat.timing [indexTiming] >= -good_timing && Beat.timer - Beat.timing [indexTiming] <= good_timing) {
			addGood ();	
			note = GameObject.Find ("Note" + indexTiming.ToString ());
			var obj = Instantiate (goodEffect, note.transform.position, transform.rotation);
			Destroy (obj, 0.25f);
			Destroy (note);
			indexTiming++;
		} else if (Beat.timer - Beat.timing [indexTiming] >= -bad_timing && Beat.timer - Beat.timing [indexTiming] <= bad_timing) {
			addBad ();
			note = GameObject.Find ("Note" + indexTiming.ToString ());
			var obj = Instantiate (badEffect, note.transform.position, transform.rotation);
			Destroy (obj, 0.25f);
			Destroy (note);
			indexTiming++;
		} else {

		}

	}

	public void addCool(){
		cool++;
		coolText.text = "Cool:" + cool.ToString ();
	}
	public void addGood(){
		good++;
		goodText.text = "Good:" + good.ToString ();
	}
	public void addBad(){
		bad++;
		badText.text = "Bad:" + bad.ToString ();
	}
		


}
