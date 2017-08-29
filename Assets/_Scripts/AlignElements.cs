using UnityEngine;
using System.Collections;

public class AlignElements : MonoBehaviour {

	public Transform[] Objetos;
	public Vector3[] posObjetos;
	public bool[] direccion;

	private Camera camara;

	//0.21;0.12;2.87
	void Start () {
		camara = GetComponent <Camera>();
		//Align ();							
	}

	void Update(){
		Align ();
	}

	void Align(){
			int limite = Objetos.Length;

			for (int i = 0; i < limite; i++) {
				float x, y, z;
				y = Screen.height * posObjetos [i].y;
				z = posObjetos [i].z;
				if (direccion [i]) {
					x = Screen.width - (Screen.width * posObjetos [i].x);
				} else {
					x = Screen.width * posObjetos [i].x;
				}		
				Objetos [i].position = camara.ScreenToWorldPoint (new Vector3 (x, y, z));
		}

	}

}
