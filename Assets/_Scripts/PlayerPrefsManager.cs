using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	static string sound = "sound";
	static string globalScore = "globalScore";
	static string topScore = "topScore";
	static string contUnlockT = "contUnlockT";
	static string contUnlockBo = "contUnlockBo";
	static string fibA = "fibA";
	static string fibB = "fibB";
	static string matches = "matches";
	static string intro = "intro";



	public static void creacionKeys(){
		// sound; first; score; topScore; cantUnlockT; cantUnlockBo
		if (llaveExiste (sound) == false) {
			setSound (1);
		}
		if (llaveExiste (globalScore) == false) {
			setGlobalScore (0);
		}
		if (llaveExiste (topScore) == false) {
			setTopScore (0);
		}
		if (llaveExiste (contUnlockT) == false) {
			setContUnlockT (1);
		}
		if (llaveExiste (contUnlockBo) == false) {
			setContUnlockBo (1);
		}
		if (llaveExiste (fibA) == false) {
			setFibA (0);
		}
		if (llaveExiste (fibB) == false) {
			setFibB (1);
		}
		if (llaveExiste (matches) == false) {
			setMatches (0);
		}
		if (llaveExiste (intro) == false) {
			setIntro (0); //0 = es la primera entrada; 1 = ya no es la primera entrada.
		}
	}

	static bool llaveExiste(string nombre){
		if (PlayerPrefs.HasKey (nombre))
			return true;
		else
			return false;
	}

	public static void eliminarLlaves(){
		PlayerPrefs.DeleteAll ();
	}

	public static void setSound(int valor){
		PlayerPrefs.SetInt (sound, valor);
	}

	public static void setGlobalScore (int valor){
		PlayerPrefs.SetInt (globalScore, valor);
	}

	public static void setTopScore (int valor){
		PlayerPrefs.SetInt (topScore, valor);
	}

	public static void setContUnlockT (int valor){
		PlayerPrefs.SetInt (contUnlockT, valor);	
	}

	public static void setContUnlockBo (int valor){
		PlayerPrefs.SetInt (contUnlockBo, valor);
	}

	public static void setFibA (int valor){
		PlayerPrefs.SetInt (fibA, valor);
	}

	public static void setFibB (int valor){
		PlayerPrefs.SetInt (fibB, valor);
	}

	public static void setMatches (int valor){
		PlayerPrefs.SetInt (matches, valor);
	}

	public static void setIntro(int valor){
		PlayerPrefs.SetInt (intro, valor);
	}

	public static bool getSound(){
		if (PlayerPrefs.GetInt (sound) == 1)
			return true;
		else
			return false;
	}

	public static int getGlobalScore(){
		return PlayerPrefs.GetInt (globalScore);
	}

	public static int getTopScore(){
		return PlayerPrefs.GetInt (topScore);
	}

	public static int getContUnlockT(){
		return PlayerPrefs.GetInt (contUnlockT);
	}

	public static int getContUnlockbo(){
		return PlayerPrefs.GetInt (contUnlockBo);
	}

	public static int getFibA(){
		return PlayerPrefs.GetInt (fibA);
	}

	public static int getFibB(){
		return PlayerPrefs.GetInt (fibB);
	}

	public static int getMatches(){
		return PlayerPrefs.GetInt (matches);
	}

	public static bool getIntro(){
		if (PlayerPrefs.GetInt (intro) == 0)
			return true;
		else
			return false;
	}
}
