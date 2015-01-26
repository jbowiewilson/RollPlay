using UnityEngine;
using System.Collections;

public class SlickTrigger : MonoBehaviour {
	public GameObject Hero;
	private bool onPlate = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (onPlate) {
			Hero.GetComponent<Hero>().canShift = false;
		}
	
	}

	void OnTriggerEnter (Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {
			onPlate = true;
		}
		//Hero Collided
	}

	void OnTriggerExit (Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {
			onPlate = false;
			//collision.gameObject.GetComponent<Hero>().canShift = true;
			Hero.GetComponent<Hero>().canShift = true;
		}
		//Hero Collided
	}
}
