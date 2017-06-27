using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	
	public GameObject badPickUpEffect;
	public Text gameoverText;

	// Aca para ejecutar un metodo de otro script (2da respuesta): http://answers.unity3d.com/questions/1083124/two-stupid-questions-about-calling-a-method-from-a.html
	// Lo use para ejecutar la corutina que reinicia el nivel luego de unos segundos (ResetGame()), tambien cuando se detecta que se gana el juego desde este script
	// Luego lo cambie porque pase la corutina al modulo GameManager en vez de al script PlayerHealth, y agregue aca una referencia explicita al GameManager, pero antes
	// esta variable era algo de tipo PlayerHealth y se instanciaba como gameObject.GetComponent<PlayerHealth>()
	public GameManager gameManager;

	void Start() {

		gameoverText.text = "";
	}

	// Update is called once per frame
	void Update () {

		if ((GameManager.lifes == 0) && (!GameManager.endGame)) {
			gameoverText.text = "GAME OVER";
			gameManager.gameOver.Play();
			GameManager.endGame = true;
			gameManager.LoadNextLevel(GameManager.currentLevel,gameManager.gameOver.clip.length+0.5f);
			// StartCoroutine(gameManager.CoResetGame()) // Esto tambien funciona...
		}
	}

	void OnTriggerEnter(Collider info) {
		
		if (info.gameObject.CompareTag ("Bad Pick Up")) {

			GameObject effect = (GameObject) Instantiate (badPickUpEffect,info.transform.position,info.transform.rotation);
			Destroy(effect,2);
			GameManager.lifes -= 1;
			GameManager.badPickups += 1;
			info.gameObject.SetActive (false);
		}
	}
}
