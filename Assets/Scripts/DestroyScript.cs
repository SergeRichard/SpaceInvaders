using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("DestroySelf", 1f);
	}
	
	void DestroySelf() {
		Destroy (gameObject);
	}
}
