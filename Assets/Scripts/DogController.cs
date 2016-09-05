using UnityEngine;
using System.Collections;

public class DogController : MonoBehaviour {

	float movementSpeed=10f;
	void Start () {
	
	}

	void Update () {
		transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
	}
}
