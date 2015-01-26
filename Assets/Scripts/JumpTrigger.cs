using UnityEngine;
using System.Collections;

public class JumpTrigger : MonoBehaviour {
	public GameObject Hero;
	public GameObject Ramp;

	public float jumpSpeed;
	//public float jumpSpeedFlat;
	public Vector2 initialVelocity;

	//public int xMod, zMod;

	// Use this for initialization
	void Start () {
		Hero = GameObject.FindGameObjectWithTag("Hero");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {
			//Hero.rigidbody.velocity = new Vector3 ((initialVelocity.x * .8f), (Hero.rigidbody.velocity.y + jumpSpeed), (initialVelocity.y * .8f));

			float angle = Mathf.Atan2(Hero.rigidbody.velocity.x,Hero.rigidbody.velocity.z);
			//float xMod = Mathf.Sin(angle) * jumpSpeedFlat;
			//float zMod = Mathf.Cos(angle) * jumpSpeedFlat;
			float xMod = Mathf.Sin(angle) * Ramp.GetComponent<RampHandler>().heroSpeed;
			float zMod = Mathf.Cos(angle) * Ramp.GetComponent<RampHandler>().heroSpeed;

			//print ("X: " + xMod);
			//print ("Z: " + zMod);

			//Hero.rigidbody.velocity = new Vector3 (Hero.rigidbody.velocity.x + xMod, (Hero.rigidbody.velocity.y + jumpSpeed), Hero.rigidbody.velocity.z + zMod);
			Hero.rigidbody.velocity = new Vector3 (xMod, (Hero.rigidbody.velocity.y + jumpSpeed), zMod);
		}
		//Hero Collided
	}
}
