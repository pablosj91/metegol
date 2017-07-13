using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Audio;

public class GeneralManager : MonoBehaviour {

	public static GeneralManager instance;

	public int teamSelection, round, score, opponentTeam, miedfield;
	public float speed, spawnRival;
	public int textura { get; set; }
	bool topScore = false;
	bool unlock = false;

	public AudioMixer mixer;
	public AudioClip[] musicBackground;
	private int prevTrack = 0;
	private int actTrack = 0;


	public bool team = false;
	private bool gameOver = true;

	public int previousScene = 0;

	void Awake () {
		if(instance){
			DestroyImmediate(gameObject);
		}
		else{
			DontDestroyOnLoad(transform.gameObject);
			instance = this;
		}
		//PlayerPrefs.DeleteAll ();
	}

	void Start () {
		round = 1;
		miedfield = 3;
		speed = 0.1f;
		spawnRival = 0.5f;


		PlayerPrefsManager.creacionKeys ();
		UnlockPlayers ();
	}

	void Update(){
		if (SceneManager.GetActiveScene ().name == "Menu") {
			if (Input.GetKeyDown (KeyCode.Escape))
				Application.Quit ();
		}

		MusicBackGround ();
	}

	private void MusicBackGround(){
		if (gameOver) {
			actTrack = 0;
		} else {
			actTrack = 1;
		}
		GetComponent<AudioSource> ().clip = musicBackground [actTrack];
		if (prevTrack != actTrack) {
			prevTrack = actTrack;
			GetComponent<AudioSource> ().Play ();
		}
	}

	public void TeamChooser(int v){
		if (v == 1) {
			team = true;
			teamSelection = 15;
			opponentTeam = 1;
		} else {
			team = false;
			teamSelection = 1;
			opponentTeam = 15;
		}
		previousScene = 1;
		if (PlayerPrefsManager.getGlobalScore() == 0) {
			SceneManager.LoadScene ("Tutorial");
		} else {
			SceneManager.LoadScene ("Scenario");
		}
		Matches ();
	}

	public void Gol(bool gol){
		round++; 
		if (speed < 0.5)
			speed += 0.05f;
		else {
			if (speed < 0.7)
				speed += 0.03f;
		}
		if(spawnRival > 0.25f)
			spawnRival -= 0.005f;
		if (gol == true) {
			score++;
		}
	}

	public void ScoreSaver(){

		//if (PlayerPrefs.HasKey ("score")) {
		PlayerPrefsManager.setGlobalScore(score + PlayerPrefsManager.getGlobalScore());
		//} else {
		//	PlayerPrefs.SetInt ("score", score);
		//}

		//if (PlayerPrefs.HasKey ("topScore")) {
		if (PlayerPrefsManager.getTopScore() < score) {
			PlayerPrefsManager.setTopScore(score);
				topScore = true;
			} else {
				topScore = false;
			}
		//} else {
		//	PlayerPrefs.SetInt ("topScore", 0);
		//}

		//Debug.LogWarning("Score " + PlayerPrefs.GetInt("score"));
		UnlockPlayers ();
	}

	private void Matches(){
		if (PlayerPrefs.HasKey ("matches")) {
			PlayerPrefs.SetInt ("matches", PlayerPrefs.GetInt("matches") + 1);
		} else {
			PlayerPrefs.SetInt ("matches", 1);
		}

		//Debug.LogWarning ("Matches " + PlayerPrefs.GetInt ("matches"));
	}	

	private void UnlockPlayers(){
		
		if (!PlayerPrefsManager.getIntro()) {
			/*if(score > 3){
				if (team) {
					PlayerPrefsManager.setContUnlockT (PlayerPrefsManager.getContUnlockT() + 1);
				} else {
					PlayerPrefsManager.setContUnlockBo (PlayerPrefsManager.getContUnlockbo() + 1);
				}*/
			if (PlayerPrefsManager.getGlobalScore() >= PlayerPrefsManager.getFibB()) {
				if (team) {
					PlayerPrefsManager.setContUnlockT (PlayerPrefsManager.getContUnlockT() + 1);
				} else {
					PlayerPrefsManager.setContUnlockBo (PlayerPrefsManager.getContUnlockbo() + 1);
				}
				int a = PlayerPrefsManager.getFibA ();
				int b = PlayerPrefsManager.getFibB ();
				int c = a + b;
				PlayerPrefsManager.setFibA (b);
				PlayerPrefsManager.setFibB (c);
				unlock = true;
			} else {
				unlock = false;
			}

		} 
	}

	public void DestroyEverything(){
		GameObject[] rivales = GameObject.FindGameObjectsWithTag ("Rival");
		GameObject[] companieros = GameObject.FindGameObjectsWithTag ("Companiero");
		GameObject[] pup = GameObject.FindGameObjectsWithTag ("PwrUp");
		for (int i = 0; i < rivales.Length; i++) {
			Destroy (rivales[i]);
		}
		for (int j = 0; j < companieros.Length; j++) {
			Destroy (companieros[j]);
		}
		for (int k = 0; k < pup.Length; k++) {
			Destroy (pup[k]);
		}
	}

	public void PlayMusic(){
		//if (PlayerPrefs.HasKey ("sound")) {
		mixer.SetFloat ("masterVol", 0f);
				//GetComponent<AudioSource> ().Play ();
		PlayerPrefsManager.setSound(1);
		//}
	}

	public void StopMusic(){	
		mixer.SetFloat ("masterVol", -80f);
		PlayerPrefsManager.setSound(0);	
	}

	/*void Presentacion(){
		if (!PlayerPrefs.HasKey ("First")) {
			PlayerPrefs.SetInt ("First", 0);
		}
		if (!PlayerPrefs.HasKey ("topScore")) {
			PlayerPrefs.SetInt ("topScore",0);
		}
		if (!PlayerPrefs.HasKey ("sound")) {
			PlayerPrefs.SetInt ("sound",1);
		}
	}*/
	void OnApplicationQuit(){
		PlayerPrefsManager.setIntro (0);
	}

	public void ResetGame(bool reset){
		gameOver = false;
		round = 1; 
		score = 0;
		miedfield = 3;
		speed = 0.1f;
		spawnRival = 0.5f;
		previousScene = 0;
		if(reset)
			SceneManager.LoadScene ("Scenario");
		else
			SceneManager.LoadScene ("Menu");
	}

	public bool getTopScore(){
		return topScore;
	}
	public bool getUnlockPlayer(){
		return unlock;
	}

	public bool getGameOver(){
		return gameOver;
	}
	public void setGameOver(bool valor){
		this.gameOver = valor;
	}
}
