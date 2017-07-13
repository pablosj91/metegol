using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private float adv;
	private Vector3 actP;
	public Rigidbody rigid;
	public Camera mainCamera;

	private GeneralManager gnrlMngr;

	private Animator anim;
	private int moveIt = Animator.StringToHash ("finished");

	void Start () {
		gnrlMngr = GameObject.FindGameObjectWithTag ("GeneralManager").GetComponent <GeneralManager> ();
		adv = 9;
		rigid.GetComponent<Rigidbody2D>();
		if(gnrlMngr.previousScene == 1)
			Invoke ("StartMoving", 1.5f);
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
		anim.enabled = true;
	}

	public void Goal(){
		anim.Play ("Rotate");
	}
}
