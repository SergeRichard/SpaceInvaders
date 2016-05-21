using UnityEngine;
using System.Collections;

public class MoveMothershipLeft : MonoBehaviour {

	private MothershipController mothershipController;
	protected AudioSource audioSource;
	public float speed = -2f;

	// Use this for initialization
	void Start () {
		mothershipController = (MothershipController)FindObjectOfType (typeof(MothershipController));
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.GameState == GameManager.GameStates.Playing) {
			if (!audioSource.isPlaying) {
				audioSource.Play ();
			}
			gameObject.transform.Translate (speed * Time.deltaTime, 0f, 0f);
			if (transform.position.x <= mothershipController.LeftSide.transform.position.x) {
				mothershipController.MothershipState = MothershipController.MothershipStates.Idle;
				Destroy (gameObject);
			}
		} else {
			audioSource.Stop ();
		}
	}
}
