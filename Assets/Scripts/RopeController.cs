using UnityEngine;
using System.Collections;

public class RopeController : MonoBehaviour {


	string layerName="Front";


	void Start () {
		InvokeRepeating("HandleRopeMovement", 0.5f, 1.3f);
		InvokeRepeating("ChangeLayer", 0.5f, 0.65f);

	}

	void Update () {
		
	}

	void HandleRopeMovement()
	{ 
		
		GameObject[] movingRopes = GameObject.FindGameObjectsWithTag("MovingRope");
		foreach (GameObject ropes in movingRopes) {

			ropes.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 900));
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


}
