using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour {

	public AudioClip[] crowds;
	public AudioClip pickUps;

	public AudioSource SFXAudioSource;
	public AudioSource SFXLocutorGol;

	AudioSource reproductor;
	int contPicks = 0;

	void Start(){
		reproductor = GetComponent<AudioSource> ();
	}

	public void fail(){
		reproductor.clip = crowds [0];
		reproductor.loop = false;
		reproductor.Play ();
	}

	public void goal(){
		reproductor.clip = crowds [1];
		reproductor.loop = true;
		reproductor.Play ();
		LocuGol ();
	}

	public void pickUp(){
		playPickUp ();
	}

	public void restorePicks(){
		contPicks = 0;
	}

	public void LocuGol(){
		SFXLocutorGol.Play ();
	}

	private void playPickUp(){
		SFXAudioSource.clip = pickUps;
		SFXAudioSource.Play ();
	}

}
