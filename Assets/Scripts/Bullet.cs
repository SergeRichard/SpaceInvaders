using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject Explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.GameState == GameManager.GameStates.Playing)
			gameObject.transform.Translate (0f, 10f * Time.deltaTime, 0f);
		if (GameManager.GameState == GameManager.GameStates.PlayerKilled || 
			GameManager.GameState == GameManager.GameStates.GamePaused || 
			GameManager.GameState == GameManager.GameStates.PlayerWins || 
			GameManager.GameState == GameManager.GameStates.IdleDuringReset)
				Destroy (gameObject);
	}
	void OnCollisionEnter2D(Collision2D collision2D) {
		Debug.Log ("hit " + collision2D.collider.tag);
		if (collision2D.collider.tag == "invader" || collision2D.collider.tag == "mothership")
			Instantiate (Explosion, collision2D.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
