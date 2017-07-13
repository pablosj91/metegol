using UnityEngine;
using System.Collections;

public class RivalManager : MonoBehaviour {

	public Rigidbody rb2D;
	public float speed;
    public GameObject pup;
	public Vector3 posicion;
	public ParticleSystem particle;
	public GameManager gmMg;
	private bool disapear;

	void Start () {
		disapear = false;
		gmMg = GameObject.FindGameObjectWithTag ("GameManager").GetComponent <GameManager> ();
		speed = Random.Range (-10, -17);
		rb2D.velocity = new Vector3(0, 0, speed);
		particle = gmMg.particules;

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

	public void OnBecameVisible() {
		disapear = true;
	}

	public void OnBecameInvisible() {
		if (disapear == true) {
			print ("ya no me ves perro");
			Destroy (gameObject);
		}
	}
}
