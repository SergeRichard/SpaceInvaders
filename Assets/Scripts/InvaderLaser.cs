using UnityEngine;
using System.Collections;

public class InvaderLaser : MonoBehaviour {

	public GameObject PlayerExplosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.GameState == GameManager.GameStates.Playing)
			gameObject.transform.Translate (0f, -5f * Time.deltaTime, 0f);
		if (GameManager.GameState == GameManager.GameStates.PlayerKilled || 
			GameManager.GameState == GameManager.GameStates.GamePaused || 
			GameManager.GameState == GameManager.GameStates.PlayerWins || 
			GameManager.GameState == GameManager.GameStates.IdleDuringReset)
			Destroy (gameObject);
	}
	void OnCollisionEnter2D(Collision2D collision2D) {
		Debug.Log ("hit " + collision2D.collider.tag );
		if (collision2D.collider.tag == "Player") {
			GameManager.GameState = GameManager.GameStates.PlayerKilled;
			Instantiate (PlayerExplosion, collision2D.collider.transform.position, Quaternion.identity);
		}
		Destroy (gameObject);
	}
}
