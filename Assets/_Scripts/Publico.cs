using UnityEngine;
using System.Collections;

public class Publico : MonoBehaviour {

	public AudioClip[] publico;
	public int track;

	void Update(){
		
		GetComponent<AudioSource> ().clip = publico [track];
		if (!GetComponent<AudioSource> ().isPlaying) {
			GetComponent<AudioSource> ().Play ();
		}

	}
}
