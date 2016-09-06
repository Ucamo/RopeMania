using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour {

	public GameObject[] cars;
	public bool spawnToLeft;
	public float speed;
	public float respawnTime;
	public float startTime;
	void Start () {
		InvokeRepeating("InstanceCar", startTime, respawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InstanceCar()
	{
		int modifier = spawnToLeft ? -1 : 1;
		Vector2 forceVector = spawnToLeft ? Vector2.left : Vector2.right;
		Vector3 firePosition = new Vector3(transform.position.x + (1.5f * modifier), transform.position.y - 0.75f, 0);
		int index = Random.Range (0, cars.Length-1);
		GameObject randomCar = cars [index];
		GameObject bPrefab = Instantiate(randomCar, firePosition, Quaternion.identity) as GameObject;
		if (!spawnToLeft) {
			bPrefab.GetComponent<SpriteRenderer> ().flipX = true;
			bPrefab.GetComponent<SpriteRenderer> ().sortingOrder =- 2;
		} else {
			bPrefab.GetComponent<SpriteRenderer> ().sortingOrder =- 1;
		}
		bPrefab.GetComponent<Rigidbody2D>().AddForce(forceVector * speed);
	}
}
