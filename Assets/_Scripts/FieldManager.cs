using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldManager : MonoBehaviour {

	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public GameObject player;
	public Transform area, midfield, areaInv;

	private Vector3 nextPosition; 
	private Queue<Transform> objectQueue;
	private Queue<Transform> withMidfield;
	public int ronda;
	private int mid;
	private bool seguir = true;
	private GeneralManager gnrlMng;

	void Start () {
		gnrlMng = GameObject.FindGameObjectWithTag ("GeneralManager").GetComponent <GeneralManager> ();
		ronda = gnrlMng.round;
		mid = gnrlMng.miedfield;
		objectQueue = new Queue<Transform> (numberOfObjects);
		nextPosition = startPosition;
		Transform a = (Transform)Instantiate (area);
		a.localPosition = new Vector3 (nextPosition.x, nextPosition.y, nextPosition.z - 7.5f);
		for(int i = 0; i< numberOfObjects; i++){
			objectQueue.Enqueue ((Transform)Instantiate (prefab));
		}
		if (ronda % 2 != 0 && ronda != 1) {
			mid ++;
		}
		a = (Transform)Instantiate (midfield);
		a.localPosition = new Vector3 (nextPosition.x, nextPosition.y + 0.001f, nextPosition.z + (mid * 7.5f));
		for (int i = 0; i < numberOfObjects; i++) {
			Recycle ();
		}
		gnrlMng.miedfield = mid;
	}

	void Update () {
		if (seguir == true) {
			if (objectQueue.Peek ().localPosition.z + recycleOffset < player.transform.position.z) {
				Recycle ();
				ronda--;
				if (ronda == 0) {
					seguir = false;
				}
			}
		}else{
			if (ronda == 0) {
				Transform c = (Transform)Instantiate (areaInv);
				c.localPosition = nextPosition;
				nextPosition.z += 7.5f;
				ronda--;
			}
		}
	}

	public void Recycle () {
		Transform o = objectQueue.Dequeue ();
		o.localPosition = nextPosition;
		nextPosition.z += 7.5f;
		objectQueue.Enqueue (o);
	}
}
