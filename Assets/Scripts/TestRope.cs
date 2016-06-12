using UnityEngine;
using System.Collections;

public class TestRope : MonoBehaviour {


	public float speed;
	string layerName="Front";
	public GameObject admiration;
	public GameObject rope;

	PlayerController objPlayer;

	void Start () {
		HideAdmiration ();
		GameObject thePlayer = GameObject.Find("Player");
		objPlayer = thePlayer.GetComponent<PlayerController>();
	}

	void Update () {
		transform.Rotate(Quaternion.Euler(60, 0, 0) * Vector3.left, -240 * Time.deltaTime*speed);
		CheckRopePosition ();
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
