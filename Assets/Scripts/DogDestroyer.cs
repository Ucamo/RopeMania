using UnityEngine;
using System.Collections;

public class DogDestroyer : MonoBehaviour {

	GameObject theSpawner;
	DogSpawner objSpawner;
	void Start () {
	
	}

	void Update () {

	}

	//Handle dog contact
	public void OnTriggerEnter2D(Collider2D node) {
		if(node.gameObject.tag == "dog")
		{
			theSpawner = GameObject.Find("DogSpawner");
			objSpawner = theSpawner.GetComponent<DogSpawner>();
			objSpawner.setThereIsADog (false);
			Destroy(node.gameObject);
		}
	}
}
