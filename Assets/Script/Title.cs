using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {
	void FixedUpdate(){
		if (Input.anyKeyDown) {
			SceneManager.LoadScene("Game");
		}
	}

}
