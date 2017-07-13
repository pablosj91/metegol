using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MetaInput : MonoBehaviour {

	public GameObject tocar;

	void Start () {
		if (SceneManager.GetActiveScene ().buildIndex == 1)
			tocar.SetActive (false);  
	}

	void Update () {
		if (SceneManager.GetActiveScene ().buildIndex == 1) {
			if (Input.GetKey ("space")) {
				tocar.SetActive (false);
			}

			#if(UNITY_ANDROID)
			if (Input.touchCount > 0)
				tocar.SetActive (false);
			#endif
		}
	
	}

	void OnTriggerEnter(Collider obj)
	{
		if (SceneManager.GetActiveScene ().buildIndex == 1) {
			string name = obj.gameObject.tag;
			if (name == "Player") {
				tocar.SetActive (true);
			}
		}
	}
}
