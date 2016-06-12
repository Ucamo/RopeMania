using UnityEngine;
using System.Collections;

public class TestRope : MonoBehaviour {


	public float speed;
	string layerName="Front";
	public GameObject admiration;
	public GameObject rope;

	void Start () {
		
	}

	void Update () {
		transform.Rotate(Quaternion.Euler(60, 0, 0) * Vector3.left, -240 * Time.deltaTime*speed);
		CheckRopePosition ();
	}

	void CheckRopePosition()
	{
		Quaternion rotation = rope.transform.rotation;
		if (rotation.y == 0) {
			if (rotation.x >= 0 &&rotation.x<=30) {
				ShowAdmiration ();
			} 
			else {
				HideAdmiration ();
			}
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
