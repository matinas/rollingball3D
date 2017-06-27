using UnityEngine;
using System.Collections;

public class MenuCameraController : MonoBehaviour {

	public Canvas menuButtons;
	public float cameraSpeed;

	void Start() {

		menuButtons.gameObject.SetActive (false);
	}

	void Update() {

		if (gameObject.transform.position.z <= -6)
			gameObject.transform.position += gameObject.transform.forward * cameraSpeed * Time.deltaTime;
		else
			menuButtons.gameObject.SetActive (true);
	}
}
