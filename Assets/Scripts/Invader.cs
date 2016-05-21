using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {
	
	public Sprite sprite1;
	public Sprite sprite2;
	public SpriteRenderer spriteRenderer;
	public InvadersController invadersController;
	protected MessageController MessageController;
	public GameObject PlayerExplosion;
	public GameObject Explosion;

	public string InvaderType;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		MessageController = FindObjectOfType(typeof(MessageController)) as MessageController;
		invadersController = FindObjectOfType (typeof(InvadersController)) as InvadersController;
	}

	void OnCollisionEnter2D(Collision2D collision2D) {
		if (collision2D.collider.tag == "bullet") {
			GameManager.AnimateAndMoveInvadersTime -= .018f;
			invadersController.MaxTimeToShoot -= .06f;
			AddScore ();
			MessageController.ScoreValue.text = GameManager.GameScore.ToString ();
			if (int.Parse (MessageController.HighScoreValue.text) < GameManager.GameScore) {
				MessageController.HighScoreValue.text = GameManager.GameScore.ToString ();
			}
			CheckIfInvadersLeft ();

			Destroy(gameObject);
		}
		if (collision2D.collider.tag == "Player") {

			GameManager.GameState = GameManager.GameStates.PlayerKilled;
			Instantiate (PlayerExplosion, collision2D.collider.transform.position, Quaternion.identity);
			Instantiate (Explosion, collision2D.collider.transform.position, Quaternion.identity);

			if (GameManager.Lives > 1) {
				CheckIfInvadersLeft ();
			}
			Destroy (gameObject);
		}
	}
	void CheckIfInvadersLeft() {
		
		GameObject[] invaders = GameObject.FindGameObjectsWithTag("invader");
		int invaderCount = invaders.Length;

		// if the count is one, than this is the last invader which will be destroyed
		if (invaderCount == 1) {
			GameManager.GameState = GameManager.GameStates.PlayerWins;
			GameManager.Lives++;
			//MessageController.LivesValue.text = GameManager.Lives.ToString();
		}
	}
	void AddScore() {
		switch (InvaderType) {
		case "InvaderTop":
			GameManager.GameScore += GameManager.InvaderDict ["InvaderTop"];
			break;

		case "InvaderMiddle":
			GameManager.GameScore += GameManager.InvaderDict ["InvaderMiddle"];
			break;

		case "InvaderBottom":
			GameManager.GameScore += GameManager.InvaderDict ["InvaderBottom"];
			break;

		}
		//CheckIfLifeGained ();
	}
//	void CheckIfLifeGained () {
//		if (GameManager.GameScore >= GameManager.NextLifeAchievements) {
//			GameManager.Lives++;
//			MessageController.LivesValue.text = GameManager.Lives.ToString();
//
//			GameManager.NextLifeAchievements += 1000;
//		}
//
//	}
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.transform.tag == "left limit" && InvadersController.swarmDirection == InvadersController.SwarmDirection.left) {
			invadersController.ReachedLimit ();
			InvadersController.swarmDirection = InvadersController.SwarmDirection.right;
		}
		if (collider.transform.tag == "right limit" && InvadersController.swarmDirection == InvadersController.SwarmDirection.right) {
			invadersController.ReachedLimit ();
			InvadersController.swarmDirection = InvadersController.SwarmDirection.left;
		}
	}
	public void ChangeAnimation(InvadersController.AnimationSequence animSequence) {
		if (animSequence == InvadersController.AnimationSequence.Anim1) {
			spriteRenderer.sprite = sprite1;
		}
		if (animSequence == InvadersController.AnimationSequence.Anim2) {
			spriteRenderer.sprite = sprite2;
		}
	}

}
