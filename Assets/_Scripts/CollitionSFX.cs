using UnityEngine;
using System.Collections;

public class CollitionSFX : MonoBehaviour {

	public AudioSource reproductor;
	private GeneralManager gnrlMngr;

	void Start(){
		gnrlMngr = GameObject.FindGameObjectWithTag ("GeneralManager").GetComponent <GeneralManager> ();
	}
		
	public void playCollition(){
		if(!gnrlMngr.getGameOver ())
			reproductor.Play ();
	}
}
