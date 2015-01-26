using UnityEngine;
using System.Collections;

public class RespawnHandler : MonoBehaviour {
	public GameObject MainCamera;
	public GameObject Hero;
	//Respawn
	private float spawnTime;
	private float spawnDelay = 1;
	private bool spawning = false;
	private bool spawnAllowed = true;
	private int spawnTotal = 3;
	private int spawnCount = 0;

	//Checkpoint
	public GameObject CheckPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!spawning) {
			if (MainCamera.GetComponent<CameraScript>().heroDying) {
				spawnTime = Time.fixedTime;
				spawning = true;
			}
		}

		//Respawn
		if (spawning) {
			if ((Time.fixedTime - spawnTime) >= spawnDelay) {
				if (spawnAllowed) {
					Hero.rigidbody.velocity = new Vector3 (0,0,0);
					Hero.transform.position = CheckPoint.transform.position;

					//health = healthMax;
					spawnCount += 1;
					if (spawnCount >= spawnTotal) {
						spawnAllowed = false;
					}
				}
				else {
					//Reset Level

					Application.LoadLevel (Application.loadedLevel);
				}

				//Status Clear
				Hero.GetComponent<Hero>().staminaRecovering = false;
				MainCamera.GetComponent<CameraScript>().heroDying = false;
				spawning = false;
			}
		}
		//Respawned

	}

}
