using UnityEngine;
using System.Collections;

public class DogController : MonoBehaviour {

	public Sprite idleDog;
	public Sprite jumpDog;
	public float jumpForce;

	float movementSpeed=3f;
	Vector3 rightFromPlayer = new Vector3(2.5f,-3.2f);
	Vector3 awayToTheRight = new  Vector3(30f,-3.2f);
	int jumps = 0;
	GameObject thePlayer;
	PlayerController objPlayer;
	bool isOnRightFromPlayer=false;
	int counter=0;
	bool moveAway=false;
	GameObject theRope;
	TestRope objRope;
	bool readyToWalk=false;


	void Start () {
		//Dog will jump a random number of times between 1 and 10
		jumps = Random.Range (2, 11);
		thePlayer = GameObject.Find("Player");
		objPlayer = thePlayer.GetComponent<PlayerController>();
		theRope = GameObject.Find("Rope_Pixel");
		objRope = theRope.GetComponent<TestRope>();
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

			//count how many jumps the dog will jump
			if (counter < jumps) {
				//SwitchIdle();
				MimicPlayerHeight ();
				HandleInput ();
			} else {
				moveAway = true;
				if (counter == jumps) {
					readyToWalk = true;
					counter++;
				}
			}
		}
		if (moveAway) {
			GetComponent<SpriteRenderer> ().flipX = true;
			transform.position = Vector3.MoveTowards (transform.position, awayToTheRight, time);
		}
		if(readyToWalk)
		{
			GetComponent<Animator> ().enabled = true;
			readyToWalk = false;
		}
		ChangeLayer ();
	}

	void HandleInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Jump();
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			Jump ();
		}
	}
		

	void SwitchIdle()
	{
		GetComponent<Animator> ().enabled = false;
		GetComponent<SpriteRenderer> ().sprite =idleDog;
	}

	void MimicPlayerHeight()
	{
		SwitchJump ();
		Vector3 playerPosition=objPlayer.getPlayerPosition();
		float positionY = playerPosition.y - .30f;
		transform.position = new Vector3 (transform.position.x, positionY);
	}
	void Jump(){
		if (!objPlayer.isGrounded()) {
			if (objRope.getTimeToJump ()) {
				objPlayer.JumpWithDog ();
				counter++;
			}
		}
	}

	void ChangeLayer()
	{
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (sprite)
		{
			sprite.sortingOrder = 0;
			sprite.sortingLayerName = "Front";
		}
	}

	void SwitchJump()
	{
		GetComponent<Animator> ().enabled = false;
		GetComponent<SpriteRenderer> ().sprite =jumpDog;
	}
		
}
	
