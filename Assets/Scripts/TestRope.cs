using UnityEngine;
using System.Collections;

public class TestRope : MonoBehaviour {


	public float speed;
	string layerName="Front";
	public GameObject admiration;
	public GameObject leftFasterIcon;
	public GameObject rightFaterIcon;

	public GameObject rope;
	GameObject thePlayer;

	PlayerController objPlayer;
	int oldJump=0;
	int newJump=1;
	float speedIncreaseFacor=0.05f;
	int increaseRate=5;

	private AudioSource audioSource;
	public AudioClip powerSound;
	public float volume;

	bool timeToJump;

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	void Start () {
		HideAdmiration ();
		HideFasterIcons ();
		thePlayer = GameObject.Find("Player");
		objPlayer = thePlayer.GetComponent<PlayerController>();
	}

	void Update () {
		if(!objPlayer.getGameOver())
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
			HideFasterIcons ();
		}

		//Change color depending on rope position
		if (rope.transform.eulerAngles.x <= 170 && rope.transform.eulerAngles.x >= 0) {
			//front
			//Color white
			SpriteRenderer renderer = rope.GetComponent<SpriteRenderer>();
			renderer.color = new Color32(255, 255, 255,255);
			ChangeLayer ("Front");
		} else {
			//back
			//Color the rope darker
			SpriteRenderer renderer = rope.GetComponent<SpriteRenderer>();
			renderer.color = new Color32(212, 153, 0,255);
			ChangeLayer ("Back");

		}
	
	}

	void CheckPlayerJumps()
	{
		int jumps = objPlayer.jumps;

		if (jumps % increaseRate==0) {
			newJump = jumps;
			if(newJump!=oldJump)
				IncreaseSpeed ();
		}

		if (jumps % 20 == 0) {
			speedIncreaseFacor = 0.08f;
		}

		if (jumps % 50 == 0) {
			speedIncreaseFacor = 0.13f;
		}
			
	}

	void IncreaseSpeed()
	{
		speed += speedIncreaseFacor;
		oldJump = newJump;
		ShowFasterIcons ();
	}

	void ChangeLayer(string val)
	{
		layerName = val;

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
		timeToJump = true;
	}

	public bool getTimeToJump()
	{
		return timeToJump;
	}

	void HideAdmiration()
	{
		admiration.SetActive (false);
		timeToJump = false;
	}

	void ShowFasterIcons()
	{
		leftFasterIcon.SetActive (true);
		rightFaterIcon.SetActive (true);
		PlayPowerUpSound ();
	}

	void HideFasterIcons()
	{
		leftFasterIcon.SetActive (false);
		rightFaterIcon.SetActive (false);
	}

	void PlayPowerUpSound()
	{
		audioSource.PlayOneShot(powerSound, volume);
	}
}
