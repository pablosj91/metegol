using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter (Collider obj){
		string name = obj.gameObject.tag;
		if (name == "Rival") 
			Destroy (obj.gameObject);
	}
}
