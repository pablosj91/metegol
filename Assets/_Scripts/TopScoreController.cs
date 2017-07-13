using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopScoreController : MonoBehaviour {

	public Text score;

	public GeneralManager gnrlMngr;

	void Start () {
		gnrlMngr = GameObject.FindGameObjectWithTag ("GeneralManager").GetComponent <GeneralManager> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TextUpdate(){
		int scoreInt = PlayerPrefs.GetInt ("Gol");
		score.text = scoreInt + "-0!";
	}
}
