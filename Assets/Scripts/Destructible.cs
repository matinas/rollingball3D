using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour {

	public Transform player;
	public Transform pieces;

	public void Destroy () {

		gameObject.rigidbody.isKinematic = true;
		gameObject.collider.enabled = false;
		player.gameObject.SetActive (false);
		pieces.gameObject.SetActive (true);
	}
}
