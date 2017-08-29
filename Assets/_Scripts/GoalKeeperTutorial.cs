using UnityEngine;
using System.Collections;

public class GoalKeeperTutorial : MonoBehaviour {

	public Animator anim;
	private int estados;
	public float[] positions;
	public int jump = 4;
	public BoxCollider coll;
	public GameObject chadow;

	void Start () {
		estados = Animator.StringToHash ("estados");
		anim.SetInteger (estados, 1);
		coll.center = new Vector3 (-0.06f, coll.center.y, coll.center.z);
	}


	public void Jump(){
		/*jump = Random.Range (0, 3);
		if(jump == 3)
			jump = 2;*/
		if (jump != 1) {
			if (jump == 2)
				anim.SetInteger ("estados", 2);
			if (jump == 0)
				anim.SetInteger ("estados", 0);
			coll.center = new Vector3 (positions [jump], coll.center.y, coll.center.z);
			Destroy (chadow);
		} else
			anim.SetInteger ("estados", 3);
	}

	public void PlayerShoots(int posicion){
		jump = Random.Range (0, 3);
		if (jump == posicion) {
			if (posicion == 0)
				jump++;
			else
				jump--;
		}
		Invoke ("Jump", 0.35f);
	}

	public void AfterShoot(){
		switch (jump){
		case 1:
			anim.Play ("Armature|GK.Joy");
			break;
		case 2:
			anim.Play ("Armature|GK.Joy.L");
			break;
		case 0:
			anim.Play ("Armature|GK.Joy.R");
			break;
		}
	}
}
