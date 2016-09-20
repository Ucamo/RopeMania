using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HolderControllers : MonoBehaviour {

	public GameObject leftHolder;
	public GameObject rightHolder;
	public GameObject player;
	public GameObject leftFasterIcon;
	public GameObject rightFaterIcon;
	public Sprite idleSpritePlayer;
	public Sprite idleSpriteLeftHolder;
	public Sprite idleSpriteRightHolder;
	public Font pixelFont;

	bool center;
	bool arrive;
	Vector3 startLeft;
	Vector3 startRight;
	Vector3 startPlayer;
	Vector3 centerPosition = new Vector3(0f,-2.8f);

	Vector3 positionLeft ;
	Vector3 positionRight ;
	Vector3 positionPlayer ;

	string start="";
	string ropemania="";
	string twoPlayerMode="";

	int numPlayers=1;

	void Start () {
		startLeft = leftHolder.transform.position;
		startRight = rightHolder.transform.position;
		startPlayer = player.transform.position;
		HideFasterIcons ();
		start = "Start";
		ropemania = "Ropemania";
		twoPlayerMode = "2P Mode";
		PlayerPrefs.SetInt ("numPlayers", numPlayers);
	}

	void Update () {
		HandleMovement ();
	}

	void HandleMovement()
	{
		 positionLeft = leftHolder.transform.position;
		 positionRight = rightHolder.transform.position;
		 positionPlayer = player.transform.position;
		float speed = Time.deltaTime;


		if (positionLeft != positionRight && !center) {
			leftHolder.transform.position = Vector3.MoveTowards (leftHolder.transform.position, rightHolder.transform.position, speed);
			rightHolder.transform.position = Vector3.MoveTowards (rightHolder.transform.position, leftHolder.transform.position, speed);
		}
		if (positionLeft == centerPosition) {
			center = true;	
		}

		if (rightHolder.GetComponent<Rigidbody2D> ().IsTouching(leftHolder.GetComponent<Collider2D> ())) {
			center = true;
		}

		if (center) {
			leftHolder.GetComponent<SpriteRenderer> ().flipX = true;
			rightHolder.GetComponent<SpriteRenderer> ().flipX = true;
			leftHolder.transform.position = Vector3.MoveTowards (leftHolder.transform.position, startLeft, speed);
			rightHolder.transform.position = Vector3.MoveTowards (rightHolder.transform.position, startRight, speed);
		}
		if (center && positionLeft == startLeft && positionRight == startRight) {
			leftHolder.GetComponent<SpriteRenderer> ().flipX = false;
			rightHolder.GetComponent<SpriteRenderer> ().flipX = false;
			center = false;
		}
		if (positionPlayer != startRight && !arrive) {
			player.transform.position = Vector3.MoveTowards (player.transform.position, startRight, speed);

		} 
		if (positionPlayer == startRight) {
			arrive = true;

		}


		if (arrive) {
			player.transform.position = Vector3.MoveTowards (positionPlayer, centerPosition, speed*2);
			leftHolder.transform.position = Vector3.MoveTowards (leftHolder.transform.position, startLeft, speed);
			rightHolder.transform.position = Vector3.MoveTowards (rightHolder.transform.position, startRight, speed);
			ShowFasterIcons ();
		}

		if (arrive && positionLeft == startLeft) {
			SwitchIdleLeft ();
			HideFasterIcons ();
		}
		if (arrive && positionRight == startRight) {
			rightHolder.GetComponent<SpriteRenderer> ().flipX = false;
			SwitchIdleRight ();
			HideFasterIcons ();
		}

		if (positionPlayer == centerPosition) {
			HideFasterIcons ();
			SwitchIdleAnimation ();
		}
	}

	public Vector3 getPositionL()
	{
		return positionLeft;
	}
	public Vector3 getPositionR()
	{
		return positionRight;
	}

	void SwitchIdleLeft()
	{
		leftHolder.GetComponent<Animator> ().Stop ();
		leftHolder.GetComponent<SpriteRenderer> ().sprite =idleSpriteLeftHolder;
	}
	void SwitchIdleRight(){
		rightHolder.GetComponent<Animator> ().Stop ();
		rightHolder.GetComponent<SpriteRenderer> ().sprite =idleSpriteRightHolder;
	}

	void SwitchIdleAnimation()
	{
		player.GetComponent<Animator> ().Stop ();
		player.GetComponent<SpriteRenderer> ().sprite =idleSpritePlayer;
	}
		

	void ShowFasterIcons()
	{
		leftFasterIcon.SetActive (true);
		rightFaterIcon.SetActive (true);
	}

	void HideFasterIcons()
	{
		leftFasterIcon.SetActive (false);
		rightFaterIcon.SetActive (false);
	}

	void OnGUI()
	{

		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.red;
		style.fontSize = 40;
		style.fontStyle = FontStyle.Bold;

		GUI.skin.font = pixelFont;
		style.normal.textColor = Color.green;

		float w = 100;
		float h = 100;
		Rect rect = new Rect((Screen.width-w)/2, (Screen.height-h)/2+50, w+50, h/2);


		AdvancedTextRendering.DrawOutline(rect, 
			start, 
			style,
			Color.black,
			Color.green,
			0.9f);

		style = new GUIStyle();
		style.normal.textColor = Color.red;
		style.fontSize = 80;
		style.fontStyle = FontStyle.Bold;

		Rect rect2 = new Rect((Screen.width-w)/4, (Screen.height-h)/2-100, (Screen.width/2)+100	, h);
	
		Color c = GUI.backgroundColor;
		GUI.backgroundColor = Color.clear;

		if( GUI.Button(rect,""))
		{
			Application.LoadLevel("level1");
		}



		AdvancedTextRendering.DrawOutline(rect2, 
			ropemania, 
			style,
			Color.white,
			Color.red,
			2f);

		Rect rect3 = new Rect((Screen.width-w)/2, (Screen.height-h)/2+100, w+100, h/2);

		if( GUI.Button(rect3,""))
		{
			//Click on 2p mode
			PlayerPrefs.SetInt ("numPlayers", 2);
			Application.LoadLevel("level1");
		}

		GUI.backgroundColor = c;

		style.normal.textColor = Color.yellow;
		style.fontSize = 40;
		style.fontStyle = FontStyle.Bold;

		AdvancedTextRendering.DrawOutline(rect3, 
			twoPlayerMode, 
			style,
			Color.black,
			Color.yellow,
			0.9f);
	}
}
