using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
//-----phguerra1623@gmail.com
[RequireComponent(typeof(Button))]
public class InGameMenuManager : MonoBehaviour {

	public GameObject btnNext;
	public Image pausaImg;
	public GameObject canvasPausa;
	public Sprite play;
	public Button button;
	public Material jugadorMaterial;
	public Animator parlante;
	public Texture[] parlanteOnOff;
	public Material parlanteMaterial;

	public AnimController player;

//	private bool gol;
	private GeneralManager gnrlMngr;
	private bool pausa = false;

	public GameObject gameOverCanvas;
	public GameObject gameOver1;
	public GameObject gameOver2;
	public GameObject gameOver3;
	public GameObject btnSiguiente;
	public GameObject menuFinal;
	public GameObject jugador;
	public Text scoreGlobal;
	public Text scoreLocal;

	public Canvas canvasPrincipal;
	public Image scoreTeam;
	public Image recordBaner;
	public Image unlockPlayer;

	public Sprite[] scoreTeams;
	public Sprite[] recordBaners;
	public Sprite[] unlockPlayers;
	public Image[] backgrounds;
	public Color[] bgColors;

	Animator gameOver1Animator;
	Animator gameOver2Animator;
	Animator gameOver3Animator;
	Animator menuFinalAnimator;
	Animator siguienteAnimator;

	SFXManager sfxManager;

	int pantalla = 0;

	void Start () {
		gnrlMngr = GameObject.FindGameObjectWithTag ("GeneralManager").GetComponent <GeneralManager> ();
		sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent <SFXManager> ();
		gameOver1Animator = gameOver1.GetComponent<Animator> ();
		gameOver2Animator = gameOver2.GetComponent<Animator> ();
		gameOver3Animator = gameOver3.GetComponent<Animator> ();
		siguienteAnimator = btnSiguiente.GetComponent<Animator> ();

		btnNext.SetActive (false);
		pausaImg.enabled = pausa;
		canvasPausa.SetActive (false);
		if (SceneManager.GetActiveScene ().name == "Tutorial")
			button.interactable = false;


	}

	public void GameoverControl(){
		Invoke ("ActivarGameOver", 1f);
	}
	private void ActivarGameOver(){
		canvasPrincipal.enabled = false;
		gameOverCanvas.SetActive (true);
		defineColors ();
		scoreLocal.text = gnrlMngr.score.ToString();
		scoreGlobal.text = PlayerPrefsManager.getGlobalScore().ToString();
		if (gnrlMngr.getTopScore() && gameOver1Animator.GetBool("Salida") == false) {
			gameOver1.GetComponent<Animator> ().enabled = true;
			btnSiguiente.GetComponent<Animator> ().enabled = true;

			pantalla = 0;
		} else if (gnrlMngr.getUnlockPlayer() && gameOver2Animator.GetBool("Salida") == false) {

			MaterialDB texture = GameObject.Find ("MaterialDB").GetComponent<MaterialDB> ();
			if (gnrlMngr.team) {
				jugadorMaterial.mainTexture = texture.matSt [PlayerPrefsManager.getContUnlockT()].mainTexture;
			} else {
				jugadorMaterial.mainTexture = texture.matBo [PlayerPrefsManager.getContUnlockbo()].mainTexture;
			}

			gameOver2.GetComponent<Animator> ().enabled = true;
			btnSiguiente.GetComponent<Animator> ().enabled = true;

			pantalla = 1;

			gameOver2.GetComponent<AudioSource> ().Play ();
		} else {			
			gameOver3.SetActive (true);
			menuFinal.GetComponent<Animator> ().enabled = true;
			siguienteAnimator.SetBool ("Salida", true);
			pantalla = 2;
		}
	}
	public void MoveGameoverPanels(){
		switch (pantalla) {
		case 0:
			gameOver1Animator.SetBool ("Salida", true);
			break;
		case 1:
			gameOver2Animator.SetBool ("Salida", true);

			break;
		}
		ActivarGameOver ();
	}
	void defineColors(){
		if (gnrlMngr.team) {
			scoreTeam.sprite = scoreTeams [0];
			scoreLocal.color = Color.black;
			recordBaner.sprite = recordBaners [0];
			unlockPlayer.sprite = unlockPlayers [0];
			backgrounds [0].color = bgColors [0];
			backgrounds [1].color = bgColors [1];
			backgrounds [2].color = bgColors [2];
		} else {
			scoreTeam.sprite = scoreTeams [1];
			scoreLocal.color = Color.white;
			recordBaner.sprite = recordBaners [1];
			unlockPlayer.sprite = unlockPlayers [1];
			backgrounds [0].color = bgColors [3];
			backgrounds [1].color = bgColors [4];
			backgrounds [2].color = bgColors [5];
		}
	}
	public void NextRound(){
		btnNext.SetActive (true);
	}

	public void PlayAgain(){
		sfxManager.restorePicks ();
		SceneManager.LoadScene ("Scenario");
		gnrlMngr.previousScene = 0;

	}

	public void VolverAJugar (bool restart)
	{
		Time.timeScale = 1;
		gnrlMngr.ResetGame (restart);
	}

	public void Pause(){
		pausa = !pausa;
		pausaImg.enabled = pausa;
		canvasPausa.SetActive (pausa);
		player.move = !pausa;
		if (pausa) {
			button.image.overrideSprite = play;
			Time.timeScale = 0;
		} else {
			button.image.overrideSprite = null;
			Time.timeScale = 1;
		}
		AnimParlante ();
	}

	//Métodos que controlan la animación y la música en:
	//-Control de la animación y la textura en el Menú principal
	public void SoundOnOff(){
		if (PlayerPrefsManager.getIntro ()) {
			if (PlayerPrefsManager.getSound ()) {				
				gnrlMngr.PlayMusic ();
			} else {		
				gnrlMngr.StopMusic ();
			}
		} else {
			if (PlayerPrefsManager.getSound ()) {				
				gnrlMngr.StopMusic ();
			} else {		
				gnrlMngr.PlayMusic ();				
			}
		}

		AnimParlante ();
	}
	private void AnimParlante(){
		if (PlayerPrefsManager.getSound()) {
			parlante.SetBool ("On", true);
			parlanteMaterial.mainTexture = parlanteOnOff [1];
		} else {
			parlanteMaterial.mainTexture = parlanteOnOff [0];
			parlante.SetBool ("On", false);
		}
	}
	void OnApplicationPause(bool pauseStatus){
		if (pauseStatus == false && SceneManager.GetActiveScene ().name == "Scenario") {
			if (pausaImg.enabled) {
				Pause ();
				pausa = pauseStatus;
			}
		}
	}

	public void SelectNewPlayer(){
		if (gnrlMngr.team) {
			gnrlMngr.textura = PlayerPrefsManager.getContUnlockT();
		} else {
			gnrlMngr.textura = PlayerPrefsManager.getContUnlockbo();
		}
		gnrlMngr.ResetGame (true);
	}
}
