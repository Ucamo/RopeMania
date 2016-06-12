using UnityEngine;
using System.Collections;

public class CarDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Handle car contact
	public void OnTriggerEnter2D(Collider2D node) {
		if(node.gameObject.tag == "Car")
		{
			Destroy(node.gameObject);
		}
	}
}
