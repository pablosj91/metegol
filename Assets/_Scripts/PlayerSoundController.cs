using UnityEngine;
using System.Collections;

public class PlayerSoundController : MonoBehaviour {

	public AudioClip[] sonidos;
	AudioSource reproductor;

	void Start(){
		reproductor = GetComponent<AudioSource> ();
	}

	public void playerBreak (){
		reproductor.clip = sonidos [0];
		reproductor.Play ();
	}

	public void movePlayer(){
		reproductor.clip = sonidos [1];
		reproductor.Play ();
	}

	public void kick(){
		reproductor.clip = sonidos [2];
		reproductor.PlayDelayed (0.4f);
	}

	public void goalKeeper(){
		reproductor.clip = sonidos [3];
		reproductor.Play ();
	}

	public void inGoal(){
		reproductor.clip = sonidos [4];
		reproductor.Play ();
	}

	public void bump(){
		reproductor.clip = sonidos [5];
		reproductor.Play ();
	}
		
	public void ballPunch(){
		reproductor.clip = sonidos [6];
		reproductor.Play ();
	}
}
