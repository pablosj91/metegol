using UnityEngine;
using System.Collections;

public class AnimEventCuboGol : MonoBehaviour {

	public GameObject txtPress;

	private int vueltas = 0;

	public void Enabled(){
		if(vueltas >= 2)
			txtPress.SetActive (true);
	}

	public void disbled(){
		txtPress.SetActive (false);
	}

	public void vuelta(){
		vueltas++;
	}
}
