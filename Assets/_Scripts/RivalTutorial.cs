using UnityEngine;
using System.Collections;

public class RivalTutorial : MonoBehaviour {

	public Rigidbody rb2D;
	public float speed;
	public GameObject pup;
	public Vector3 posicion;
	public ParticleSystem particle;
	public TutorialManager tutMngr;
	//private bool disapear;

	void Start () {
	//	disapear = false;
		tutMngr = GameObject.FindGameObjectWithTag ("GameManager").GetComponent <TutorialManager> ();
		speed = -9;
		rb2D.velocity = new Vector3(0, 0, speed);
		particle = tutMngr.particules;
	}

	void OnTriggerEnter(Collider obj)
	{
		string name = obj.gameObject.tag;
		if (name == "Companiero")
		{
			posicion = new Vector3 (this.transform.position.x + 0.7f, this.transform.position.y + 0.15f, this.transform.position.z);
			Destroy(obj.gameObject);
			Destroy(this.gameObject);
			Instantiate(pup, posicion, Quaternion.identity);
			ParticlePlayer (transform.position);
		}
		if (name == "Rival") {

			Destroy(obj.gameObject);
			ParticlePlayer (transform.position);
			Destroy(this.gameObject);
		}
		if (name == "Player") {
			print ("golpeado JUGADOR");
			rb2D.velocity = new Vector3 (0, 0, 0);
			this.GetComponent <Animator> ().Play ("Armature|Wait");
		}
	}

	void ParticlePlayer(Vector3 posBro){
		Vector3 posiciones = new Vector3 ( posBro.x + 0.7f, posBro.y + 0.15f, posBro.z);
		Instantiate(particle, posiciones, Quaternion.identity);
		particle.playOnAwake = true;
	}


}
