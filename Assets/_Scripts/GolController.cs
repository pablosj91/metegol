using UnityEngine;
using System.Collections;

public class GolController : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = GetComponent <Animator> ();
		anim.speed = 0;
	
	}
	
	void OnTriggerEnter(Collider obj){
		string name = obj.gameObject.tag;
		if(name == "Pelota")
			anim.speed = 1;
	}
}
