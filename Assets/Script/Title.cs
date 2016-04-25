using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {



	void FixedUpdate(){
		if (Input.anyKeyDown) {
			Application.LoadLevel ("Game");
		}
	}

}
