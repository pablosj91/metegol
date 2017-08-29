using UnityEngine;
using System.Collections;

public class ThirdInput : MonoBehaviour {

	public PlayerTutorial player;
	public GameObject jugador;
	private bool cambiar = false;
	public GameObject comelas;
	public GameObject velocidad;
	public int c = 0;
	private Touch touch;

	void Start () {
		comelas.SetActive (false);
		velocidad.SetActive (false);
		cambiar = false;
	}

	void Update () {
		#if(UNITY_EDITOR)
		if (Input.GetKeyUp ("space") && cambiar == true && c >= 1) {
			velocidad.SetActive (true);
			c++;
		} 
		if (Input.GetKeyUp ("space") && cambiar == true && c > 2) {
			comelas.SetActive (false);
			velocidad.SetActive (false);
			player.rb2D.velocity = new Vector3 (0, 0, 9);
			jugador.GetComponent <Animator> ().speed = 1;
			cambiar = false;
			
		}
		#endif
		//#if(UNITY_ANDROID)
		if(Input.touchCount >0)
			touch = Input.touches [0];
		if(Input.touchCount > 0 && cambiar == true && c >= 1 && touch.phase == TouchPhase.Ended){
			velocidad.SetActive (true);
			c++;
		}
		if(Input.touchCount > 0 && cambiar == true && c > 2){
			comelas.SetActive (false);
			velocidad.SetActive (false);
			player.rb2D.velocity = new Vector3 (0, 0, 9);
			jugador.GetComponent <Animator> ().speed = 1;
			cambiar = false;
			
		}
		//#endif
	}

	void OnTriggerEnter(Collider obj)
	{
		string name = obj.gameObject.tag;
		if (name == "Player")
		{
			comelas.SetActive (true);
			player.rb2D.velocity = new Vector3 (0, 0, 0);
			cambiar = true;
			jugador.GetComponent <Animator> ().speed = 0;
			player.move = true;
			c++;
		}

	}
}
