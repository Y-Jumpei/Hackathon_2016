using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour {
	
	public Text coolText;
	public Text goodText;
	public Text badText;
	public Text maxcomboText;

	void Start () 
	{
		maxcomboText.text = "Max Combo: " + ScoreController.MaxCombo;
		coolText.text = "Cool: " + ScoreController.coolCount;
		goodText.text = "Good: " + ScoreController.goodCount;
		badText.text = "Bad: " + ScoreController.badCount;

	}
	

}
