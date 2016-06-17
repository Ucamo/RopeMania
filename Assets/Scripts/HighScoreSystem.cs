using UnityEngine;
using System.Collections;

public class HighScoreSystem : MonoBehaviour {

	GameObject thePlayer;
	PlayerController objPlayer;

	int score;
	int highScore;
	bool writeOnScoreBoard=false;
	int lastHighScore;

	void Start () {

		thePlayer = GameObject.Find("Player");
		objPlayer = thePlayer.GetComponent<PlayerController>();
		highScore=PlayerPrefs.GetInt("highScore", highScore);
	
	}
	
	// Update is called once per frame
	void Update () {
		score = objPlayer.getScore ();
		ScoreHandler ();
	}

	void ScoreHandler()
	{
		if (objPlayer.getGameOver ()) {
			if (score > highScore) {
				highScore = score;
				SetHighScore (score);
			}
		}
	
	}

	void SetHighScore(int score)
	{
		bool isSignedIn = GameJolt.API.Manager.Instance.CurrentUser != null;
		if (isSignedIn) {
			int scoreValue = score; // The actual score.
			string scoreText = score+" jumps"; // A string representing the score to be shown on the website.
			int tableID = 162478; // Set it to 0 for main highscore table.
			string extraData = ""; // This will not be shown on the website. You can store any information.

			GameJolt.API.Scores.Add(scoreValue, scoreText, tableID, extraData, (bool success) => {
				//Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
			});
		} 
	
	}
}
