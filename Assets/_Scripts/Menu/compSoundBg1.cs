using UnityEngine;
using System.Collections;

public class SFXButtonManager : MonoBehaviour {

	public void playSound(){
		GetComponent<AudioSource> ().Play ();
	}
}
