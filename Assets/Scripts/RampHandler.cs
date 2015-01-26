using UnityEngine;
using System.Collections;

public class RampHandler : MonoBehaviour {
	private Vector3 gravity;
	public float heroSpeed;

	// Use this for initialization
	void Start () {
		gravity = Physics.gravity;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {
			float heroSpeedX = collision.gameObject.rigidbody.velocity.x;
			float heroSpeedY = collision.gameObject.rigidbody.velocity.z;
			heroSpeed = Mathf.Sqrt (((heroSpeedX*heroSpeedX)+(heroSpeedY*heroSpeedY)));
			Physics.gravity = new Vector3 (0,0,0);
		}
		//Hero Collided
	}
	void OnTriggerExit (Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {
			Physics.gravity = gravity;
		}
		//Hero Collided
	}
}
