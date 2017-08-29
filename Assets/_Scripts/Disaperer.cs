using UnityEngine;
using System.Collections;

public class Disaperer : MonoBehaviour {

	public bool disapear;

	void Start(){
		disapear = false;
	}

	void OnBecameVisible() {
		disapear = true;
	}

	void OnBecameInvisible() {
		if (disapear == true) {
			if (transform.root != transform) {
				//Destroy (transform.parent.gameObject);
				print ("uno menos");
			}
			else
				Destroy (gameObject);
		}
	}
	/*public void OnBecameInvisible() {
		print ("ya no me ves perro");
		Destroy (gameObject);
	}*/
}
