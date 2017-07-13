using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class ShareImageCanvas : MonoBehaviour {

	private bool isProcessing = false;
	public Image btnGeneral;
	public Image imgShare;
	public string mensaje;
	public GameObject share;
	public Sprite imgT;
	public Sprite imgBo;
	public Text score1;
	public Text score2;
	private GeneralManager gnMng;

	void Start(){
		gnMng = GameObject.Find ("GeneralManager").GetComponent<GeneralManager> ();
			
	}

	public void ButtonShare(){
		btnGeneral.enabled = false;
		if (gnMng.team) {
			imgShare.sprite = imgT;
			score1.text = gnMng.score.ToString ();
			score2.text = "0";
		} else {
			imgShare.sprite = imgBo;
			score2.text = gnMng.score.ToString ();
			score1.text = "0";
		}
		share.SetActive (true);
		if (!isProcessing) {
			
			StartCoroutine (ShareScreenshot());

		}
	}

	public IEnumerator ShareScreenshot()
	{
		isProcessing = true;


		// wait for graphics to render
		yield return new WaitForSeconds (0.5f);
		yield return new WaitForEndOfFrame();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		// create the texture
		//Texture2D screenTexture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,true);
		Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);;
		// put buffer into texture
		screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		// apply
		Texture2D newShare = new Texture2D(800,800,TextureFormat.RGB24, false);
		//float x = (1.0f / 800);
		//float y = (1.0f / 800);
		for (int i = 0; i < newShare.height; i++) {
			for (int j = 0; j < newShare.width; j++) {
				Color newColor = screenTexture.GetPixelBilinear ((float)j / (float)newShare.width, (float)i / (float)newShare.height);
				newShare.SetPixel (j, i, newColor);
			}
		}
		newShare.Apply ();

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		byte[] dataToSave = newShare.EncodeToPNG();
		string destination = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
		File.WriteAllBytes(destination, dataToSave);
		yield return new WaitForSeconds (2);
		if(!Application.isEditor)
		{
			// block to open the file and share it ------------START
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + destination);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

			intentObject.Call<AndroidJavaObject> ("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), ""+ mensaje);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");

			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

			currentActivity.Call("startActivity", intentObject);
		}
		isProcessing = false;
		btnGeneral.enabled = true;
		share.SetActive (false);
	}



}
