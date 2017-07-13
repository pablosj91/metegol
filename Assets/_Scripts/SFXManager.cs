using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour {

	public AudioClip[] crowds;
	public AudioClip[] pickUps;

	public AudioSource SFXAudioSource;

	AudioSource reproductor;
	int contPicks = 0;

	void Start(){
		reproductor = GetComponent<AudioSource> ();
	}

	public void fail(){
		reproductor.clip = crowds [0];
		reproductor.Play ();
	}

	public void goal(){
		reproductor.clip = crowds [1];
		reproductor.Play ();
	}

	public void pickUp(){
		contPicks++;
		if (contPicks >= pickUps.Length - 1) {
			contPicks = pickUps.Length - 1;
		}
		playPickUp ();
	}

	public void restorePicks(){
		contPicks = 0;
	}

	private void playPickUp(){
		SFXAudioSource.clip = pickUps [contPicks];
		SFXAudioSource.Play ();
	}

}
