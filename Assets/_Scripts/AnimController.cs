using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class AnimController : MonoBehaviour {

	private float fingerStartTime = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;

	private bool isSwipe = false;
	public bool move = true;
	private float minSwipeDist = 50.0f;
	private float maxSwipeTime = 0.5f;

	public Rigidbody rb2D;
	public float speed; 

	private Animator anim;
	private int estados = Animator.StringToHash ("estados");
	public int pupC;

	private GeneralManager gnrlMng;
	public BallController ball;

	public CameraController camC;

	public GameObject slider;
	public SliderController sliderController;

	private GoalKeeperController goalKeeper;
	public Image pupImg;
	public MaterialDB matDB;
	public GameObject pup;
	public Slider velocidadSlider;
	public GameObject shadow;
	public GameObject gameOver;
	public GameObject gameOverPanel;
	private bool gameOverBool = true;

	public Button button;

	private SFXManager sfxCrowdManager;
	private InGameMenuManager menuManager;

	void Awake(){
		sliderController.enabled = false;
		slider.SetActive (false);
		gameOverPanel.SetActive (false);
	}

	void Start () {
		pup.SetActive (false);
		gnrlMng = GameObject.FindGameObjectWithTag ("GeneralManager").GetComponent <GeneralManager> ();
		menuManager = GameObject.Find ("InGameMenu").GetComponent<InGameMenuManager> ();
		sfxCrowdManager =  GameObject.FindGameObjectWithTag ("SFXManager").GetComponent <SFXManager> ();
		anim = GetComponent<Animator> ();
		if (gnrlMng.previousScene == 1) {
			Invoke ("StartMoving", 1.5f);
			anim.Play ("Armature|Wait");
		}
		else
			rb2D.velocity = new Vector3(0, 0, speed);
		velocidadSlider.value = gnrlMng.speed;
		gnrlMng.setGameOver (false);
	}

	void Update () {
		if (move == true) {
			//#if(UNITY_ANDROID)
			if (Input.touchCount > 0) {
				foreach (Touch touch in Input.touches) {
					switch (touch.phase) {
					case TouchPhase.Began:
						// this is a new touch
						isSwipe = true;
						fingerStartTime = Time.time;
						fingerStartPos = touch.position;
						break;

					case TouchPhase.Canceled:
						// the thouch is being canceled
						isSwipe = false;
						break;

					case TouchPhase.Ended:
						float gestureTime = Time.time - fingerStartTime;
						float gestureDist = (touch.position - fingerStartPos).magnitude;

						if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist) {
							Vector2 direction = touch.position - fingerStartPos;
							Vector2 swipeType = Vector2.zero;

							if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {
								//the swipe is horizontal
								swipeType = Vector2.right * Mathf.Sign (direction.x);
							} else {
								//the swipe is vertical
								swipeType = Vector2.up * Mathf.Sign (direction.y);
							}

							if (swipeType.x != 0.0f) {
								if (swipeType.x > 0.0f) {
									if (transform.position.x < 3.7f) {
										transform.position += new Vector3 (1.5f, 0, 0);

									}
									//anim.SetTrigger (slideHash);
								} else {
									if (transform.position.x > -2.3f) {
										transform.position -= new Vector3 (1.5f, 0, 0);
									}
									//anim.SetTrigger (slideHash);
								}

							}
							GetComponent<PlayerSoundController>().movePlayer();
						}
						break;
					}
				}
			}
			//#endif
			#if(UNITY_EDITOR)
			if(Input.GetKeyUp ("left") && transform.position.x > -2.3f)
			{
				transform.position -= new Vector3 (1.5f, 0, 0);
				GetComponent<PlayerSoundController>().movePlayer();
			}
			if(Input.GetKeyUp ("right") && transform.position.x < 3.7f)
			{
				transform.position += new Vector3 (1.5f, 0, 0);
				GetComponent<PlayerSoundController>().movePlayer();
			}

			#endif
		}
	}

	void StartMoving(){
		rb2D.velocity = new Vector3(0, 0, speed);
		anim.Play ("Armature|Run");
	}

	void OnTriggerEnter (Collider obj){
		string name = obj.gameObject.tag;
		if (name == "Rival") {
			GetComponent<PlayerSoundController>().bump();
			sfxCrowdManager.fail ();
			Dismember ();
			GameOver ();
		}
		if (name == "Meta") {
			anim.SetInteger (estados, 1);
			button.interactable = false;
			if (transform.position.x != 0.7f) {
				anim.Play ("Armature|Shrink");
				Invoke ("MovePlayer", 0.3f);
			}
			camC.MoveIt ();
			rb2D.velocity = new Vector3(0, 0, 0);
			Application.CaptureScreenshot (""+Application.persistentDataPath+"/metegol.png");
			gnrlMng.DestroyEverything ();
			Invoke ("EnableSlider", 1);
			move = false;
			ball.GoForward ();
		}
		if(name == "Companiero")
		{
			GetComponent<PlayerSoundController>().bump();
			sfxCrowdManager.fail ();
			Dismember ();
			GameOver ();
			//gnrlMng.Invoke ("ResetGame", 1.5f);
		}
		if(name == "PwrUp")
		{
			obj.enabled = false;
			Destroy(obj.gameObject);
			pupC++;
			velocidadSlider.value -= 0.04f;
			pup.SetActive (true);
			pup.GetComponent <Animator> ().Play ("Eaten");
			pupImg.GetComponent <Animator> ().Play ("Increase");
			Invoke ("DeactivatePup",0.5f);
			sfxCrowdManager.pickUp ();
		}
		if (name == "Finish") {
			camC.Stop ();
		}
		if (name == "Pelota") {
			ball.GoForward ();
		}
	}

	void EnableSlider(){
		slider.SetActive (true);
		sliderController.enabled = true;
	}

	public void NextRound(){
		gnrlMng.round++;

	}

	public void Kick(){
		anim.Play ("Armature|Kick");
		goalKeeper = GameObject.FindGameObjectWithTag ("Arquero").GetComponent <GoalKeeperController> ();
		goalKeeper.PlayerShoots ();
	}

	private void MovePlayer(){
		transform.position = new Vector3 (0.7f, transform.position.y, transform.position.z);
		rb2D.velocity = new Vector3(0, 0, 0);
	}

	void DeactivatePup(){
		pup.SetActive (false);
	}

	public void Celebration(){
		int celebration = Random.Range (0, 3);
		if (celebration == 3)
			celebration = 2;
		switch (celebration) {
		case(0):
			anim.SetInteger ("Festejo", 1);
			break;
		case(1):
			anim.SetInteger ("Festejo", 2);
			break;
		case(2):
			anim.SetInteger ("Festejo", 3);
			break;
		}

	}

	public void Crying(){
		anim.SetInteger ("Festejo", 4);
	}

	void Dismember(){
		Destroy (shadow);
		rb2D.velocity = new Vector3 (0, 0, 0);
		camC.Stop ();
		anim.Play ("Armature|Break.01");
		move = false;
	}

	public void GameOver(){
		gnrlMng.setGameOver (true);
		button.interactable = false;
		if (gameOverBool == true) {
			/*Vector3 positionGV = new Vector3 (-2.46f, transform.position.y, transform.position.z + 10.0f);
			Instantiate (gameOver, positionGV, Quaternion.identity);*/
			//gameOverPanel.SetActive (true);
			gnrlMng.ScoreSaver ();
			menuManager.GameoverControl ();
			gameOverBool = false;
		}

	}

}	