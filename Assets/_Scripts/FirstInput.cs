using UnityEngine;
using System.Collections;

public class FirstInput : MonoBehaviour {

	public PlayerTutorial player;
	public GameObject jugador;
	public RivalTutorial rival;
	public RivalTutorial rival2;
	public CameraController camCon;
	private bool cambiar = true;
	public GameObject swipe;

	void Start () {
		swipe.GetComponent <Animator> ().enabled = false;
		swipe.SetActive (false);
		
	}

	void Update () {
		if (player.right == true && jugador.transform.position.x == 2.2f && cambiar == true) {
			rival.rb2D.velocity = new Vector3 (0, 0, -11);
			player.rb2D.velocity = new Vector3 (0, 0, 9);
			camCon.rigid.velocity = new Vector3 (0, 0, 9);
			rival2.rb2D.velocity = new Vector3 (0, 0, -10);
			jugador.GetComponent <Animator> ().speed = 1;
			player.move = false;
			cambiar = false;
			swipe.SetActive (false);
		}
	}
	
	void OnTriggerEnter(Collider obj)
	{
		string name = obj.gameObject.tag;
		if (name == "Player")
		{
			swipe.SetActive (true);
			swipe.GetComponent <Animator> ().enabled = true;
			rival.rb2D.velocity = new Vector3 (0, 0, 0);
			rival2.rb2D.velocity = new Vector3 (0, 0, 0);
			player.rb2D.velocity = new Vector3 (0, 0, 0);
			camCon.rigid.velocity = new Vector3 (0, 0, 0);
			jugador.GetComponent <Animator> ().speed = 0;
			player.move = true;
		}

	}
}
