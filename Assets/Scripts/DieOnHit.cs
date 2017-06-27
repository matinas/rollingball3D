using UnityEngine;
using System.Collections;

public class DieOnHit : MonoBehaviour {

	public AudioClip dieSound;

	void OnTriggerEnter() {

		Debug.Log ("Se elimino al enemigo");

		// Una forma de hacerlo...
		// Destroy (transform.parent.gameObject);

		// Otra forma de hacerlo usando comunicacion entre scripts
		EnemyModel em = (EnemyModel) (transform.GetComponentInParent<EnemyModel> ());
		EnemySimple es = (EnemySimple) (transform.GetComponentInParent<EnemySimple> ());

		GameManager.killedEnemys += 1;

		if (em != null)	em.Die ();
		else if (es != null) es.Die ();
	}
}
