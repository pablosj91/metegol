using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {

	public GameObject playerRend;
	public GameObject goalkeper;
	public GameObject[] broders;
	public GameObject[] enemigos;
	public MaterialDB matDb;
	private GeneralManager gnrlMngr;
	private int matComp, matRiv;
	public GameObject DTST, DTB;
	public ParticleSystem particules;

	void Start () {
		gnrlMngr = GameObject.FindGameObjectWithTag ("GeneralManager").GetComponent <GeneralManager> ();
		broders = GameObject.FindGameObjectsWithTag ("Companiero");
		enemigos = GameObject.FindGameObjectsWithTag ("Rival");
		matComp = gnrlMngr.teamSelection + 1;
		matRiv = gnrlMngr.opponentTeam;
		//playerRend.GetComponent <Renderer>().material = matDb.matDB[gnrlMngr.textura];
		//goalkeper.GetComponentInChildren <Renderer> ().material = matDb.matDB [gnrlMngr.opponentTeam -1];
		/*for (int i = 0; i < broders.Length; i++) {
			broders [i].GetComponentInChildren <Renderer> ().material = matDb.matDB [matComp + i];
		}
		for (int j = 0; j < enemigos.Length; j++) {
			enemigos [j].GetComponentInChildren <Renderer> ().material = matDb.matDB [matRiv + j];
		}*/
		if (matComp > matRiv) {
			playerRend.GetComponent <Renderer>().material = matDb.matSt[gnrlMngr.textura];
			goalkeper.GetComponentInChildren <Renderer> ().material = matDb.matBo [0];
			DTST.SetActive (true);
			DTB.SetActive (false);
			particules = matDb.particleDB [0];
			for (int i = 0; i < broders.Length; i++) {
				broders [i].GetComponentInChildren <Renderer> ().material = matDb.matSt [matComp + i];
			}
			for (int j = 0; j < enemigos.Length; j++) {
				enemigos [j].GetComponentInChildren <Renderer> ().material = matDb.matBo [matRiv + j];
			}
		} else {
			playerRend.GetComponent <Renderer>().material = matDb.matBo[gnrlMngr.textura];
			goalkeper.GetComponentInChildren <Renderer> ().material = matDb.matSt [0];
			DTST.SetActive (false);
			DTB.SetActive (true);
			particules = matDb.particleDB [1];
			for (int i = 0; i < broders.Length; i++) {
				broders [i].GetComponentInChildren <Renderer> ().material = matDb.matBo [matRiv + i];
			}
			for (int j = 0; j < enemigos.Length; j++) {
				enemigos [j].GetComponentInChildren <Renderer> ().material = matDb.matSt [matComp + j];
			}
		}
	}

}
