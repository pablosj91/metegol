using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject rival;
	public GameObject rivalRend;
	public GameObject player;
	public GameObject playerRend;
	public GameObject companieros;
	public GameObject companieroRend;
	public GameObject goalkeper;
	public GameObject DTST;
	public GameObject DTB;
	public MaterialDB matDb;

	public GameObject countDown;
	public Animator animCount;
	public Texture [] countTexture;
	private int countC = 1;

	private static float timeLeft = 1;

	private float posz, topRangeZ, spawnRival;
	public float[] posiciones, carriles, filas, companieroX;
	public List<int> filasUsadas, columnasUsadas, materialUsed, companieroZ;
	private int filaC, fila, filaAd, filaCAd, matComp, matRiv, compCant, ai;
	private bool menor = true;
	private bool material = false;
	private GeneralManager gnrlMngr;
	public ParticleSystem particules;

	public Image img;
	public Text txt;

	private float spawnDelay = 0.2f;
	private float speedT = 0;

	private AudioSource reproductor;

	void Start () {
		gnrlMngr = GameObject.FindGameObjectWithTag ("GeneralManager").GetComponent<GeneralManager> ();
		matComp = gnrlMngr.teamSelection + 1;
		matRiv = gnrlMngr.opponentTeam;
		compCant = gnrlMngr.round + Random.Range (2, 4);
		spawnRival = gnrlMngr.spawnRival;
		//playerRend.GetComponent <Renderer>().material = matDb.matDB[gnrlMngr.textura];
		PositionZ ();
		StartCoroutine ("SpawnCompanieros");
		if (matComp > matRiv) {
			playerRend.GetComponent <Renderer>().material = matDb.matSt[gnrlMngr.textura];
			goalkeper.GetComponentInChildren <Renderer> ().material = matDb.matBo [0];
			img.sprite = matDb.spriteDB [0];
			txt.color = Color.black;
			particules = matDb.particleDB [0];
			DTST.SetActive (true);
			DTB.SetActive (false);
		} else {
			playerRend.GetComponent <Renderer>().material = matDb.matBo[gnrlMngr.textura];
			goalkeper.GetComponentInChildren <Renderer> ().material = matDb.matSt [0];
			img.sprite = matDb.spriteDB [1];
			txt.color = Color.white;
			particules = matDb.particleDB [1];
			DTST.SetActive (false);
			DTB.SetActive (true);
		}
		UpdateText ();
		if(gnrlMngr.previousScene == 1){
			countDown.GetComponent <Renderer> ().material.mainTexture = countTexture [2];
			spawnDelay += 1.5f;
			spawnRival -= 0.2f;
			reproductor = countDown.GetComponent <AudioSource> ();
			reproductor.Play ();
			Invoke ("StartRunning", 1.5f);
			countDown.SetActive (true);
			InvokeRepeating ("CountDrop", 0.5f, 0.5f);
		}else{
			countDown.SetActive (false);
		}
		StartCoroutine ("Spawn");
	}

	void StartRunning(){
//		reproductor.Play ();
		animCount.Play ("countGone");
		//player.GetComponent <AnimController> ().speed = speedT;	
	}

	void Update () {
		posz = player.transform.position.z;
		if (posz > topRangeZ - 17.5f)
			StopCoroutine ("Spawn");

	}
	IEnumerator Spawn(){
		yield return new WaitForSeconds (spawnDelay);
		while (timeLeft > 0) {
			Vector3 spawnPosition = new Vector3 (posiciones[Random.Range (0, 5)], -1.85f, posz + 30.6f);
			if (matComp > matRiv) {
				rivalRend.GetComponent <Renderer> ().material = matDb.matBo [Random.Range (1, 13)];
				print ("materia companiero= "+(matComp-3)+" material riv= "+matRiv);
			}
			else
				rivalRend.GetComponent <Renderer>().material = matDb.matSt[Random.Range (1, 13)];
			Instantiate (rival, spawnPosition, rival.transform.rotation);
			yield return new WaitForSeconds(spawnRival);
		}
	}

	IEnumerator SpawnCompanieros()
	{
		/*int comp = matComp, riv;
		if (matComp > matRiv) {
			riv = 29;
		} else {
			riv = matRiv;
		}*/
		int comp = 1, riv = 1;
		yield return new WaitForSeconds (0.0f);
		for (int i = 0; i < compCant; i++) {
			while (menor == true) {
				filaC = Random.Range (0, 5);
				fila = Random.Range (0, ai);
				filaCAd = Random.Range (0, 5);
				filaAd = Random.Range ((ai + 1), (companieroZ.Count - 1));
				if (carriles [filaC] < 3 && filas [fila] < 5) {
					carriles [filaC]++;
					filas [fila]++;
					menor = false;
				} 
				for (int f = 0; f < filasUsadas.Count; f++) {
					if (filasUsadas [f] == fila && columnasUsadas [f] == filaC) {
						menor = true;
						carriles [filaC]--;
						filas [fila]--;
						break;
					}
				}
				if (menor == false) {
					filasUsadas.Add (fila);
					columnasUsadas.Add (filaC);
				}
			}
			while (menor == false) {
				filaCAd = Random.Range (0, 5);
				filaAd = Random.Range ((ai + 1), (companieroZ.Count - 1));
				if(carriles [filaCAd] < 3 && filas [filaAd]< 5){
					carriles [filaCAd]++;
					filas [fila]++;
					menor = true;
				}
				for(int j = 0; j < filasUsadas.Count; j++){
					if(filasUsadas[j] == filaAd && columnasUsadas [j] == filaCAd){
						menor = false;
						carriles[filaCAd]--;
						filas[filaAd]--;
						break;
					}
				}
				if(menor == true){
					filasUsadas.Add (filaAd);
					columnasUsadas.Add (filaCAd);
				}
			}
			if(matComp > matRiv)
			{
				companieroRend.GetComponent <Renderer> ().material = matDb.matSt[comp];
			}else
			{
				companieroRend.GetComponent <Renderer> ().material = matDb.matBo[comp];
			}
			//companieroRend.GetComponent <Renderer> ().material = matDb.matDB[comp];
			comp++;
			Vector3 spawnPosition = new Vector3 (companieroX [filaC], -1.85f,companieroZ[fila]);
			Vector3 spawnPositionA = new Vector3 (companieroX [filaCAd], -1.85f, companieroZ [filaAd]);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (companieros, spawnPosition, spawnRotation);
			//companieroRend.GetComponent <Renderer> ().material = matDb.matDB[comp];
			// estos dos if igualitos sirven para decidir que material es el q se va a usar
			// mas adelante podrian conventirce en un switch facilmente.
			if(matComp > matRiv)
			{
				companieroRend.GetComponent <Renderer> ().material = matDb.matSt[comp];
			}else
			{
				companieroRend.GetComponent <Renderer> ().material = matDb.matBo[comp];
			}
			comp++;
			Instantiate (companieros, spawnPositionA, spawnRotation);
			if (comp >= 13) {
				comp = matComp;
			}
			menor = true;
		}
	}

	void CountDrop(){
		animCount.Play ("CountDown");
		reproductor.Play ();
		countDown.GetComponent <Renderer> ().material.mainTexture = countTexture [countC];
		countC --;
		if (countC < 0)
			CancelInvoke ("CountDrop");
	}

	private int MaterialSelector(){
		int matRandom = 0;
		material = false;
		while (material == false) {
			matRandom = Random.Range (1, 14);
			for(int j = 0; j < materialUsed.Count; j++){
				if(matRandom == materialUsed[j]){
					break;
				}else{
					if(j == materialUsed.Count - 1){
						material = !material;
					}
				}
			}
		}
		return matRandom;
	}

	void PositionZ(){
		int startPos = 9;
		topRangeZ = gnrlMngr.miedfield * 2 * 7.5f;
		while (startPos <= topRangeZ){
			companieroZ.Add (startPos);
			startPos += 3;
		}
		GetMid ();
	}

	void GetMid(){
		float midZ = topRangeZ / 2;
		ai = 0;
		for (int i = 0; i < companieroZ.Count; i++) {
			if (midZ > companieroZ [i]) {
				ai = i;
			} else
				break;
		}
	}

	void UpdateText(){
		txt.text = gnrlMngr.score+"";
	}


}
