using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour {
	public GameObject Self;
	public int health;
	public bool Key, Red, Blue;
	public bool keyBlue = false, keyRed = false;

	// Use this for initialization
	void Start () {
		if (Key) {
			health = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision collision) {
		//Hero Colliding

		if (collision.gameObject.tag == "Hero") {
			if (Key) {
				//Update Key State
				keyRed = collision.gameObject.GetComponent<Hero>().keyRed;
				keyBlue = collision.gameObject.GetComponent<Hero>().keyBlue;

				if (Red) {
					if (keyRed) {
						DamageCollision();
					}
				}

				if (Blue) {
					if (keyBlue) {
						DamageCollision();
					}
				}
			}
			else {
				DamageCollision();
			}
		}
		//Hero Collided
	}

	void OnTriggerEnter (Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {
			Destroy (Self);
		}
		//Hero Collided
	}

	void DamageCollision () {
		//health -= collision.gameObject.GetComponent<Hero>().damageEnvironment;
		health -= 1;
		
		//Destruction Check
		if (health <= 0) {
			//Self.renderer.enabled = false;
			//Self.collider.enabled = false;
			Destroy (Self);
			//Destroy (Self.GetComponent<Destructible>());
		}
		//Destruction Checked
	}
}
