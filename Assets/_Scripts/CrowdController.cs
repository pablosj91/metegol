using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CrowdController : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = GetComponent <Animator> ();
	}
	
	public void Celebrate(){
		anim.SetInteger ("Celebracion", 1);
	}
}
