using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	// Update is called once per frame
	void Update () {

		if (gameObject.CompareTag ("Pick Up"))
			transform.Rotate(new Vector3(15,30,45) * Time.deltaTime);
		else
			if (gameObject.CompareTag ("Bad Pick Up"))
				transform.Rotate(new Vector3(0,30,0) * Time.deltaTime);
	}
}
