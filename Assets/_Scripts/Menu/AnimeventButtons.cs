using UnityEngine;
using System.Collections;

public class AnimeventButtons : MonoBehaviour {

	public int aumentar;
	public int disminuir;
	public float tiempo;
	public AnimationClip clip1;
	public AnimationClip clip2;
	public Transform icono;

	void Start(){
		AnimationCurve curve = AnimationCurve.Linear (tiempo, icono.localScale.x + aumentar , tiempo, icono.localScale.x + aumentar);
		clip1.SetCurve (icono.gameObject.name, typeof(Transform), "localScale.x", curve);
		curve = AnimationCurve.Linear (tiempo, icono.localScale.y + aumentar , tiempo, icono.localScale.y + aumentar);
		clip1.SetCurve (icono.gameObject.name, typeof(Transform), "localScale.y", curve);
		curve = AnimationCurve.Linear (tiempo, icono.localScale.y + aumentar , tiempo, icono.localScale.y + aumentar);
		clip1.SetCurve (icono.gameObject.name, typeof(Transform), "localScale.z", curve);


		AnimationCurve curve2 = AnimationCurve.Linear (tiempo, icono.localScale.x - disminuir , tiempo, icono.localScale.x - disminuir);
		clip2.SetCurve (icono.gameObject.name, typeof(Transform), "localScale.x", curve2);
		curve = AnimationCurve.Linear (tiempo, icono.localScale.y - disminuir , tiempo, icono.localScale.y - disminuir);
		clip2.SetCurve (icono.gameObject.name, typeof(Transform), "localScale.y", curve2);
		curve = AnimationCurve.Linear (tiempo, icono.localScale.y - disminuir , tiempo, icono.localScale.y - disminuir);
		clip2.SetCurve (icono.gameObject.name, typeof(Transform), "localScale.z", curve2);
	}
}



