using UnityEngine;
using System.Collections;

public class SpeedBoostTrigger : MonoBehaviour {
	public GameObject Hero;
	public bool boostX, boostZ, negative;
	private float speed = .2f;
	private bool regionActive = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (regionActive) {
			if (boostX) {
				if (!negative) {
					Hero.rigidbody.velocity = new Vector3 ((Hero.rigidbody.velocity.x + speed), Hero.rigidbody.velocity.y, Hero.rigidbody.velocity.z);
				}
				else if (negative) {
					Hero.rigidbody.velocity = new Vector3 ((Hero.rigidbody.velocity.x - speed), Hero.rigidbody.velocity.y, Hero.rigidbody.velocity.z);
					
				}
			}
			else if (boostZ) {
				if (!negative) {
					Hero.rigidbody.velocity = new Vector3 (Hero.rigidbody.velocity.x, Hero.rigidbody.velocity.y, (Hero.rigidbody.velocity.z + speed));
				}
				else if (negative) {
					Hero.rigidbody.velocity = new Vector3 (Hero.rigidbody.velocity.x, Hero.rigidbody.velocity.y, (Hero.rigidbody.velocity.z - speed));
					
				}
			}
		}
	
	}

	void OnTriggerEnter (Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {
			regionActive = true;
		}
		//Hero Collided
	}
	void OnTriggerExit (Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {
			regionActive = false;
		}
		//Hero Collided
	}
}
