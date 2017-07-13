using UnityEngine;
using System.Collections;

public class AnimEventCameras : MonoBehaviour {

	protected void habilitarObjetos(GameObject[] obj){
		for (int i = 0; i < obj.Length; i++) {
			obj [i].SetActive (true);
		}
	}

	protected void habilitarObjetos(GameObject obj){
		obj.SetActive (true);
	}

	protected void habilitarComponentes(Animator[] anim){
		for (int i = 0; i < anim.Length; i++) {		
			anim [i].enabled = true;
		}
	}

	protected void deshabilitarObjetos(GameObject[] obj){
		for (int i = 0; i < obj.Length; i++) {
			obj [i].SetActive (false);
		}
	}

	protected void deshabilitarObjetos(GameObject obj){
		obj.SetActive (false);
	}

	protected void desHabilitarComponentes(Animator[] anim){
		for (int i = 0; i < anim.Length; i++) {		
			anim [i].enabled = false;
		}

	}

}
