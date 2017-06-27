using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}

	// Esto llama despues de todos los Update de todos los gameObject, para que no hayan inconsistencias
	// en el posicionamiento de la camara, y se setee correctamente con la posicione actualizada de la bola
	void LateUpdate () {
		transform.position = player.transform.position + offset;
		transform.LookAt (player.transform);
	}
}
