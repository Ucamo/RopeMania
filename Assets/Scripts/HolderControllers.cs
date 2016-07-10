using UnityEngine;
using System.Collections;

public class HolderControllers : MonoBehaviour {

	public GameObject leftHolder;
	public GameObject rightHolder;
	bool center;
	Vector3 startLeft;
	Vector3 startRight;
	void Start () {
		startLeft = leftHolder.transform.position;
		startRight = rightHolder.transform.position;
	}
	

	void Update () {
		Vector3 positionLeft = leftHolder.transform.position;
		Vector3 positionRight = rightHolder.transform.position;
		if (positionLeft != positionRight && !center) {
			leftHolder.transform.position = Vector3.MoveTowards (leftHolder.transform.position, rightHolder.transform.position, Time.deltaTime);
			rightHolder.transform.position = Vector3.MoveTowards (rightHolder.transform.position, leftHolder.transform.position, Time.deltaTime);
		}
		if (positionLeft == positionRight) {
			center = true;
		}
		if (center) {
			leftHolder.GetComponent<SpriteRenderer> ().flipX = true;
			rightHolder.GetComponent<SpriteRenderer> ().flipX = true;
			leftHolder.transform.position = Vector3.MoveTowards (leftHolder.transform.position, startLeft, Time.deltaTime);
			rightHolder.transform.position = Vector3.MoveTowards (rightHolder.transform.position, startRight, Time.deltaTime);
		}
		if (center && positionLeft == startLeft && positionRight == startRight) {
			leftHolder.GetComponent<SpriteRenderer> ().flipX = false;
			rightHolder.GetComponent<SpriteRenderer> ().flipX = false;
			center = false;
		}
	}
}
