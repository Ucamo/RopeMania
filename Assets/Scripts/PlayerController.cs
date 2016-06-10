using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float jumpForce;
	public bool grounded;
	public bool candoublejump;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput ();
	}

	void HandleInput()
	{
		
		if (Input.GetMouseButtonDown(0))
		{
			Jump();
		}




	}

	//Handle ground contact
	void OnCollisionEnter2D(Collision2D coll)
	{
		//Contact with ground
		if (coll.gameObject.tag == "Ground")
		{
			grounded = true;
		}
	}


	void Jump()
	{
		if (grounded) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, 0);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
			candoublejump = true;
			grounded = false;
		}
	}
}
