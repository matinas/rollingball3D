using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

	public Text pickupsText;
	public Text badPickupsText;
	public Text killedEnemysText;

	// Use this for initialization
	void Start () {
	
		pickupsText.text = "x" + GameManager.finalScore.ToString();
		badPickupsText.text = "x" + GameManager.badPickups.ToString();
		killedEnemysText.text = "x" + GameManager.killedEnemys.ToString();
	}
}
