using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {

	public float[] posision;
	public int lugar;

	void Start () {
		CoinAppearance ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CoinAppearance()
	{
		for(int i = 0; i < posision.Length; i++)
		{
			print ( posision [i]);
		}
		lugar = Random.Range (0, 3);
		transform.position = new Vector3 (posision[lugar], transform.position.y, transform.position.z);
	}

}
