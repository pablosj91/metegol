using UnityEngine;
using System.Collections;

public class MoveGameOverMenus : MonoBehaviour {

	public float velocidad;

	void Update () {
		transform.position = new Vector3(transform.position.x + velocidad * Time.deltaTime, transform.position.y, transform.position.z);
	}
}
