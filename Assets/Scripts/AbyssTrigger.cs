using UnityEngine;
using System.Collections;

public class AbyssTrigger : MonoBehaviour {
	public GameObject MainCamera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {
			//collision.gameObject.GetComponent<Hero>().canShift = false;
			//Destroy(collision.gameObject.GetComponent<Hero>());
			MainCamera.GetComponent<CameraScript>().heroDying = true;
			collision.gameObject.rigidbody.velocity = new Vector3 (0, -10, 0);
		}
		//Hero Collided
	}
}
