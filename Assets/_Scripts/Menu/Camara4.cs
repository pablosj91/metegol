using UnityEngine;
using System.Collections;

public class Camara4 : AnimEventCameras {

	public GameObject[] objetos;

	public Animator cuboGolAnimator;
	public Animator animatorCamara3;
	public AudioSource sonido;


	public void desHabilitarCanvas(){
		deshabilitarObjetos (objetos); 
	}

	public void habilitarCuboGol(){
		habilitarObjetos (objetos);
		cuboGolAnimator.SetBool ("primero", false);
		cuboGolAnimator.SetBool ("presionar", false);
		sonido.Play ();
		GameObject.Find ("MenuManager").GetComponent<MenuManager> ().AnimParlante ();
	}

}
