using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mothership : MonoBehaviour {

	private List<int> scores;

	protected MessageController MessageController;
	protected MothershipController MothershipController;
	public GameObject MothershipScore;

	// Use this for initialization
	void Start () {
		scores = new List<int> () { 50, 100, 150, 200, 250, 300, 350, 400, 450, 500 };

		MessageController = FindObjectOfType(typeof(MessageController)) as MessageController;
		MothershipController = FindObjectOfType (typeof(MothershipController)) as MothershipController;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D collision2D) {
		if (collision2D.collider.tag == "bullet") {
			AddScore ();

			MessageController.ScoreValue.text = GameManager.GameScore.ToString ();
			MothershipController.MothershipState = MothershipController.MothershipStates.Idle;
			Destroy(gameObject);
		}

	}
	void AddScore() {
		int rand = Random.Range (0, scores.Count);

		GameManager.GameScore += scores[rand];
		Debug.Log ("Mothership score " + scores [rand]);

		GameObject m = (GameObject)Instantiate (MothershipScore, transform.position + new Vector3(-.3f,.15f, 0f), Quaternion.identity);
		m.GetComponent<TextMesh> ().text = scores [rand].ToString ();
	}

}
