using UnityEngine;
using System.Collections;

public class CompanieroController : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = GetComponent <Animator> ();
		anim.speed = Random.Range (1, 2.5f);
	}

}
