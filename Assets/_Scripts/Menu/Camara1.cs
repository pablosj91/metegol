using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Camara1 : AnimEventCameras {

	public GameObject[] elementos;
	public Button boton1;
	public Button boton2;

	public void habilitarLetras(){
		habilitarObjetos (elementos);
		boton1.enabled = true;
		boton2.enabled = true;
	}

	public void desHabilitarLetras(){
		deshabilitarObjetos (elementos);
	}


}
