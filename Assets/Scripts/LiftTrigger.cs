using UnityEngine;
using System.Collections;

public class LiftTrigger : MonoBehaviour {
	public GameObject LiftExit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {
			//Teleport
			collision.gameObject.transform.position = LiftExit.transform.position;
		}
		//Hero Collided
	}
}
