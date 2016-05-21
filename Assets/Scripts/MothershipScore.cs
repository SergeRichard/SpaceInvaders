using UnityEngine;
using System.Collections;

public class MothershipScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("DestroyScore", 2f);
	}
	void DestroyScore() {
		Destroy (gameObject);
	}
	

}
