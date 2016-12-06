using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float jumpForce;
	public bool grounded;
	public bool candoublejump;

	public int jumps;
	public int highScore;
	public Font pixelFont;
	string scoreText;
	string highScoreText;

	bool gameOver=false;

	public GameObject playerPrefab;
	public GameObject counter;

	public Sprite jumpSprite;
	public Sprite idleSprite;

	public Sprite counter1;
	public Sprite counter2;
	public Sprite counter3;

	private AudioSource audioSource;
	public AudioClip jumpSound;
	public AudioClip hitSound;
	public AudioClip startSound;
	public float volume;

	GameObject theRope;
	TestRope objRope;
	bool paused=false;

	int numPlayers=1;
	public GameObject player2Prefab;
	GameObject thePlayer2;
	Player2Controller objPlayer2;

	bool touch1Bool;
	bool touch2Bool;

	bool hasShowGameOverMessage=false;

	void Start () {
		highScore=PlayerPrefs.GetInt("highScore", highScore);
		theRope = GameObject.Find("Rope_Pixel");
		objRope = theRope.GetComponent<TestRope>();
		numPlayers=PlayerPrefs.GetInt("numPlayers", numPlayers);
		CheckNumPlayers ();
		StartWait ();
	}

	void CheckNumPlayers()
	{
		//if the number of players is 2, it will add a new player.
		if (numPlayers == 2) {
			Vector3 oldPosition = getPlayerPosition();
			movePlayer1ToPosition1 (oldPosition);
			SpawnPlayer2OnPosition2 (oldPosition);
		} 
	}

	void movePlayer1ToPosition1(Vector3 oldPosition)
	{
		//Position Player one on first position
		transform.position = new Vector3 (-1f, oldPosition.y, oldPosition.z);
	}

	void SpawnPlayer2OnPosition2(Vector3 oldPosition)
	{
		//Spawn player 2 and position it on second position
		Vector3 firePosition = new Vector3(1f, oldPosition.y, 0);
		GameObject player2 = player2Prefab;
		GameObject bPrefab = Instantiate(player2, firePosition, Quaternion.identity) as GameObject;
		thePlayer2 =  bPrefab;
		if (bPrefab != null) {
			objPlayer2 = bPrefab.GetComponent<Player2Controller> ();
		}
	}

	public GameObject getP2Reference()
	{
		return thePlayer2;
	}

	void StartWait(){
		//Stops a certain amount of seconds and shows counters
		StopTime ();
		ShowCounter ();
		StartCoroutine(WaitToStart());
	}

	void StartGame()
	{
		PlayStartSound ();
	}

	void StopTime()
	{
		if (!paused) {
			Time.timeScale = 0.00001f;
			paused = true;
		}

	}

	void ResumeTime()
	{
		if (paused) {
			Time.timeScale = 1;
			paused = false;
		}
	}

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	IEnumerator WaitToStart(){
		//Count to 3 seconds in real time
		int countUpTo = 3;
		float waitTime = 1;

		for (int i=0; i < countUpTo; i++) {
			//Pause here
			PlayStartSound();
			SwitchCounterAnimation (countUpTo - i);
			yield return new WaitForSeconds(waitTime*0.00001f);
		}
		ResumeTime ();
		HideCounter ();
	}
		
	// Update is called once per frame
	void Update () {
		HandleInput ();
		HandleJumpsText ();
		HandleScores ();
	}

	void HandleScores()
	{
		if (jumps > highScore) {
			highScore = jumps;
			PlayerPrefs.SetInt ("highScore", highScore);
		}

	}

	public int getScore()
	{
		return jumps;
	}

	void ShowCounter ()
	{
		counter.SetActive (true);
	}

	void HideCounter()
	{
		counter.SetActive (false);
	}

	void HandleInput()
	{
		if (!paused) {
			if (Input.GetMouseButtonDown(0))
			{
				if (numPlayers != 2)
					Jump();
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Application.LoadLevel("menu");
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				Jump();
			}
			if (Input.GetTouch(0).phase == TouchPhase.Stationary)
			{
				touch1Bool = true;

			}
			else
			{
				touch1Bool = false;
			}

			if (Input.GetTouch(1).phase == TouchPhase.Stationary)
			{
				touch2Bool = true;

			}
			else
			{
				touch2Bool = false;
			}

			if (touch1Bool && touch2Bool)
			{
				Jump();
				objPlayer2.Jump();
			}
		}
	}

	void SwitchCounterAnimation(int number)
	{
		switch (number) {
		case 1:
			counter.GetComponent<SpriteRenderer> ().sprite = counter1;
			break;
		case 2:
			counter.GetComponent<SpriteRenderer> ().sprite = counter2;
			break;
		case 3:
			counter.GetComponent<SpriteRenderer> ().sprite = counter3;
			break;
		default:
			HideCounter ();
			break;
		}
	}

	void SwitchIdleAnimation()
	{
		playerPrefab.GetComponent<SpriteRenderer> ().sprite =idleSprite;
	}

	void SwitchJumpAnimation()
	{
		playerPrefab.GetComponent<SpriteRenderer> ().sprite = jumpSprite;
	}

	//Handle ground contact
	void OnCollisionEnter2D(Collision2D coll)
	{
		//Contact with ground
		if (coll.gameObject.tag == "Ground")
		{
			grounded = true;
			SwitchIdleAnimation ();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Contact with rope
		if (!gameOver) {
			if (other.gameObject.tag == "MovingRope" && grounded==true) {
				string layer =other.GetComponent<SpriteRenderer>().sortingLayerName;
				if (layer == "Front") {
					gameOver = true;
					playerPrefab.transform.Rotate (Vector3.forward * -90);
					PlayHitSound ();
				}
			}
		}
	
	}

	void RestartGame()
	{
		Application.LoadLevel(Application.loadedLevel);
	}


	void Jump()
	{
		if (grounded && !gameOver) {
			SwitchJumpAnimation ();
			GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, 0);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
			candoublejump = true;
			grounded = false;
			if(objRope.getTimeToJump())
				jumps++;
			PlayJumpSound ();
		}
		if (gameOver) {
			RestartGame ();
		}
	}

	public bool isGrounded()
	{
		return grounded;
	}

	public void JumpWithDog()
	{
		if (grounded==false && !gameOver) {
			if (objRope.getTimeToJump ()) {
				jumps++;
				PlayJumpSound ();
			}

		}
	}

	public void HandleJumpsText()
	{
		scoreText = "Jumps : " + jumps;
		highScoreText = "High Score : " + highScore;
	}

	public Vector3 getPlayerPosition()
	{
		return transform.position;
	}
		

	void OnGUI()
	{

		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.red;
		style.fontSize = 40;
		style.fontStyle = FontStyle.Bold;

		GUI.skin.font = pixelFont;
		style.normal.textColor = Color.green;
		//GUI.Label(new Rect(20, 20, 100, 100), scoreText, style);

		AdvancedTextRendering.DrawOutline(new Rect(0,40,100,100), 
			scoreText, 
			style,
			Color.black,
			Color.green,
			0.9f);

		AdvancedTextRendering.DrawOutline(new Rect(0,80,100,100), 
			highScoreText, 
			style,
			Color.black,
			Color.white,
			0.9f);

		string gameOverMessage = "Try again";
		Color colorGameOver = Color.cyan;
		if (numPlayers == 2) {
			gameOverMessage = "P2 Win";
			colorGameOver = Color.magenta;
			if (objPlayer2.getGameOver ()) {
				gameOverMessage = "Draw";
				colorGameOver = Color.cyan;
			}
		}
		Color c = GUI.backgroundColor;
		GUI.backgroundColor = Color.clear;


		if (!gameOver && !paused) {
			if (numPlayers == 2) {
				if (!objPlayer2.getGameOver ()) {
					Rect rectP1 = new Rect (0, 0, (Screen.width / 2), (Screen.height));
					if( GUI.Button(rectP1,""))
					{
						if (!paused)
						{
							Jump();
						}
					}
					Rect rectP2 = new Rect ((Screen.width / 2), 0, (Screen.width / 2), (Screen.height));
					if( GUI.Button(rectP2,""))
					{
						if (!paused)
						{
							objPlayer2.Jump();
						}
					}

				}
		
			}
		}

		if (gameOver) {
				style.fontSize = 60;
				style.normal.textColor = Color.cyan;
				Rect rect = new Rect ((Screen.width) / 2 - 150, (Screen.height) / 2 - 50, 0, 0);
				//GUI.Label(rect, "Try again",style);
				AdvancedTextRendering.DrawOutline (rect, 
					gameOverMessage, 
					style,
					Color.black,
					colorGameOver,
					0.9f);
				StopTime ();
				Rect rect3 = new Rect(0, 0,Screen.width, Screen.height);
				GUI.depth = 1;
				if( GUI.Button(rect3,""))
				{
					ResumeTime();
					Jump();
				}
			}
		//GUI.backgroundColor = c;
	}

	public bool getGameOver()
	{
		return gameOver;
	}

	public void setGameOver()
	{
		gameOver = true;
	}

	void PlayJumpSound()
	{
		audioSource.PlayOneShot(jumpSound, volume);
	}

	void PlayHitSound()
	{
		audioSource.PlayOneShot(hitSound, volume/4);
	}

	void PlayStartSound()
	{
		audioSource.PlayOneShot(startSound, volume/4);
	}
}
