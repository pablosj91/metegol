using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class ShareScore : MonoBehaviour {

	public RenderTexture overviewTexture;
	GameObject OVcamera;
	public string path = "";

	void Start()
	{
		OVcamera = GameObject.Find("CameraScore");
	}
	 
	void LateUpdate()
	{

		if (Input.GetKeyDown("f9"))
	    {
			StartCoroutine(TakeScreenShot());
		}

	}

	// return file name
	string fileName(int width, int height)
	{
		return string.Format("screen_{0}x{1}_{2}.png",width, height,System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}

	public IEnumerator TakeScreenShot()
	{
		yield return new WaitForEndOfFrame();

		Camera camOV = OVcamera.GetComponent<Camera>();

		//RenderTexture currentRT = camOV.targetTexture;

		//RenderTexture.active = camOV.targetTexture;
		camOV.Render();
		Texture2D imageOverview = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		imageOverview.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		imageOverview.Apply();

		//RenderTexture.active = currentRT;
	        
		byte[] bytes = imageOverview.EncodeToPNG();

		// save in memory
	    string filename = fileName(Convert.ToInt32(imageOverview.width), Convert.ToInt32(imageOverview.height));
		path = Path.Combine(Application.persistentDataPath, filename);

		System.IO.File.WriteAllBytes(path, bytes);

		//Application.CaptureScreenshot (path);



	}

}
