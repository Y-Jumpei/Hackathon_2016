using UnityEngine;
using System.Collections;

public class DeleteNote : MonoBehaviour {
	public float time=0;

	void Update(){
		time += Time.deltaTime;
	}

	void OnTriggerEnter(Collider hit){
		if(hit.CompareTag("Finish")){
			Destroy (gameObject);
		}
	}
}
