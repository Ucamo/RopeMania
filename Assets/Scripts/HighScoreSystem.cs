using UnityEngine;
using System.Collections;

public class HighScoreSystem : MonoBehaviour {

	GameObject thePlayer;
	PlayerController objPlayer;

	int score;
	int highScore;
	bool writeOnScoreBoard=false;
	int lastHighScore;
	int achievedTrophyId;

	void Start () {

		thePlayer = GameObject.Find("Player");
		objPlayer = thePlayer.GetComponent<PlayerController>();
		highScore=PlayerPrefs.GetInt("highScore", highScore);
		achievedTrophyId = PlayerPrefs.GetInt ("60315", achievedTrophyId);
	
	}
	
	// Update is called once per frame
	void Update () {
		score = objPlayer.getScore ();
		ScoreHandler ();
		TrophyHandler ();
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

	void UnlockTrophy(int idTrophy)
	{
		achievedTrophyId=PlayerPrefs.GetInt (idTrophy.ToString(), achievedTrophyId);
		if (achievedTrophyId != idTrophy) {
			GameJolt.API.Trophies.Unlock (idTrophy);
			PlayerPrefs.SetInt (idTrophy.ToString(), idTrophy);
		}
	}

	void TrophyHandler()
	{
		int idTrophy = 0;
		if (!objPlayer.getGameOver ()) {
			if (score >= 10) {
				idTrophy = 60315;
				UnlockTrophy (idTrophy);
			}
			if (score >= 20) {
				idTrophy = 60316;
				UnlockTrophy (idTrophy);
			}
			if (score >= 30) {
				idTrophy = 60317;
				UnlockTrophy (idTrophy);
			}
			if (score >= 50) {
				idTrophy = 60318;
				UnlockTrophy (idTrophy);
			}
			if (score >= 60) {
				idTrophy = 60319;
				UnlockTrophy (idTrophy);
			}
			if (score >= 70) {
				idTrophy = 60320;
				UnlockTrophy (idTrophy);
			}
			if (score >= 100) {
				idTrophy = 60322;
				UnlockTrophy (idTrophy);
			}
			if (score >= 120) {
				idTrophy = 60323;
				UnlockTrophy (idTrophy);
			}
			if (score >= 130) {
				idTrophy = 60324;
				UnlockTrophy (idTrophy);
			}
			if (score >= 150) {
				idTrophy = 60325;
				UnlockTrophy (idTrophy);
			}
			if (score >= 200) {
				idTrophy = 60326;
				UnlockTrophy (idTrophy);
			}
			if (score >= 250) {
				idTrophy = 60327;
				UnlockTrophy (idTrophy);
			}
			if (score >= 300) {
				idTrophy = 60328;
				UnlockTrophy (idTrophy);
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
