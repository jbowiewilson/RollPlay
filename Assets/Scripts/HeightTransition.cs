using UnityEngine;
using System.Collections;

public class HeightTransition : MonoBehaviour {
	public GameObject Hero;
	public float targetHeight;
	private float heightCap = 1;
	private float speed = 20;
	//private float transitionTime = 1;
	private bool falling, rising;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (falling) {
			//Hero.transform.position = Vector3.Lerp((Hero.transform.position), new Vector3 (Hero.transform.position.x, targetHeight, Hero.transform.position.z), (Time.fixedDeltaTime * transitionTime));
			Hero.rigidbody.AddForce (0, -speed, 0);

			if (Mathf.Abs(targetHeight - Hero.transform.position.y) <= heightCap) {
				Hero.transform.position = new Vector3 (Hero.transform.position.x, targetHeight, Hero.transform.position.z);
				falling = false;
			}
		}

		if (rising) {
			//Hero.transform.position = Vector3.Lerp((Hero.transform.position), new Vector3 (Hero.transform.position.x, targetHeight, Hero.transform.position.z), (Time.fixedDeltaTime * transitionTime));
			Hero.rigidbody.AddForce (0, speed, 0);
			
			if (Mathf.Abs(targetHeight - Hero.transform.position.y) <= heightCap) {
				Hero.transform.position = new Vector3 (Hero.transform.position.x, targetHeight, Hero.transform.position.z);
				rising = false;
			}
		}
	
	}

	void OnTriggerEnter (Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {
			if (Hero.transform.position.y < targetHeight) {
				rising = true;
			}
			else if (Hero.transform.position.y > targetHeight) {
				falling = true;
			}
			//collision.gameObject.GetComponent<Hero>().canShift = false;
			//Destroy(collision.gameObject.GetComponent<Hero>());
			//MainCamera.GetComponent<CameraScript>().heroDying = true;
			//collision.gameObject.rigidbody.velocity = new Vector3 (0, -20, 0);
		}
		//Hero Collided
	}
}
