using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderTutorial : MonoBehaviour {

	public Slider slider;
	private bool timesUp = true, chutar = true;
	public float speed;
	public GameObject pelota;
	private GeneralManager gnrlMngr;
	public PlayerTutorial player;
	public int pupConter;
	public Image handle;

	void Start () {
		slider.value = 1.5f;
		InvokeRepeating ("Vibrate", 0.3f, 1.0f);
		gnrlMngr = GameObject.FindGameObjectWithTag ("GeneralManager").GetComponent<GeneralManager> ();
		speed = gnrlMngr.speed;
		pupConter = player.pupC; 
	}

	void FixedUpdate () {
		while (speed >= 0.1f) {
			if (pupConter > 0)
				speed -= 0.04f;
			else
				break;
			pupConter--;
			print ("speed "+speed);
		}
		if (timesUp == true) {
			if (slider.value < slider.maxValue)
				slider.value += speed;
			else
				timesUp = !timesUp;
		}
		else {
			if (slider.value > slider.minValue)
				slider.value -= speed;
			else
				timesUp = !timesUp;
		}

		if (chutar == true) {
			#if(UNITY_EDITOR)
			if (Input.GetKey ("space")) {
				speed = 0;
				slider.GetComponentInChildren <Image> ().enabled = false;
				if (slider.value >= -1.85f && slider.value <= 1.85f) {
					if (slider.value > -0.6f && slider.value < 0.62f) {
						chutar = false;
						pelota.GetComponent <BallTutorial> ().Shoot (1);
						player.Kick (1);
					}
					if (slider.value >= 0.62f && slider.value <= 1.85f) {
						chutar = false;
						pelota.GetComponent <BallTutorial> ().Shoot (2);
						player.Kick (2);
					}
					if (slider.value > -1.85f && slider.value <= -0.6f) {
						chutar = false;
						pelota.GetComponent <BallTutorial> ().Shoot (0);
						player.Kick (0);
					}
				} else {
					if (slider.value < -1.85f) {
						chutar = false;
						pelota.GetComponent <BallTutorial> ().Shoot (4);
						player.Kick (4);
						player.move = true;
						player.Invoke ("GameOver", 1.5f);
					}
					if (slider.value > 1.85f) {
						chutar = false;
						pelota.GetComponent <BallTutorial> ().Shoot (3);
						player.Kick (4);
						player.move = true;
						player.Invoke ("GameOver", 1.5f);
					}
				}
				CancelInvoke ();
				Invoke ("DisapearHandle", 0.3f);
			}
			#endif
			//#if(UNITY_ANDROID)
			if(Input.touchCount > 0)
			{
				slider.GetComponentInChildren <Image> ().enabled = false;
				speed = 0;
				if (slider.value >= -1.85f && slider.value <= 1.85f) {
					if (slider.value > -0.6f && slider.value < 0.62f) {
						chutar = false;
						pelota.GetComponent <BallTutorial> ().Shoot (0);
						player.Kick (1);
					}
					if (slider.value >= 0.62f && slider.value <= 1.85f) {
						chutar = false;
						pelota.GetComponent <BallTutorial> ().Shoot (1);
						player.Kick (2);
					}
					if (slider.value >= -1.85f && slider.value <= -0.6f) {
						chutar = false;
						pelota.GetComponent <BallTutorial> ().Shoot (2);
						player.Kick (0);
					}
				} else{
					if (slider.value < -1.85f) {
						chutar = false;
						pelota.GetComponent <BallTutorial> ().Shoot (4);
						player.Kick (4);
						player.Invoke ("GameOver", 1.5f);
					}
					if (slider.value > 1.85f) {
						chutar = false;
						pelota.GetComponent <BallTutorial> ().Shoot (3);
						player.Kick (4);
						player.Invoke ("GameOver", 1.5f);
					}
				}
				CancelInvoke ();
				Invoke ("DisapearHandle", 0.3f);
			}
			//#endif
		}

	}

	void Vibrate(){
		Handheld.Vibrate ();
	}

	public void SlowTime(int contador){
		if (speed >= 1.0f) {
			speed -= 0.03f;
		}
	}

	void DisapearHandle(){
		handle.enabled = false;
	}
}
