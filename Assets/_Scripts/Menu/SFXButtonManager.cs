using UnityEngine;
using System.Collections;

public class SFXButton : MonoBehaviour {

	public void playSound(){
		GetComponent<AudioSource> ().Play ();
	}
}
