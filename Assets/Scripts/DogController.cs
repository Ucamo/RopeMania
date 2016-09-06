using UnityEngine;
using System.Collections;

public class DogController : MonoBehaviour {

	public Sprite idleDog;
	public Sprite jumpDog;
	public float jumpForce;

	float movementSpeed=3f;
	Vector3 rightFromPlayer = new Vector3(3f,-2.8f);
	int jumps = 0;

	void Start () {
		//Dog will jump a random number of times between 1 and 10
		jumps = Random.Range (1, 11);
	}

	void Update () {
		HandleMovement ();
	}

	void HandleMovement(){
		//Move towards the right spot from the player
		float time = Time.deltaTime*movementSpeed;
		Vector3 currentPosition = transform.position;
		transform.position = Vector3.MoveTowards (transform.position, rightFromPlayer, time);

		if (currentPosition == rightFromPlayer) {
			//Stop walking, start jumping.
			//SwitchIdle();
			Jump ();
		}
	}

	void SwitchIdle()
	{
		GetComponent<Animator> ().Stop ();
		GetComponent<SpriteRenderer> ().sprite =idleDog;
	}

	void Jump(){
		//SwitchJump ();
		GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, 0);
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
	}

	void SwitchJump()
	{
		GetComponent<Animator> ().Stop ();
		GetComponent<SpriteRenderer> ().sprite =jumpDog;
	}

}
