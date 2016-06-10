using UnityEngine;
using System.Collections;

public class TestRope : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Quaternion.Euler(60, 0, 0) * Vector3.left, -240 * Time.deltaTime);
	}
}
