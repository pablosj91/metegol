using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraController : MonoBehaviour {

	private float adv;
	private Vector3 actP;
	public Rigidbody rigid;
	public Camera mainCamera;

	private GeneralManager gnrlMngr;

	private Animator anim;
	private int moveIt = Animator.StringToHash ("finished");
	public AudioSource reproductor;

	void Start () {
		gnrlMngr = GameObject.FindGameObjectWithTag ("GeneralManager").GetComponent <GeneralManager> ();
		Scene scene = SceneManager.GetActiveScene ();
		adv = 9;
		rigid.GetComponent<Rigidbody2D>();
		if (gnrlMngr.previousScene == 1 && scene.name == "Scenario") {
			Invoke ("StartMoving", 1.5f);
		}
		else
			rigid.velocity = new Vector3(0, 0, adv);
		//rigid.velocity = new Vector3(0, 0, adv);
		anim = mainCamera.GetComponent <Animator> ();
		anim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		

			
	}

	void StartMoving(){
		rigid.velocity = new Vector3(0, 0, adv);
	}

	public void Stop(){
		rigid.velocity = new Vector3 (0, 0, 0);
	}

	public void MoveIt(){
		reproductor.Play ();
		anim.enabled = true;
	}

	public void Goal(){
		anim.Play ("Rotate");
	}
}
