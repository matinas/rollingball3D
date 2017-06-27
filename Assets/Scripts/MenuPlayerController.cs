using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuPlayerController : MonoBehaviour {

	public float speed;
	public float jumpHeight;
	
	public AudioClip wallHit;

	public GameObject pickUpEffect;
	public GameObject badPickUpEffect;

	private Rigidbody rb;

	void Start() {

		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {

		// Note: When applying movement calculations inside FixedUpdate, you do not need to multiply your values by
		// Time.deltaTime. This is because FixedUpdate is called on a reliable timer, independent of the frame rate

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider info) {

		if (info.gameObject.CompareTag ("Pick Up")) {

			GameObject effect = (GameObject) Instantiate (pickUpEffect,info.transform.position,info.transform.rotation);
			Destroy(effect,3);
			info.gameObject.SetActive (false); // Or Destroy(info.gameObject);
		}
		else
			if (info.gameObject.CompareTag ("Bad Pick Up")) {
			
				GameObject effect = (GameObject) Instantiate (badPickUpEffect,info.transform.position,info.transform.rotation);
				Destroy(effect,2);
				info.gameObject.SetActive (false);
			}
	}

	void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.CompareTag ("Wall")) {
			audio.clip = wallHit;
			audio.Play ();
		}
	}
}