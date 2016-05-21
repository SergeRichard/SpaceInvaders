using UnityEngine;
using System.Collections;

public class LivesController : MonoBehaviour {

	public GameObject[] Lives;
	public MessageController MessageController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DisplayLives() {
		foreach (var life in Lives) {
			life.SetActive (false);
		}
		MessageController.LivesValueXText.SetActive (false);
		for(int count = 1; count < GameManager.Lives; count++) {
			if (count-1 < 15)
				Lives[count-1].SetActive (true);
		}
		if (GameManager.Lives > 16) {
			string extraLivesAmt = (GameManager.Lives - 15).ToString ();
			MessageController.LivesValue.text = extraLivesAmt;
			MessageController.LivesValueXText.SetActive (true);

		} else {
			MessageController.LivesValue.text = "";
		}
	}
}
