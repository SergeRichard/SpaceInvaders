using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


	private float time;
	private float invaderTimeAccumulated;

	public GameObject PlayerShip;
	public GameObject Bullet;
	public Transform BulletSpawner;
	public InvadersController InvadersController;
	public float ShootAgainTime;
	public static float InvadersVerticalOffset;

	public float speed = 2f;
	public static float AnimateAndMoveInvadersTime;
	public MessageController MessageController;
	public LivesController LivesController;

	public static int GameScore;
	public static int NextLifeAchievements;
	public static int GameLevel;
	public static int Lives;
	public static Dictionary<string, int> InvaderDict;
	public enum GameStates
	{
		PlayerKilled, GamePaused, GamePausedByPlayer, Playing, GameOver, PlayerWins, IdleDuringReset
	}

	public static GameStates GameState;
	public GameOverController GameOverController;

	// Use this for initialization
	void Start () {
		// make sure that first shot at beginning of game can start right away.
		time = 3f;
		AnimateAndMoveInvadersTime = 1f;
		GameScore = 0;
		Lives = 3;
		NextLifeAchievements = 1000;
		GameLevel = 1;
		MessageController.ScoreValue.text = GameScore.ToString ();
		GameState = GameStates.Playing;
		InvaderDict = new Dictionary<string, int> () {{"InvaderBottom",10},{"InvaderMiddle",20},{"InvaderTop",30} };
		InvadersVerticalOffset = 0f;
		LivesController.DisplayLives ();

		//PlayerPrefs.DeleteKey ("High Score");
		MessageController.HighScoreValue.text = PlayerPrefs.GetInt ("High Score").ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && GameState != GameStates.GamePausedByPlayer) {
			GameState = GameStates.GamePausedByPlayer;
			GamePausedByPlayer ();
		}
		else if (Input.GetKeyDown (KeyCode.Escape) && GameState == GameStates.GamePausedByPlayer) {
			GameState = GameStates.Playing;
			ResumeGamePausedByPlayer ();
		}
		if (GameState == GameStates.Playing) {
			time += Time.deltaTime;
			invaderTimeAccumulated += Time.deltaTime;

			// if no bullet in scene, then able to shoot again.
			if (BulletSpawner.childCount == 0)
				time = ShootAgainTime;

			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
				PlayerShip.transform.Translate (Vector3.left * Time.deltaTime * speed);
				PlayerShip.transform.position = new Vector3 (Mathf.Clamp (PlayerShip.transform.position.x, -6.4F, 6.4F), PlayerShip.transform.position.y);
			} 
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
				PlayerShip.transform.Translate (Vector3.right * Time.deltaTime * speed);
				PlayerShip.transform.position = new Vector3 (Mathf.Clamp (PlayerShip.transform.position.x, -6.4F, 6.4F), PlayerShip.transform.position.y);
			}

			if (Input.GetKey (KeyCode.Space) && time >= ShootAgainTime) {
				time = 0f;
				PlayerShip.GetComponent<AudioSource> ().Play ();
				SpawnBullet ();
			}
			if (invaderTimeAccumulated >= AnimateAndMoveInvadersTime) {
				invaderTimeAccumulated = 0f;
				InvadersController.ChangeAnimationAndMove ();
			}
		}

		if (GameState == GameStates.PlayerKilled) {
			GameState = GameStates.GamePaused;
			PlayerShip.GetComponent<SpriteRenderer> ().enabled = false;
			Lives--;
			LivesController.DisplayLives ();
			//MessageController.LivesValue.text = Lives.ToString ();
			Invoke ("ResumeGame", 3f);
		}
		if (GameState == GameStates.PlayerWins) {
			PlayerPrefs.SetInt ("High Score", int.Parse(MessageController.HighScoreValue.text));
			PlayerPrefs.Save ();
			LivesController.DisplayLives ();
			Invoke ("ResetGame", 3f);
			GameState = GameStates.IdleDuringReset;
		}

	}
	void ResetGame() {
		InvadersVerticalOffset -= .2f;
		InvadersController.MaxTimeToShoot = InvadersController.MaxTimeToShootMaxReset;
		InvadersController.SpawnInvaders ();
		time = 3f;
		AnimateAndMoveInvadersTime = 1f;
		GameLevel++;
		Debug.Log ("GameLevel % 3 = " + (GameLevel % 3));
		if (GameLevel % 6 == 0) {
			InvadersVerticalOffset = 0;
		}
		GameState = GameStates.Playing;
	}
	void ResumeGame() {
		if (Lives <= 0) {
			LoadGameOver ();
		} else {
			PlayerShip.GetComponent<SpriteRenderer> ().enabled = true;
			GameState = GameStates.Playing;
		}

	}

	void LoadGameOver() {
		PlayerPrefs.SetInt ("High Score", int.Parse(MessageController.HighScoreValue.text));

		GameOverController.FadeInGameOver ();
		GameOverController.OnGameOverComplete += OnGameOverComplete;
		//SceneManager.LoadScene ("GameOver");
	}
	void OnGameOverComplete() {
		GameOverController.OnGameOverComplete -= OnGameOverComplete;

		PlayerPrefs.Save ();
		SceneManager.LoadScene ("TitleScreen");

	}
	void SpawnBullet() {
		Vector3 newPosition = new Vector3 (PlayerShip.transform.position.x, PlayerShip.transform.position.y + .5f, 0f);
		GameObject bullet = (GameObject)Instantiate (Bullet, newPosition, Quaternion.identity);
		bullet.transform.SetParent (BulletSpawner);
		GameState = GameStates.Playing;
	}
	void GamePausedByPlayer() {
		MessageController.PauseBackground.SetActive (true);
		MessageController.ResumeText.SetActive (true);
		MessageController.QuitText.SetActive (true);
	}
	public void ResumeGamePausedByPlayer() {
		GameState = GameStates.Playing;
		MessageController.PauseBackground.SetActive (false);
		MessageController.ResumeText.SetActive (false);
		MessageController.QuitText.SetActive (false);
	}
	public void QuitToTitleScreen() {
		SceneManager.LoadScene ("TitleScreen");
	}
}
