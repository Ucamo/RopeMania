using UnityEngine;
using System.Collections;

public class DogController : MonoBehaviour {

	public Sprite idleDog;
	public Sprite jumpDog;
	public float jumpForce;

	float movementSpeed=3f;
	Vector3 rightFromPlayer = new Vector3(2.5f,-2.8f);
	Vector3 awayToTheRight = new  Vector3(4f,-2.8f);
	int jumps = 0;
	GameObject thePlayer;
	PlayerController objPlayer;
	bool isOnRightFromPlayer=false;
	int counter=0;
	bool moveAway=false;


	void Start () {
		//Dog will jump a random number of times between 1 and 10
		jumps = Random.Range (2, 11);
		thePlayer = GameObject.Find("Player");
		objPlayer = thePlayer.GetComponent<PlayerController>();

	}

	void Update () {
		if (!objPlayer.getGameOver ()) {
			HandleMovement ();
		}

	}

	void HandleMovement(){
		//Move towards the right spot from the player
		float time = Time.deltaTime*movementSpeed;
		Vector3 currentPosition = transform.position;
		if(!isOnRightFromPlayer)
			transform.position = Vector3.MoveTowards (transform.position, rightFromPlayer, time);

		if (currentPosition == rightFromPlayer) {
			isOnRightFromPlayer = true;
		}

		if (isOnRightFromPlayer) {
			//Stop walking, start jumping.
			SwitchIdle();
			//count how many jumps the dog will jump
			if (counter < jumps) {
				Jump ();
				counter++;
			} else {
				moveAway = true;
			}
		}
		if (moveAway) {
			//GetComponent<Animator> ().Play ("dog_walk_1");
			transform.position = Vector3.MoveTowards (transform.position, awayToTheRight, time);
		}
	}
		

	void SwitchIdle()
	{
		GetComponent<Animator> ().Stop ();
		GetComponent<SpriteRenderer> ().sprite =idleDog;
	}

	void Jump(){
		SwitchJump ();
		Vector3 playerPosition=objPlayer.getPlayerPosition();
		transform.position = new Vector3 (transform.position.x, playerPosition.y);

	}

	void SwitchJump()
	{
		GetComponent<Animator> ().Stop ();
		GetComponent<SpriteRenderer> ().sprite =jumpDog;
	}
		
}
	
