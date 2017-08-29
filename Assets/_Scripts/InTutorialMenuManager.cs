using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InTutorialMenuManager : MonoBehaviour {

	public GameObject btnNext;
	public Button button;
	private GeneralManager gnrlMngr;
	public Image scoreTeam;
	public Sprite[] scoreTeams;
	public Text score;

	public Image imgScoreLocal;
	public Sprite[] scoreLocal;
	public Text txtScoreLocal;

	void Start () {
		gnrlMngr = GameObject.FindGameObjectWithTag ("GeneralManager").GetComponent <GeneralManager> ();
		defineColors ();
		if (SceneManager.GetActiveScene ().name == "Tutorial")
			button.interactable = false;
	}

	public void GameoverControl(){
		Invoke ("ActivarGameOver", 1f);
	}


	void defineColors(){
		if (gnrlMngr.team) {
			scoreTeam.sprite = scoreTeams [0];
			score.color = Color.black;
			imgScoreLocal.sprite = scoreLocal [0];
			txtScoreLocal.color = Color.black;
		} else {		
			imgScoreLocal.sprite = scoreLocal [1];
			txtScoreLocal.color = Color.white;
			scoreTeam.sprite = scoreTeams [1];
			score.color = Color.white;
		}
		txtScoreLocal.text = gnrlMngr.score.ToString();
	}
	public void NextRound(){
		btnNext.SetActive (true);
	}

	public void PlayAgain(){
		SceneManager.LoadScene ("Scenario");
	}

	public void VolverAJugar (bool restart)
	{
		Time.timeScale = 1;
		gnrlMngr.ResetGame (restart);
	}

	public void SoundOnOff(){

		if (PlayerPrefsManager.getSound()) {				
			//gnrlMngr.PlayMusic ();
		} else {				
			//gnrlMngr.StopMusic ();
		}

	}

	void OnApplicationPause(bool pauseStatus){
		
	}

	public void SelectNewPlayer(){
		if (gnrlMngr.team) {
			gnrlMngr.textura = PlayerPrefsManager.getContUnlockT() + 15;
		} else {
			gnrlMngr.textura = PlayerPrefsManager.getContUnlockbo() + 1;
		}
		gnrlMngr.ResetGame (true);
	}
}
