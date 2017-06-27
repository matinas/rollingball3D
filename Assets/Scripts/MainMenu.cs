using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

	public void QuitGame () {
	
		Debug.Log ("Exiting game...");
		Application.Quit ();
	}
	
	public void LoadLevel (string level) {
	
		Application.LoadLevel (level);
	}

	public void SetVolume(float volume) {

		GameObject musicManager = GameObject.FindGameObjectWithTag ("Music Manager");
		if (musicManager != null) {
			Debug.Log ("Encontro music manager");
			AudioSource music = musicManager.GetComponent<AudioSource>();
			music.volume = volume;
		}

		// Para sacar el focus del Slider, sino al presionar las teclas
		// de direccion para mover la pelota se baja/sube el volumen
		EventSystem.current.SetSelectedGameObject(null);
	}
}
