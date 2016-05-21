using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour {

	public delegate void GameOverComplete ();
	public static event GameOverComplete OnGameOverComplete;

	public GameObject FadePlane;
	public GameObject GameOverText;

	void Start() {
		FadePlane.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);
		GameOverText.GetComponent<Text> ().color = new Color (1, 1, 1, 0);

	}

	public void FadeInGameOver() {
		StartCoroutine (FadeIn ());
	}
	IEnumerator FadeIn() {
		float fadeAmount = 0;

		while (fadeAmount < 1) {
			fadeAmount += .01f;
			FadePlane.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, fadeAmount);
			GameOverText.GetComponent<Text> ().color = new Color (1, 1, 1, fadeAmount);
			yield return new WaitForSeconds (.01f);
		}
		Invoke ("Complete", 3f);
	}
	void Complete() {
		if (OnGameOverComplete != null)
			OnGameOverComplete ();
	}
}
