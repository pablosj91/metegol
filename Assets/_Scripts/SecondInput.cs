using UnityEngine;
using System.Collections;

public class SecondInput : MonoBehaviour {

	public PlayerTutorial player;
	public GameObject jugador;
	public RivalTutorial rival;
	public RivalTutorial rival2;
	public CameraController camCon;
	private bool cambiar = false;
	public GameObject swipe;

	void Start () {
		swipe.SetActive (false);
	}

	void Update () {
		if (jugador.transform.position.x < 2.2f && cambiar == true) {
			rival.rb2D.velocity = new Vector3 (0, 0, -11);
			player.rb2D.velocity = new Vector3 (0, 0, 9);
			camCon.rigid.velocity = new Vector3 (0, 0, 9);
			rival2.rb2D.velocity = new Vector3 (0, 0, -10);
			jugador.GetComponent <Animator> ().speed = 1;
			rival2.GetComponent <Animator> ().speed = 1;
			swipe.SetActive (false);
			cambiar = false;
		} 
	}

	void OnTriggerEnter(Collider obj)
	{
		string name = obj.gameObject.tag;
		if (name == "Player")
		{
			swipe.SetActive (true);
			rival.rb2D.velocity = new Vector3 (0, 0, 0);
			rival2.rb2D.velocity = new Vector3 (0, 0, 0);
			player.rb2D.velocity = new Vector3 (0, 0, 0);
			camCon.rigid.velocity = new Vector3 (0, 0, 0);
			cambiar = true;
			jugador.GetComponent <Animator> ().speed = 0;
			rival2.GetComponent <Animator> ().speed = 0;
			player.move = true;
		}

	}
}
