using UnityEngine;
using System.Collections;

public class KillOnHit : MonoBehaviour {

	public GameManager gameManager;

	void OnTriggerEnter(Collider colInfo) {

		if (!GameManager.endGame) {

			if (colInfo.CompareTag ("Player")) {
				Destructible destructible = (Destructible) colInfo.GetComponent("Destructible");
				destructible.Destroy();
			}

			GameManager.endGame = true;
			gameManager.LoadNextLevel(GameManager.currentLevel,2.0f);
		}
	}
}
