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

	public Sprite jumpSprite;
	public Sprite idleSprite;

	private AudioSource audioSource;
	public AudioClip jumpSound;
	public AudioClip hitSound;
	public AudioClip startSound;
	public float volume;

	GameObject theRope;

	TestRope objRope;


	// Use this for initialization
	void Start () {
		PlayStartSound ();
		highScore=PlayerPrefs.GetInt("highScore", highScore);
		theRope = GameObject.Find("Rope_Pixel");
		objRope = theRope.GetComponent<TestRope>();
	}

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
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

	public void HandleJumpsText()
	{
		scoreText = "Jumps : " + jumps;
		highScoreText = "High Score : " + highScore;
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

		AdvancedTextRendering.DrawOutline(new Rect(0,0,100,100), 
			scoreText, 
			style,
			Color.black,
			Color.green,
			0.9f);

		AdvancedTextRendering.DrawOutline(new Rect(0,40,100,100), 
			highScoreText, 
			style,
			Color.black,
			Color.white,
			0.9f);

		if (gameOver) {
			style.fontSize = 60;
			style.normal.textColor = Color.cyan;
			Rect rect = new Rect((Screen.width)/2-150, (Screen.height)/2-50, 0, 0);
			//GUI.Label(rect, "Try again",style);
			AdvancedTextRendering.DrawOutline(rect, 
				"Try again", 
				style,
				Color.black,
				Color.cyan,
				0.9f);
		}
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
