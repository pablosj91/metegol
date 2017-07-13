 	using UnityEngine;
using System.Collections;

public class DTController : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = GetComponent <Animator> ();
	}
	
	void OnTriggerEnter(Collider obj){
		string name = obj.gameObject.tag;
		if (name == "Pelota")
			anim.Play ("Armature|Hit");
	}

	public void Celebrate(bool gol){
		if (gol) {
			anim.SetInteger ("Celebracion", 1);
		} else {
			anim.SetInteger ("Celebracion", 0);
			anim.speed = Random.Range (0.5f, 1.0f);
		}
	}
}
