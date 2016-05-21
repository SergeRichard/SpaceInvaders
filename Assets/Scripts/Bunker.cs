using UnityEngine;
using System.Collections;

public class Bunker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log ("Bunker is hit!");
		if (collider.tag != "invader") {
			Destroy (collider.gameObject);
			Destroy (gameObject);
		}
	}

}
