using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class MenuManager : MonoBehaviour {
	
	private GeneralManager gnrlMngr;
	//Varibles para controlar los diferentes Swipes
	private Vector2 fingerStartPos;
	private bool isSwipe = false;
	private bool tigreSwipe = false;
	private bool bolivarSwipe = false;
	//Variable para continuar con la carga de la siguiente escena
	private bool continuar = false;
	//Arreglos para conocer los jugadores que están bloqueados
	private bool[] teamTigre;
	private bool[] teamBolivar;
	//Variable que pasa al GeneralManager para saber el equipo escogido
	private int teamChoose;
	//Controlan la animación del Logo y la animación del principio
	//private bool cuboGol = false;
	public Animator cuboGolAnimator;
	//Cámaras para controla la animación y Swipe
	private Camera camara4;
	private Camera camara1;
	private Camera camara2;
	//Controlar la elección de jugadores, con animación, materiales y texturas
	private bool sumarContadorT = true;
	private bool sumarContadorBo = true;

	public Animator tigreAnimator;
	public Material tigreMaterial;
	public Texture[] tigreTexture;
	int contTextureT = 0;

	public Animator bolivarAnimator;
	public Material bolivarMaterial;
	public Texture[] bolivarTexture;
	int contTextureBO = 0;

	public Texture bloqueado;
	public GameObject txtPress;
	//Elementos de UI que muestran el score Globar y mejor Score
	public Text txtGlobal;
	public Text txtScore;
	//Partículas que se muestran al momento de elegir el equipo para jugar
	public GameObject particula1;
	public GameObject particula2;
	public Animator escudoTigre;
	public Animator escudoBolivar;
	//Controlar la capacidad de encender o apagar la música
	public Animator parlante;
	public Texture[] parlanteOnOff;
	public Material parlanteMaterial;
	//Control de la aniación del principio del juego
	private Animator logoAnimator;
	public GameObject logo;
	public Animator camaraAnimator;
	public Animator pulpo;
	public GameObject icoScoreGlobal;
	//Canvas que contienen las diferentes opciones, comienzan desactivados.
	public Canvas CanvasCam1;
	public Canvas CanvasCam2;
	public Canvas CanvasCam3;
	//Animator cámaras para la selección
	public Animator camara1animator;
	public Animator camara2animator;


	void Start () {
		
		gnrlMngr = GameObject.FindGameObjectWithTag ("GeneralManager").GetComponent <GeneralManager> ();
		camara4 = GameObject.Find ("Camera4").GetComponent<Camera> ();
		camara1 = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camara2 = GameObject.Find ("Camera2").GetComponent<Camera> ();
		logoAnimator = logo.GetComponent<Animator> ();

		bolivarMaterial.mainTexture = bolivarTexture [contTextureBO];
		tigreMaterial.mainTexture = tigreTexture [contTextureT];

		teamTigre = new bool[tigreTexture.Length];
		teamBolivar = new bool[bolivarTexture.Length];
		gnrlMngr.setGameOver (true);
		//PlayerPrefsManager.eliminarLlaves ();
		PlayerPrefsManager.creacionKeys ();
		AnimParlante ();
		TextUpdate ();
		armarEquipos ();
		Presentacion ();
	}

	void Update(){
		//Debug.Log (PlayerPrefsManager.getIntro ().ToString ());
		if (!continuar) {
			if (PlayerPrefsManager.getIntro()) {
				Debug.Log ("primero");
				ControlTouchFirst ();
				if (camaraAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Camera4")) {
					logo.SetActive (true);
					icoScoreGlobal.SetActive (true);
				} else {
					logo.SetActive (false);
					icoScoreGlobal.SetActive (false);
				}
			} else {
				//Debug.Log ("segundo");
				pulpo.enabled = false;
				ControlTouch ();
				CancelarAnimacion ();
			}
		}
		CambiarContadores ();
		ChangeTexture ();
	}
	void ControlTouchFirst(){
		
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			switch (touch.phase) {
			case TouchPhase.Began:
				CuboGolAnim (camara4.ScreenToViewportPoint (touch.position), true);
				Debug.Log ("1");
				break;
			case TouchPhase.Stationary:
				CuboGolAnim (camara4.ScreenToViewportPoint (touch.position), true);
				Debug.Log ("1");
				break;
			case TouchPhase.Moved:
				CuboGolAnim (camara4.ScreenToViewportPoint (touch.position), true);
				Debug.Log ("1");
				break;
			case TouchPhase.Canceled:
				CuboGolAnim (camara4.ScreenToViewportPoint (touch.position), false);
				Debug.Log ("0");
				break;
			case TouchPhase.Ended:
				CuboGolAnim (camara4.ScreenToViewportPoint (touch.position), false);
				Debug.Log ("0");
				break;
			}
		}
	}

	void ControlTouch(){
		
		if (Input.touchCount > 0) {

			Touch touch = Input.GetTouch (0);

			switch (touch.phase) {

			case TouchPhase.Began:
				isSwipe = false;
				fingerStartPos = touch.position;
				CuboGolAnim (camara4.ScreenToViewportPoint (touch.position), true);
				if (!bolivarSwipe) {
					TigrePlayerChooser (camara1.ScreenToViewportPoint (touch.position), true);
				}
				if (!tigreSwipe) {
					BolivarPlayerChooser (camara2.ScreenToViewportPoint (touch.position), true);
				}
				break;
			case TouchPhase.Moved:
				isSwipe = true;
				if (!bolivarSwipe) {
					TigrePlayerChooser (camara1.ScreenToViewportPoint (touch.position), true);
				}
				if (!tigreSwipe) {
					BolivarPlayerChooser (camara2.ScreenToViewportPoint (touch.position), true);
				}
				break;
			case TouchPhase.Stationary:
				CuboGolAnim (camara4.ScreenToViewportPoint (touch.position), true);
				if (!bolivarSwipe) {
					TigrePlayerChooser (camara1.ScreenToViewportPoint (touch.position), true);
				}
				if (!tigreSwipe) {
					BolivarPlayerChooser (camara2.ScreenToViewportPoint (touch.position), true);
				}
				break;
			case TouchPhase.Ended:

				Vector2 direction = touch.position - fingerStartPos;
				Vector2 swipeType = Vector2.zero;

				if (isSwipe == false && direction.magnitude <= 20) {
					if (tigreSwipe) {						
						tigreAnimator.SetBool ("Derecha", true);
					} else if (bolivarSwipe) {
						bolivarAnimator.SetBool ("Izquierda", true);
					}
				} else {
					if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {
						swipeType = Vector2.right * Mathf.Sign (direction.x);
					}						

					if (swipeType.x != 0.0f) {

						if (swipeType.x > 0.0f) {
							if (tigreSwipe) {
								tigreAnimator.SetBool ("Derecha", true);
							} else if (bolivarSwipe) {
								bolivarAnimator.SetBool ("Derecha", true);
							}
						} else {
							if (tigreSwipe) {
								tigreAnimator.SetBool ("Izquierda", true);
							} else if (bolivarSwipe) {
								bolivarAnimator.SetBool ("Izquierda", true);
							}
						}
					} 
				}
				tigreSwipe = false;
				bolivarSwipe = false;
				isSwipe = false;
				CuboGolAnim (camara4.ScreenToViewportPoint (touch.position), false);
				TigrePlayerChooser (camara1.ScreenToViewportPoint (touch.position), false);
				BolivarPlayerChooser (camara2.ScreenToViewportPoint (touch.position), false);
				break;
			}

		}
	}
	//Métodos que son activados con un elemento dentro de uno de los Canvas como:
	//-Escudos
	public void TeamChooser(int v){
		print ("trataste de escoger");
		if (GameObject.Find ("bling(Clone)") == null) {

			teamChoose = v;
			if (Input.touchCount >= 1) {
				Touch touch = Input.GetTouch (0);
				Camera camera;
				if (v == 1) {
					
					if (teamTigre [contTextureT]) {
						print ("tigre");
						continuar = true;
						camera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
						Instantiate (particula1, camera.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 1)), Quaternion.identity);
						Instantiate (particula2, camera.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 1)), Quaternion.identity);

						escudoTigre.SetBool ("Elegido", true);
						tigreAnimator.SetBool ("Elegido", true);
						bolivarAnimator.SetBool ("NoElegido", true); 

						camara1animator.SetBool ("Seleccion", true);

						Invoke ("SiguienteEscena", 2.3f);
					} else {
						contTextureT = -1;
						tigreAnimator.SetBool ("Derecha", true);
						continuar = false;
					}		

				} else {

					if (teamBolivar [contTextureBO]) {
						print ("bolibar");
						continuar = true;
						camera = GameObject.Find ("Camera2").GetComponent<Camera> ();
						Instantiate (particula1, camera.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 1)), Quaternion.identity);
						Instantiate (particula2, camera.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 1)), Quaternion.identity);

						escudoBolivar.SetBool ("Elegido", true);
						tigreAnimator.SetBool ("NoElegido", true);
						bolivarAnimator.SetBool ("Elegido", true); 

						camara2animator.SetBool ("Seleccion", true);

						Invoke ("SiguienteEscena", 2.3f);
					} else {
						contTextureBO = -1;
						bolivarAnimator.SetBool ("Izquierda", true);
						continuar = false;
					}

				}
			}		
		}
	}
	//-Jugador del tigre
	public void TigrePlayerChooser(Vector3 ubicacion, bool press){
		if ((ubicacion.x >= 0 && ubicacion.x <= 1) && (ubicacion.y <= 0.5 && ubicacion.y >= 0)) {
			if (press) {
				tigreSwipe = true;				
			} else {
				tigreSwipe = false;
			}
		}
		//tigreSwipe = true;
	}
	//-Jugador del bolivar
	public void BolivarPlayerChooser(Vector3 ubicacion, bool press){
		if ((ubicacion.x >= 0 && ubicacion.x <= 1) && (ubicacion.y <= 0.5 && ubicacion.y >= 0)) {
			if (press) {
				bolivarSwipe = true;
			} else {
				bolivarSwipe = false;
			}
		}
		//bolivarSwipe = true;
	}
	//-Ícono de Facebook
	public void OpenFacebook(){
		Application.OpenURL ("http://www.facebook.com/pulposoftware/");
	}

	private void SiguienteEscena(){

		if (continuar) {
			gnrlMngr.TeamChooser (teamChoose);
			if (teamChoose == 1) {
				print ("textura del tigre = " + contTextureT);
				gnrlMngr.textura = contTextureT + 1;
				print ("textura = "+ gnrlMngr.textura); 
			} else {
				gnrlMngr.textura = contTextureBO + 1;
			}
		}
	
	}
	//Actualización de los componentes Text de UI que muestran el Score global y Top score del jugador y su equipo
	private void TextUpdate(){
		txtGlobal.text = PlayerPrefsManager.getGlobalScore().ToString();
		txtScore.text = PlayerPrefsManager.getTopScore().ToString();
	}
	//Animación y control táctil de la animación del Logo
	private void CuboGolAnim(Vector3 ubicacion, bool press){
		float y = 0.75f;

		if (PlayerPrefsManager.getIntro()) {
			y = 0;
			if (logoAnimator.GetCurrentAnimatorStateInfo (0).IsName ("CuboTemblando")) {
				if (press == false) {
					PlayerPrefsManager.setIntro (1);

					camaraAnimator.SetBool ("Salto", true);
					logoAnimator.SetBool ("Idle", true);
					cuboGolAnimator.SetBool ("primero", true);

					CanvasCam1.enabled = true;
					CanvasCam2.enabled = true;
					CanvasCam3.enabled = true;
				} else {
					txtPress.SetActive (false);
					cuboGolAnimator.enabled = true;
				}
			} 
		}

		if (ubicacion.x >= 0 && ubicacion.y >= y) {			
			if (press) {
				cuboGolAnimator.SetBool ("presionar", true);
			} else {
				Debug.Log ("Release");
				cuboGolAnimator.SetBool ("presionar", false);			
			}

		}




	}
	//Métodos que controlan el cambio de textura, animación y contadores de arreglos de dichas texturas para elegir un jugador
	//-Control de la animación de ambos equipos (The Strongest y Bolivar)
	private void ChangeTexture(){
		if (tigreAnimator.GetCurrentAnimatorStateInfo (0).IsName ("BackFromLeft") || tigreAnimator.GetCurrentAnimatorStateInfo (0).IsName ("BackFromRight")) {
			
			limiteContadores (contTextureT, true);
			if (teamTigre [contTextureT]) {
				tigreMaterial.mainTexture = tigreTexture [contTextureT];
			} else {
				tigreMaterial.mainTexture = bloqueado;
			}

			tigreAnimator.SetBool ("Derecha", false);
			tigreAnimator.SetBool ("Izquierda", false);
		} else if (bolivarAnimator.GetCurrentAnimatorStateInfo (0).IsName ("BackFromLeftBo") || bolivarAnimator.GetCurrentAnimatorStateInfo (0).IsName ("BackFromRightBo")) {
			
			limiteContadores (contTextureBO, false);
			if (teamBolivar [contTextureBO]) {
				bolivarMaterial.mainTexture = bolivarTexture [contTextureBO];
			} else {
				bolivarMaterial.mainTexture = bloqueado;
			}
				
			bolivarAnimator.SetBool ("Derecha", false);
			bolivarAnimator.SetBool ("Izquierda", false);
		}
	}
	//-Control de los límites de los arreglos, para volver a comenzar el contador y llevarlo hasta el final
	private void limiteContadores(int cont, bool equipo){
		//true = tigre; false = bolivar
		if (cont > tigreTexture.Length-1) {
			if (equipo)
				contTextureT = 0;
			else
				contTextureBO = 0;
		} else if (cont < 0) {
			if (equipo)
				contTextureT = tigreTexture.Length - 1;
			else
				contTextureBO = bolivarTexture.Length - 1;
		}
	}		
	//-*************************Da el permiso para la suma o resta de las variables que controlan la posición de las texturas en sus respectivos arreglos
	private void CambiarContadores(){
		if (sumarContadorBo) {
			if (bolivarAnimator.GetCurrentAnimatorStateInfo (0).IsName ("MoveLeftBo")) {
				contTextureBO++;
			} else if (bolivarAnimator.GetCurrentAnimatorStateInfo (0).IsName ("MoveRightBo")) {
				contTextureBO--;
			}
			sumarContadorBo = false;
		}

		if (sumarContadorT) {
			if (tigreAnimator.GetCurrentAnimatorStateInfo (0).IsName ("MoveLeft")) {
				contTextureT--;
				sumarContadorT = false;
			} else if (tigreAnimator.GetCurrentAnimatorStateInfo (0).IsName ("MoveRight")) {
				contTextureT++;
				sumarContadorT = false;
			}
		}

		if (tigreAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Armature|MS_Idle")) {
			sumarContadorT = true;

		}
		if (bolivarAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Armature|MS_Idle")) {
			sumarContadorBo = true;
		}
	}
	//Métodos que sirven para conocer los jugadores que se encuentran bloqueados, se dividen en:
	//-Llenar los vectores booleanos que tendrán la información de jugadores disponibles
	private void armarEquipos(){
		//CantPlayers ();

		for (int i = 0; i < teamTigre.Length; i++) {
			if (i + 1 <= PlayerPrefsManager.getContUnlockT()) {
				teamTigre [i] = true;
			} else {
				teamTigre [i] = false;
			}
		}

		for (int i = 0; i < teamBolivar.Length; i++) {
			if (i + 1 <=  PlayerPrefsManager.getContUnlockbo()) {
				teamBolivar [i] = true;
			} else {
				teamBolivar [i] = false;
			}
		}
	}
	//-Validación de las variables globales que controlan la cantidad de jugadores desbloqueados hasta el momento
	/*private void CantPlayers(){

		if (!PlayerPrefs.HasKey ("cantUnlockT"))
			PlayerPrefs.SetInt ("cantUnlockT", 1);

		if (!PlayerPrefs.HasKey ("cantUnlockBo"))
			PlayerPrefs.SetInt ("cantUnlockBo", 1);
		

	*/
	//Métodos que controlan la animación y la música en:
	//-Control de la animación y la textura en el Menú principal
	public void SoundOnOff(){
		if (PlayerPrefsManager.getIntro ()) {
			if (PlayerPrefsManager.getSound ()) {				
				gnrlMngr.PlayMusic ();
			} else {		
				gnrlMngr.StopMusic ();
			}
		} else {
			if (PlayerPrefsManager.getSound ()) {				
				gnrlMngr.StopMusic ();
			} else {		
				gnrlMngr.PlayMusic ();				
			}
		}

		AnimParlante ();
	}
	public void AnimParlante(){
		if (PlayerPrefsManager.getSound()) {
			parlante.SetBool ("On", true);
			parlanteMaterial.mainTexture = parlanteOnOff [1];
		} else {
			parlanteMaterial.mainTexture = parlanteOnOff [0];
			parlante.SetBool ("On", false);
		}
	}
	//Animación que se realiza al principio del juego
	void Presentacion(){
		
		if (PlayerPrefsManager.getIntro()) {
			camaraAnimator.enabled = true;
			logoAnimator.enabled = true;
			cuboGolAnimator.enabled = false;
			CanvasCam1.enabled = false;
			CanvasCam2.enabled = false;
			CanvasCam3.enabled = false;
			SoundOnOff ();
		} else {
			camaraAnimator.enabled = false;
			logoAnimator.enabled = false;
			cuboGolAnimator.enabled = true;
			CanvasCam1.enabled = true;
			CanvasCam2.enabled = true;
			CanvasCam3.enabled = true;
			txtPress.SetActive (false);
		}
	}
	void habilitarTxtPress(){
		txtPress.SetActive (true);
	}
		
	//--------------Métodos por implementar
	public void TopScore(){
		SceneManager.LoadScene ("TopScorer");
	
	}

	private void CancelarAnimacion(){
		if (camaraAnimator.enabled) {
			if (camaraAnimator.GetNextAnimatorStateInfo(0).IsName("Camara4Idle") == false) {
				//cuboGolAnimator.SetBool ("presionar", false);
			}
		}
	}
}

