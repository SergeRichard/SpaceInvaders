using UnityEngine;
using System.Collections;

public class MothershipController : MonoBehaviour {

	public enum MothershipStates {Idle, SpawnedLeftSide, SpawnedRightSide}
	public MothershipStates MothershipState;

	public enum SidesToSpawn {left, right}
	public SidesToSpawn SideToSpawn;

	public GameObject MothershipMovingLeft;
	public GameObject MothershipMovingRight;
	public Transform LeftSide;
	public Transform RightSide;
	public float minSpawnTime;
	public float maxSpawnTime;

	private float time;
	private float spawnTime;


	// Use this for initialization
	void Start () {
		time = 0f;
		spawnTime = Random.Range (minSpawnTime, maxSpawnTime);
		ChooseSideRandomly ();
	}

	void ChooseSideRandomly ()
	{
		if (Random.Range (0, 2) == 0) {
			SideToSpawn = SidesToSpawn.left;
		}
		else {
			SideToSpawn = SidesToSpawn.right;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.GameState == GameManager.GameStates.Playing) {
			if (MothershipState == MothershipStates.Idle) {
				time += Time.deltaTime;
				if (time >= spawnTime) {
					time = 0f;
					spawnTime = Random.Range (minSpawnTime, maxSpawnTime);

					GameObject spawn;

					if (SideToSpawn == SidesToSpawn.left) {
						spawn = (GameObject)Instantiate (MothershipMovingRight, LeftSide.position, Quaternion.identity);
						spawn.transform.parent = LeftSide;
						MothershipState = MothershipStates.SpawnedLeftSide;
						ChooseSideRandomly ();
					}
					else if (SideToSpawn == SidesToSpawn.right) {
						spawn = (GameObject)Instantiate (MothershipMovingLeft, RightSide.position, Quaternion.identity);
						spawn.transform.parent = RightSide;
						MothershipState = MothershipStates.SpawnedRightSide;
						ChooseSideRandomly ();
					}

				}
			}
			if (MothershipState == MothershipStates.SpawnedLeftSide) {

			}
			if (MothershipState == MothershipStates.SpawnedRightSide) {

			}
		}
	}
}
