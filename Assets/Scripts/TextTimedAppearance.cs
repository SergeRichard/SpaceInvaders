using UnityEngine;
using System.Collections;

public class TextTimedAppearance : MonoBehaviour {

	public GameObject[] ImagesAndTextToShow;

	// Use this for initialization
	void Start () {
		foreach (var imageAndTextToShow in ImagesAndTextToShow) {
			imageAndTextToShow.SetActive (false);
		}
		StartCoroutine (DisplayTextTimer ());
	}
	
	IEnumerator DisplayTextTimer() {
		foreach (var imageAndTextToShow in ImagesAndTextToShow) {

			yield return new WaitForSeconds(.3f);
			imageAndTextToShow.SetActive (true);


		}
	}
}
