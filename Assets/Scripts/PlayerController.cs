using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpHeight;
	
	public AudioClip wallHit;
	public AudioClip jumpWallHit;
	public AudioClip fallWallHit;

	public GameObject pickUpEffect;

	public GameManager gameManager;

	public Text countText;
	public Text winText;

	public int pickupMax = 12;

	private Rigidbody rb;
	private bool isFalling;
	private float distToGround;

	void Start() {

		rb = GetComponent<Rigidbody> ();
		speed = 10.0f;
		SetCountText ();
		winText.text = "";
		jumpHeight = 5;
		isFalling = false;
		distToGround = collider.bounds.extents.y;
	}

	void FixedUpdate() {

		// Note: When applying movement calculations inside FixedUpdate, you do not need to multiply your values by
		// Time.deltaTime. This is because FixedUpdate is called on a reliable timer, independent of the frame rate

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);

		// Hacemos que la bola pueda saltar
		if (Input.GetKeyDown (KeyCode.Space) && (!GameManager.endGame)) {
			Debug.Log ("Saltaaaa");
			if (!isFalling)
			{
				rb.velocity = new Vector3(0, jumpHeight, 0);
				audio.clip = jumpWallHit;
				audio.Play();
				StartCoroutine(DelayFall());
			}
		}

		// A modo de prueba simplemente...
		// Funciona perfecto...
//		if (IsGrounded ())
//			Debug.Log ("Toca el piso");
//		else
//			Debug.Log ("NO toca el piso");
	}

	void OnTriggerEnter(Collider info) {

		if (info.gameObject.CompareTag ("Pick Up")) {

			GameObject effect = (GameObject) Instantiate (pickUpEffect,info.transform.position,info.transform.rotation);
			Destroy(effect,3);
			info.gameObject.SetActive (false); // Or Destroy(info.gameObject);
			GameManager.currentScore += 1;
			SetCountText();
		}
	}

	void SetCountText() {

		countText.text = "Count: " + GameManager.currentScore.ToString ();
		if ((GameManager.currentScore >= pickupMax) && (!GameManager.endGame)) {
			winText.text = "You Win!";
			GameManager.endGame = true;
			GameManager.currentLevel += 1;
			gameManager.LoadNextLevel(GameManager.currentLevel,3.0f);
		}
	}

	void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.CompareTag ("Wall")) {
			audio.clip = wallHit;
			audio.Play ();
		}
	}

	// Se ejecuta cuando el objeto esta colisionando con otro
	void OnCollisionStay() {

		if (isFalling) {
			audio.pitch = Random.Range(0.5f,1.5f);
			audio.clip = fallWallHit;
			audio.Play();
			isFalling = false;
		}
	}

	// Esto es simplemente para retrasar un poco el seteo de isFalling en TRUE. Si no lo hicieramos, y lo pusieramos apenas presionamos espacio en el FixedUpdate,
	// lo que pasaria es que por el orden de ejecucion del script (https://docs.unity3d.com/Manual/ExecutionOrder.html) luego del FixedUpdate se ejecutaria el
	// OnCollisionStay para el mismo frame, y como isFalling esta en TRUE pensaria que ya toco el piso cuando en realidad recien comenzo el salto...
	// De hecho, revisando el orden de ejecucion, por el simple hecho de poner el delay como una corutina, ya estamos retrasando el seteo para despues del
	// CollisionStay (i.e.: si pusieramos 0 en el WaitForeSeconds tambien funcionaria como queremos).

	IEnumerator DelayFall() {

		yield return new WaitForSeconds(0.0f);
		isFalling = true;
	}

	// A modo de prueba simplemente...
	bool IsGrounded() {

		// Esto lo que hace es lanzar un rayo desde cierta posicion, en cierta direccion y con cierta magnitud, y verificar si
		// intersecta algun collider de la escena. En este caso estamos lanzando un rayo desde el centro de la bola, en direccion
		// hacia abajo con una magnitud igual a la distancia inicial al suelo (mas un pequenio offset para estar seguros)
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}
}