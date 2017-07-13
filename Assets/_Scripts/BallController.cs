using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallController : MonoBehaviour {

	private Animator anim;
	private int iddle = Animator.StringToHash ("iddle");
	private bool shot;
	private int side;
	private GeneralManager gnrlMngr;
	private GameObject [] crowd;
	private CrowdController cwrdController;
	private GoalKeeperController goalKeeper;
	private GameObject [] dt;

	public CameraController camC; 
	public GameObject player;
	public GameObject g;
	public GameObject o;
	public GameObject l;
	public InGameMenuManager ingManager;
	public SFXManager sfxCrowdManager;
	public GameObject marcador;

	public Sprite[] texturasGol;

	private CoinManager coin;

	void Start () {
		anim = GetComponent <Animator> ();
		shot = false;
		gnrlMngr = GameObject.FindGameObjectWithTag ("GeneralManager").GetComponent <GeneralManager> ();
		sfxCrowdManager =  GameObject.FindGameObjectWithTag ("SFXManager").GetComponent <SFXManager> ();
	}

	void Update(){
		if (shot == true) {
			switch (side) {
			case 2:
				transform.Translate (4.0f * Time.deltaTime, 0, 30.5f * Time.deltaTime);
				break;
			case 0:
				transform.Translate (-7.0f * Time.deltaTime, 0, 30.5f * Time.deltaTime);
				break;
			case 1:
				transform.Translate (0, 0, 30.5f * Time.deltaTime);
				break;
			case 3:
				transform.Translate (6.9f * Time.deltaTime, 1 * Time.deltaTime, 27f * Time.deltaTime);
				break;
			case 4:
				transform.Translate (-11f * Time.deltaTime, 3f * Time.deltaTime, 27f * Time.deltaTime);
				break;
			} 
		}
	}

	void OnTriggerEnter (Collider obj){
		string name = obj.gameObject.tag;
		if (name == "Arquero") {
			gnrlMngr.Gol (false);
			shot = false;
			transform.Translate (0, 0, 0);
			FindKeeper ();
			player.GetComponent <AnimController> ().Crying ();
			ingManager.Invoke ("NextRound", 3);
			FindDT (false);
			GetComponentInParent<PlayerSoundController> ().goalKeeper ();
			sfxCrowdManager.fail ();
		}
		if (name == "Arco") {
			obj.enabled = false;
			gnrlMngr.Gol (true);
			transform.Translate (0, 0, 0);
			shot = false;
			FindCrowd ();
			camC.Goal ();
			Invoke ("ApearGOL", 1);
			ingManager.Invoke ("NextRound", 3);
			FindDT (true);
			gnrlMngr.setGameOver (true);
			sfxCrowdManager.goal ();
			GetComponentInParent<PlayerSoundController> ().inGoal ();
		}
		if (name == "DT") {
			transform.Translate (0, 0, 0);
			shot = false;
			FindKeeper ();
			player.GetComponent <AnimController> ().Crying ();
			obj.enabled = false;
			Invoke ("GoToShit", 0.05f);
			sfxCrowdManager.fail ();
			GetComponentInParent<PlayerSoundController> ().ballPunch();
		}
	}

	public void Shoot(int chut){
		coin = GameObject.FindGameObjectWithTag ("Coin").GetComponent <CoinManager> ();
		if (chut == coin.lugar) {
			goalKeeper = GameObject.FindGameObjectWithTag ("Arquero").GetComponent <GoalKeeperController> ();
			goalKeeper.volarLugar = false;
			goalKeeper.noSaltar = chut;
		}
		side = chut;
		Invoke ("StartShooting", 0.5f);
		GetComponentInParent<PlayerSoundController> ().kick ();
	}

	public void GoForward(){
		anim.SetBool (iddle, true);
		anim.enabled = false;
	}

	void StartShooting (){
		shot = true;
	}

	void FindCrowd(){
		crowd = GameObject.FindGameObjectsWithTag ("Crowd");
		for (int i = 0; i < crowd.Length; i++) {
			cwrdController = crowd[i].GetComponent <CrowdController> ();
			cwrdController.Celebrate ();
		}
	}

	void FindKeeper(){
		goalKeeper = GameObject.FindGameObjectWithTag ("Arquero").GetComponent <GoalKeeperController> ();
		goalKeeper.AfterShoot ();
	}

	void ApearGOL(){
		player.GetComponent <AnimController> ().Celebration ();
		Vector3 positioner = new Vector3 (-1.3f, 4.07f, player.transform.position.z);
		GameObject nuevoG = (GameObject) Instantiate (g, positioner, Quaternion.identity);
		Vector3 positionerO = new Vector3 (0.2f, 4.07f, player.transform.position.z);
		Instantiate (o, positionerO, Quaternion.identity);
		Vector3 positionerL = new Vector3 (1.7f, 4.07f, player.transform.position.z);
		Instantiate (l, positionerL, Quaternion.identity);
		if (gnrlMngr.team) {
			nuevoG.GetComponentInChildren<SpriteRenderer> ().sprite = texturasGol [0];
		} else {
			nuevoG.GetComponentInChildren<SpriteRenderer> ().sprite = texturasGol [1];
		}
		nuevoG.GetComponentInChildren<TextMesh> ().text = gnrlMngr.score.ToString();
	}

	void FindDT(bool gol){
		dt = GameObject.FindGameObjectsWithTag ("DT");
		for (int i = 0; i < dt.Length; i++) {
			dt [i].GetComponent <DTController> ().Celebrate (gol);
		}
	}

	void GoToShit(){
		transform.Translate (0, -100 * Time.deltaTime, 0);
	}

}
