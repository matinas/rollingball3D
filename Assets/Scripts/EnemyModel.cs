using UnityEngine;
using System.Collections;

public class EnemyModel : MonoBehaviour {

	public Rigidbody player;
	public float bounceAmount = 5.0f;

	public GameObject enemyDeathEffect;

	private Animator centerAnim;
	public Animator enemyAnim;

	public GameObject colliders;

	private Vector3 tempPosition;
	private Quaternion tempRotation;
	private bool hasDied = false;
	private float playbackTime;

	public void Awake () {

		centerAnim = (Animator) transform.GetComponentInParent<Animator> ();
		centerAnim.Play ("Enemy Movement 2", 0, Random.Range (0.0f, 1.0f));
	}

	public void Die() {

		hasDied = true;

		Instantiate (enemyDeathEffect, transform.position, transform.rotation);
		player.velocity = new Vector3(player.velocity.x,bounceAmount,player.velocity.z);

		audio.Play ();

		// Almacenamos las posiciones del objeto previo a lanzar las transiciones de animacion...
		tempPosition = gameObject.transform.position;
		tempRotation = gameObject.transform.rotation;
		playbackTime = centerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;

		centerAnim.SetTrigger ("Stop");
		enemyAnim.SetTrigger ("Die");

		// Desactivamos todos los colliders (uno es para la hitbox, otro para la killbox y otro para que la pelota
		// choque en algunos casos (no es trigger), ya que si no estuviera se puede generar comportamientos extranios)
		colliders.SetActive(false);

		StartCoroutine (ComeAlive (5.0f));
	}

	// Esto es porque si no actualizamos la posicion con la que tenia antes de hacer la transicion de animacion la
	// nueva animacion se hace relativa al padre del Animator (o sea EnemyCenterModel), por lo que si matamos un
	// enemigo en la mitad del nivel, al ejecutarse la animacion de Stop volvera al origen donde estaba EnemyCenterModel
	public void LateUpdate() {

		if (hasDied) {
			gameObject.transform.position = tempPosition;
			gameObject.transform.rotation = tempRotation;
		}
	}

	// Rutina que permite revivir el enemigo muerto despues de cierta cantidad de segundos
	IEnumerator ComeAlive(float delay) {

		yield return new WaitForSeconds(delay);

		centerAnim.Play ("Enemy Movement 2", 0, playbackTime); // Ejecutamos la animacion desde el mismo punto en que habia muerto el goomba...
		enemyAnim.SetTrigger ("Alive");

		colliders.SetActive(true);

		hasDied = false;
	}
}
