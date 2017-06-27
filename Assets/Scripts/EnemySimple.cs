using UnityEngine;
using System.Collections;

public class EnemySimple : MonoBehaviour {

	public Rigidbody player;
	public float bounceAmount = 5.0f;

	public GameObject enemyDeathEffect;

	public void Die() {

		AudioSource audio = gameObject.GetComponentInParent<AudioSource>();
		audio.Play ();

		Instantiate (enemyDeathEffect, transform.position, transform.rotation);
		player.velocity = new Vector3(player.velocity.x,bounceAmount,player.velocity.z);

//		Destroy (gameObject);
		gameObject.SetActive (false);
	}
}
