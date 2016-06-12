using UnityEngine;
using System.Collections;

public class TestRope : MonoBehaviour {


	public float speed;
	string layerName="Front";
	public GameObject admiration;
	public GameObject rope;
	GameObject thePlayer;

	PlayerController objPlayer;

	void Start () {
		HideAdmiration ();
		thePlayer = GameObject.Find("Player");
		objPlayer = thePlayer.GetComponent<PlayerController>();
	}

	void Update () {
		transform.Rotate(Quaternion.Euler(60, 0, 0) * Vector3.left, -240 * Time.deltaTime*speed);
		CheckRopePosition ();
		CheckPlayerJumps ();
	}

	void CheckRopePosition()
	{
		if (rope.transform.eulerAngles.x<=90 && rope.transform.eulerAngles.x>=30) {
			if(!objPlayer.getGameOver())
				ShowAdmiration ();
		} 
		else {
			HideAdmiration ();
		}
	
	}

	void CheckPlayerJumps()
	{
		int jumps = objPlayer.jumps;

		if (jumps % 5==0) {
			IncreaseSpeed ();
		}
			
	}

	void IncreaseSpeed()
	{
		speed += 0.001f;
	}

	void ChangeLayer()
	{
		if (layerName == "Front")
			layerName = "Back";
		else
			layerName="Front";

		GameObject[] movingRopes = GameObject.FindGameObjectsWithTag("MovingRope");
		foreach (GameObject ropes in movingRopes) {

			SpriteRenderer sprite = ropes.GetComponent<SpriteRenderer>();

			if (sprite)
			{
				sprite.sortingOrder = 1;
				sprite.sortingLayerName = layerName;
			}


		}

	}

	void ShowAdmiration()
	{
		admiration.SetActive (true);
	}

	void HideAdmiration()
	{
		admiration.SetActive (false);
	}
}
