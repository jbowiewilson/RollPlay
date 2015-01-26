  using UnityEngine;
using System.Collections;

public class GateHandler : MonoBehaviour {
	public GameObject GateEntrance1, GateEntrance2;
	public GameObject GateExit1, GateExit2;
	public GameObject Lift;

	private Vector3 gateExit1Closed, gateExit2Closed, gateExit1Open, gateExit2Open;
	private Vector3 gateEntrance1Closed, gateEntrance2Closed, gateEntrance1Open, gateEntrance2Open;

	private float liftSpeed = 2;
	private float doorSpeed;
	private Vector3 liftStart, liftEnd;
	private float stallTime, stallDelay = 2;
	public float liftVertical = 2.5f;
	private bool liftStarted = false, liftReseting = false,liftFinishing;
	public bool orientationX, orientationZ;
	// Use this for initialization
	void Start () {
		//Lift
		//Entrance.SetActive(false);
		liftStarted = false;
		liftReseting = false;
		liftFinishing = false;

		liftStart = Lift.transform.position;

		liftEnd = new Vector3(Lift.transform.position.x, Lift.transform.position.y + liftVertical, Lift.transform.position.z);

		stallTime = Time.fixedTime;	
		Lift.rigidbody.isKinematic = true;

		//Entrance
		gateEntrance1Closed = GateEntrance1.transform.position;
		gateEntrance2Closed = GateEntrance2.transform.position;

		//X?
		if (orientationX) {
			gateEntrance1Open = new Vector3 (GateEntrance1.transform.position.x - 2, GateEntrance1.transform.position.y, GateEntrance1.transform.position.z);
			gateEntrance2Open = new Vector3 (GateEntrance2.transform.position.x + 2, GateEntrance2.transform.position.y, GateEntrance2.transform.position.z);
		}
		//Z?
		if (orientationZ) {
			gateEntrance1Open = new Vector3 (GateEntrance1.transform.position.x, GateEntrance1.transform.position.y, GateEntrance1.transform.position.z + 2);
			gateEntrance2Open = new Vector3 (GateEntrance2.transform.position.x, GateEntrance2.transform.position.y, GateEntrance2.transform.position.z - 2);
		}

		//Exit
		gateExit1Closed = new Vector3(GateExit1.transform.position.x, GateExit1.transform.position.y, GateExit1.transform.position.z);
		gateExit2Closed = new Vector3(GateExit2.transform.position.x, GateExit2.transform.position.y, GateExit2.transform.position.z);

		//X?
		if (orientationX) {
			gateExit1Open = new Vector3 (GateExit1.transform.position.x - 2, GateExit1.transform.position.y, GateExit1.transform.position.z);
			gateExit2Open = new Vector3 (GateExit2.transform.position.x + 2, GateExit2.transform.position.y, GateExit2.transform.position.z);
		}
		//Z?
		if (orientationZ) {
			gateExit1Open = new Vector3 (GateExit1.transform.position.x, GateExit1.transform.position.y, GateExit1.transform.position.z  + 2);
			gateExit2Open = new Vector3 (GateExit2.transform.position.x, GateExit2.transform.position.y, GateExit2.transform.position.z  - 2);
		}

		GateEntrance1.transform.position = gateEntrance1Open;
		GateEntrance2.transform.position = gateEntrance2Open;
	}
	
	// Update is called once per frame
	void Update () {
		if (liftStarted) {

			Lift.rigidbody.velocity = new Vector3 (0, liftSpeed, 0);

			if (Lift.transform.position.y >= liftEnd.y){
				//Lift at Top

				Lift.rigidbody.velocity = new Vector3 (0, 0, 0);
				Lift.rigidbody.isKinematic = true;
				Lift.transform.position = liftEnd;
				stallTime = Time.fixedTime;
				liftStarted = false;
				liftReseting = true;

				GateExit1.transform.position = new Vector3 (gateExit1Open.x, gateExit1Open.y + liftVertical, gateExit1Open.z);
				GateExit2.transform.position = new Vector3 (gateExit2Open.x, gateExit2Open.y + liftVertical, gateExit2Open.z);
				


				//gateExit1Closed = new Vector3 (GateExit1.transform.position.x, GateExit1.transform.position.y, GateExit1.transform.position.z);

			}
		}

		else if (liftReseting) {
			if ((Time.fixedTime - stallTime) >= stallDelay) {
				liftReseting = false;
				liftFinishing = true;
				if (orientationX) {
					GateExit1.rigidbody.velocity = new Vector3 (doorSpeed, 0, 0);
					GateExit2.rigidbody.velocity = new Vector3 (-doorSpeed, 0, 0);
				}

				else if (orientationZ) {
					GateExit1.rigidbody.velocity = new Vector3 (0, 0, doorSpeed);
					GateExit2.rigidbody.velocity = new Vector3 (0, 0, -doorSpeed);
				}

				if (GateExit1.transform.position == new Vector3 (gateExit1Closed.x, gateExit1Closed.y + liftVertical, gateExit1Closed.z) && GateExit2.transform.position == new Vector3 (gateExit2Closed.x, gateExit2Closed.y + liftVertical, gateExit2Closed.z)) {
					GateExit1.rigidbody.velocity = new Vector3 (0, 0, 0);
					GateExit2.rigidbody.velocity = new Vector3 (0, 0, 0);

					//Begin Decent

					Lift.rigidbody.isKinematic = false;
					Lift.rigidbody.velocity = new Vector3 (0, -liftSpeed, 0);
				}



			}
		}

		 else if (liftFinishing) {
			if (Lift.transform.position.y <= liftStart.y){

				Lift.rigidbody.velocity = new Vector3 (0, 0, 0);
				Lift.rigidbody.isKinematic = true;

				Lift.transform.position = liftStart;
				stallTime = Time.fixedTime;
				liftFinishing = false;

				GateEntrance1.transform.position = gateEntrance1Open;
				GateEntrance2.transform.position = gateEntrance2Open;
			}
		}
	}

	void OnTriggerEnter(Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {
			if (!liftReseting && !liftFinishing) {
				//collision.gameObject.GetComponent<Hero>().ExclamationMarkRender.SetActive(false);
				if ((Time.fixedTime - stallTime) >= stallDelay) {
					StartLift ();
				}
			}
		}
		//Hero Collided
	}

	void StartLift () {
		//print ("Lifting");

		GateEntrance1.transform.position = gateEntrance1Closed;
		GateEntrance2.transform.position = gateEntrance2Closed;

		Lift.rigidbody.isKinematic = false;
		//Lift.rigidbody.velocity = new Vector3 (0, 2, 0);
		liftStarted = true;

	}
}
