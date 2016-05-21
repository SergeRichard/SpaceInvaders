using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InvadersController : MonoBehaviour {

	public enum AnimationSequence { Anim1, Anim2};
	public enum SwarmDirection {left, right};
	public float InvaderSpeed;
	public float InvaderDownStep;
	public List<AudioClip> InvaderMoveSound;
	public AnimationSequence animSequence;
	public static SwarmDirection swarmDirection;
	public float MinTimeToShoot;
	public float MaxTimeToShoot;

	[HideInInspector]
	public float MaxTimeToShootMaxReset;

	public float AccumulatedTime;
	public GameObject InvaderLaser;
	public Transform InvaderLasers;
	public GameObject Invader_1;
	public GameObject Invader_2;
	public GameObject Invader_3;
	public Transform row1;
	public Transform row2;
	public Transform row3;
	public Transform row4;
	public Transform row5;


	private float randTimeToShoot;
	private AudioSource audioSource;
	private int soundIndex;



	// Use this for initialization
	void Start () {
		MaxTimeToShootMaxReset = MaxTimeToShoot;
		soundIndex = 0;
		animSequence = AnimationSequence.Anim2;
		swarmDirection = SwarmDirection.left;
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = InvaderMoveSound [0];
		AccumulatedTime = 0f;
		randTimeToShoot = Random.Range (MinTimeToShoot, MaxTimeToShoot);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.GameState == GameManager.GameStates.Playing) {
			AccumulatedTime += Time.deltaTime;

			if (AccumulatedTime >= randTimeToShoot) {
				AccumulatedTime = 0f;
				randTimeToShoot = Random.Range (MinTimeToShoot, MaxTimeToShoot);
				ShootInvaderLaser ();
			}

		}
	}
	public void SpawnInvaders() {
		Vector3 row1Start = new Vector3 (-3.5f, 2.105f, 0f);
		Vector3 row2Start = new Vector3 (-3.5f, 1.4f, 0f);
		Vector3 row3Start = new Vector3 (-3.5f, 0.7f, 0f);
		Vector3 row4Start = new Vector3 (-3.5f, 0f, 0f);
		Vector3 row5Start = new Vector3 (-3.5f, -0.7f, 0f);
		row1Start.y += GameManager.InvadersVerticalOffset;
		row2Start.y += GameManager.InvadersVerticalOffset;
		row3Start.y += GameManager.InvadersVerticalOffset;
		row4Start.y += GameManager.InvadersVerticalOffset;
		row5Start.y += GameManager.InvadersVerticalOffset;


		float xCoord = 0f;
		float xCoordInc = 0.7f;

		for (int x = 0; x < 11; x++) {
			
			GameObject invader = (GameObject)Instantiate (Invader_1, row1Start, Quaternion.identity);
			invader.transform.parent = row1.transform;
			row1Start.x += xCoordInc;

		}
		for (int x = 0; x < 11; x++) {

			GameObject invader = (GameObject)Instantiate (Invader_2, row2Start, Quaternion.identity);
			invader.transform.parent = row2.transform;
			row2Start.x += xCoordInc;

		}
		for (int x = 0; x < 11; x++) {

			GameObject invader = (GameObject)Instantiate (Invader_2, row3Start, Quaternion.identity);
			invader.transform.parent = row3.transform;
			row3Start.x += xCoordInc;

		}
		for (int x = 0; x < 11; x++) {

			GameObject invader = (GameObject)Instantiate (Invader_3, row4Start, Quaternion.identity);
			invader.transform.parent = row4.transform;
			row4Start.x += xCoordInc;

		}
		for (int x = 0; x < 11; x++) {

			GameObject invader = (GameObject)Instantiate (Invader_3, row5Start, Quaternion.identity);
			invader.transform.parent = row5.transform;
			row5Start.x += xCoordInc;

		}
	}
	void ShootInvaderLaser() {
		var invaders = GetComponentsInChildren<Invader> ();
		int invaderAmount = invaders.Length;
		Vector3 offset = new Vector3 (0f, -.35f, 0f);
		if (invaderAmount > 0) {
			if (invaderAmount == 1) {				
				GameObject laser = (GameObject)Instantiate (InvaderLaser, invaders [0].transform.position + offset, Quaternion.identity);
				laser.GetComponent<SpriteRenderer> ().color = invaders [0].GetComponent<SpriteRenderer> ().color;
				laser.transform.parent = InvaderLasers.transform;

			} else {
				int rand = Random.Range (0, invaderAmount);
				GameObject laser = (GameObject)Instantiate (InvaderLaser, invaders [rand].transform.position + offset, Quaternion.identity);
				laser.GetComponent<SpriteRenderer> ().color = invaders [rand].GetComponent<SpriteRenderer> ().color;
				laser.transform.parent = InvaderLasers.transform;


			}


		}
	}
	public void ChangeAnimationAndMove() 
	{
		var invaders = GetComponentsInChildren<Invader> ();
		switch (animSequence) {
		case AnimationSequence.Anim1:
			foreach (var invader in invaders) {
				invader.ChangeAnimation (animSequence);
				MoveInvader (invader);
			}
			animSequence = AnimationSequence.Anim2;
			break;
		case AnimationSequence.Anim2:
			foreach (var invader in invaders) {
				invader.ChangeAnimation (animSequence);
				MoveInvader (invader);
			}
			animSequence = AnimationSequence.Anim1;
			break;
		}
		PlayInvaderSound ();
	}
	void PlayInvaderSound() {
		audioSource.clip = InvaderMoveSound [soundIndex];
		audioSource.Play ();

		soundIndex++;
		if (soundIndex == 4)
			soundIndex = 0;
	}
	public void MoveInvader(Invader invader) {
		if (swarmDirection == SwarmDirection.left) {
			invader.transform.Translate (-InvaderSpeed, 0f, 0f);
		} else {
			invader.transform.Translate (InvaderSpeed, 0f, 0f);
		}
	}
	public void ReachedLimit() {
		var invaders = GetComponentsInChildren<Invader> ();
		foreach (var invader in invaders) {
			invader.transform.Translate (0f, -InvaderDownStep, 0f);

		}
	}

}
