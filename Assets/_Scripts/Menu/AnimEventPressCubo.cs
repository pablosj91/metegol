using UnityEngine;
using System.Collections;

public class AnimEventPressCubo : MonoBehaviour {

	public AudioClip[] soundsCubo;

	public void playExplosion(){
		GetComponent<AudioSource> ().clip = soundsCubo [0];
		if (!GetComponent<AudioSource> ().isPlaying) {
			GetComponent<AudioSource> ().Play ();
		}
	}
	public void changeAudio(){
		GetComponent<AudioSource> ().clip = soundsCubo [1];
		GetComponent<AudioSource> ().Play ();
	}
}
