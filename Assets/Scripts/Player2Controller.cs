using UnityEngine;
using System.Collections;

public class Player2Controller : MonoBehaviour {

	public float jumpForce;
	public bool grounded;
	public bool candoublejump;

	public int jumps;
	public int highScoreP2;
	public Font pixelFont;
	string scoreText;
	string highScoreText;

	bool gameOver=false;

	public GameObject playerPrefab;
	public GameObject counter;

	public Sprite jumpSprite;
	public Sprite idleSprite;


	private AudioSource audioSource;
	public AudioClip jumpSound;
	public AudioClip hitSound;
	public AudioClip startSound;
	public float volume;

	GameObject theRope;
	TestRope objRope;
	bool paused=false;
	GameObject thePlayer;
	PlayerController objPlayer;


	// Use this for initialization
	void Start () {
		highScoreP2=PlayerPrefs.GetInt("highScoreP2", highScoreP2);
		theRope = GameObject.Find("Rope_Pixel");
		objRope = theRope.GetComponent<TestRope>();
		thePlayer = GameObject.Find("Player");
		objPlayer = thePlayer.GetComponent<PlayerController>();
		StartWait ();
	}

	void StartWait(){
		StopTime ();
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

		int countUpTo = 3;
		float waitTime = 1;

		for (int i=0; i < countUpTo; i++) {
			//Pause here
			PlayStartSound();
			yield return new WaitForSeconds(waitTime*0.00001f);
		}
		ResumeTime ();
	}

	// Update is called once per frame
	void Update () {
		HandleInput ();
		HandleJumpsText ();
		HandleScores ();
	}

	void HandleScores()
	{
		if (jumps > highScoreP2) {
			highScoreP2 = jumps;
			PlayerPrefs.SetInt ("highScoreP2", highScoreP2);
		}

	}

	public int getScore()
	{
		return jumps;
	}
		

	void HandleInput()
	{
		if (!paused) {
			if (Input.GetKeyDown(KeyCode.K))
			{
				Jump();
			}
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
		if (grounded && !gameOver && !objPlayer.getGameOver()) {
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
		highScoreText = "High Score : " + highScoreP2;
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

		AdvancedTextRendering.DrawOutline(new Rect(Screen.width/2,40,100,100), 
			scoreText, 
			style,
			Color.black,
			Color.magenta,
			0.9f);

		AdvancedTextRendering.DrawOutline(new Rect(Screen.width/2,80,100,100), 
			highScoreText, 
			style,
			Color.black,
			Color.white,
			0.9f);
		
		Color c = GUI.backgroundColor;
		GUI.backgroundColor = Color.clear;

		string gameOverMessage = "P1 Win";

		bool p1Lost = objPlayer.getGameOver ();
		if (!p1Lost) {
			if (gameOver) {
				style.fontSize = 60;
				style.normal.textColor = Color.cyan;
				Rect rect = new Rect ((Screen.width) / 2 - 150, (Screen.height) / 2 - 50, 0, 0);
				//GUI.Label(rect, "Try again",style);
				AdvancedTextRendering.DrawOutline (rect, 
					gameOverMessage, 
					style,
					Color.black,
					Color.green,
					0.9f);
				StopTime ();
				Rect rect3 = new Rect(0, 0,Screen.width, Screen.height);
				if( GUI.Button(rect3,""))
				{
					//Click on 2p mode
					ResumeTime();
					Jump();
				}
			}
		}
		GUI.backgroundColor = c;
	}

	public bool getGameOver()
	{
		return gameOver;
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
