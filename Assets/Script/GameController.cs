using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject L_hand_palm;

	void FixedUpdate(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			Application.LoadLevel ("Title");
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
		
		
}
