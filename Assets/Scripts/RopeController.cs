using UnityEngine;
using System.Collections;

public class RopeController : MonoBehaviour {


	string layerName="Front";
	public float jumpForce;
	public float gravity;
	public float jumpInterval;
	public float changeLayerInterval;

	public GameObject admiration;

	void Start () {
		
		InvokeRepeating("HandleRopeMovement", 0.5f, jumpInterval);
		InvokeRepeating("ChangeLayer", 0.5f, changeLayerInterval);

	}

	void Update () {
		
	}

	void HandleRopeMovement()
	{ 
		
		GameObject[] movingRopes = GameObject.FindGameObjectsWithTag("MovingRope");
		foreach (GameObject ropes in movingRopes) {

			ropes.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
			ropes.GetComponent<Rigidbody2D> ().gravityScale = gravity;

		}
		HideAdmiration();
				
	}

	void ChangeLayer()
	{
		ShowAdmiration();
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
