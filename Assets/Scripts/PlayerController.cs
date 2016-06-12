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

	// Use this for initialization
	void Start () {
		highScore=PlayerPrefs.GetInt("highScore", highScore);
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
		//Contact with row
		if (!gameOver) {
			if (other.gameObject.tag == "MovingRope" && grounded==true) {
				string layer =other.GetComponent<SpriteRenderer>().sortingLayerName;
				if (layer == "Front") {
					gameOver = true;
					playerPrefab.transform.Rotate (Vector3.forward * -90);
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
			jumps++;
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

	public class AdvancedTextRendering{

		//draw text of a specified color, with a specified outline color
		public static void DrawOutline(Rect position ,string text, GUIStyle style, Color outColor, Color inColor, float strokeSize){
			GUIStyle backupStyle = style;
			var oldColor = style.normal.textColor;

			style.normal.textColor = outColor;
			position.x -= strokeSize;
			GUI.Label(position, text, style);
			position.x += strokeSize*2;
			GUI.Label(position, text, style);
			position.x -= strokeSize;
			position.y -= strokeSize;
			GUI.Label(position, text, style);
			position.y += strokeSize*2;
			GUI.Label(position, text, style);
			position.y -= strokeSize;
			style.normal.textColor = inColor;
			GUI.Label(position, text, style);

			style.normal.textColor = oldColor;
			style = backupStyle;
		}
	}


}
