using UnityEngine;
using System.Collections;

public class CompanieroController : MonoBehaviour {

	private Animator anim;
	private CollitionSFX collition;

	void Start () {
		anim = GetComponent <Animator> ();
		anim.speed = Random.Range (1, 2.5f);
		collition = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent <CollitionSFX> ();
	}

	void OnTriggerEnter(Collider obj)
	{
		string name = obj.gameObject.tag;
		if (name == "Rival") {
			collition.playCollition ();
		}
	}

}
