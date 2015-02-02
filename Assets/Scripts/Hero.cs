using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hero : MonoBehaviour {
	public GameObject MainCamera;
	public GameObject Self;
	public GameObject HeroRender;
	public GameObject HeroRenderRunning, HeroRenderStanding;
	public GameObject HeroSelectionDull, HeroSelectionFull;
	public GameObject HeroArrow;
	public GameObject ArrowFull, ArrowDull, ArrowEdge;
	public GameObject HeroArrowDirector;
	public GameObject HeroRenderDirector;
	public GameObject ExclamationMarkRender;

	//Hero State Change and Selection
	private int heroState = 0;
	private bool heroSelected = false;
	private bool heroStateChanged = false;
	public float heroScore = 0;

	//Hero Movement
	public bool canShift = true;
	private bool shiftStarted = false;
	private bool withinStableRange = false;
	private float currentDistance, stableDistance;
	private Vector2 startSpot, endSpot, shiftSpeed;
	private int screenFraction = 3;
	private float speed = 14;


	//Movement Cooldown
	private float sRecoveryRate = 1;
	private float sRecoveryTime;
	public bool staminaRecovering = false;

	//Keys
	public bool keyRed = false, keyBlue = false;

	// Use this for initialization
	void Start () {
		//Arrow Blank Start
		HeroArrow.SetActive(false);
		ArrowFull.SetActive(false);
		ArrowDull.SetActive(false);
		HeroSelectionFull.SetActive(false);
		HeroSelectionDull.SetActive(false);

		//Movement Baselines
		Vector3 translateSpot = MainCamera.camera.WorldToScreenPoint(Self.transform.position);
		startSpot = new Vector2 (translateSpot.x, translateSpot.y);
		float distance = (startSpot.y / screenFraction);
		stableDistance = distance;

		//Gravity Modification
		Physics.gravity = new Vector3 (0,-60, 0);
	}
	
	// Update is called once per frame
	void Update () {
		//Hero State Change
		if (heroStateChanged) {
			heroStateChange ();
		}
		//Hero State Changed

		//Hero Render Rotate
		HeroRenderDirector.transform.position = new Vector3((Self.transform.position.x + Self.rigidbody.velocity.x), Self.transform.position.y, (Self.transform.position.z + Self.rigidbody.velocity.z));
		HeroRender.transform.LookAt(HeroRenderDirector.transform.position);
		//Hero Render Rotated

		//Stable Range Check + Position Update
		Vector3 translateSpot = MainCamera.camera.WorldToScreenPoint(HeroRender.transform.position);
		startSpot = new Vector2 (translateSpot.x, translateSpot.y);
		currentDistance = Vector2.Distance (startSpot, Input.mousePosition);
		
		if (currentDistance < stableDistance) {
			withinStableRange = true;
			//print ("Hero Selected");
			heroSelected = true;
		}
		else {
			withinStableRange = false;
			//print ("Hero Unselected");
			heroSelected = false;
		}
		//Stable Range Checked + Position Updated

		//Hero Selection

		if (heroSelected) {
			HeroSelectionFull.transform.LookAt(MainCamera.transform.position);
			HeroSelectionDull.transform.LookAt(MainCamera.transform.position);

			if (canShift) {
				HeroSelectionFull.SetActive(true);
				HeroSelectionDull.SetActive(false);
			}
			if (!canShift) {
				HeroSelectionDull.SetActive(true);
				HeroSelectionFull.SetActive(false);
			}
		}
		else if (!heroSelected) {
			HeroSelectionDull.SetActive(false);
			HeroSelectionFull.SetActive(false);
		}
		//Hero Selected

		//Launch
		if (Input.GetMouseButtonDown(0) && !shiftStarted && withinStableRange) {
			shiftStarted = true;
		}

		if (shiftStarted) {
			HeroArrow.SetActive(true);
			PredictLaunch();
		}

		if (canShift) {
			if (Input.GetMouseButtonUp(0) && shiftStarted) {
				if (withinStableRange) {
					Self.rigidbody.velocity = new Vector3 (0,0,0);
					shiftStarted = false;
				}
				else {
					endSpot = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					ShiftVelocity();
					shiftStarted = false;

					//Stamina Recovery Adjustment
					staminaRecovering = true;
					sRecoveryTime = Time.fixedTime;
				}
				HeroArrow.SetActive(false);
			}
		}
		if (Input.GetMouseButtonUp(0) && shiftStarted) {
			HeroArrow.SetActive(false);
			shiftStarted = false;
		}
		//Launched

		//Handle Cooldown
		if (staminaRecovering) {
			canShift = false;
			if ((Time.fixedTime - sRecoveryTime) >= sRecoveryRate) {
				staminaRecovering = false;
			}
		}
		else {
			canShift = true;
		}
		//Cooldown Handled
	
	}

	void heroStateChange () {
		switch (heroState) {
			//Set Hero Render to Standing
		case 0:
			HeroRenderRunning.gameObject.SetActive(false);
			HeroRenderStanding.gameObject.SetActive(true);
			return;
			//Set Hero Render to Running
		case 1:
			HeroRenderStanding.gameObject.SetActive(false);
			HeroRenderRunning.gameObject.SetActive(true);
			return;
		}
	}

	void PredictLaunch () {
		endSpot = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		float shiftX;
		float shiftY;

		HeroArrow.transform.localRotation.eulerAngles.Set (0,0,0);



		//ANGLE
		shiftX = -(startSpot.x - endSpot.x);
		shiftY = -(startSpot.y - endSpot.y);
		float angle = Mathf.Atan2(shiftY,shiftX);

		//SCALE
		float distance = Vector2.Distance (startSpot, endSpot);
		distance = (distance - stableDistance);
		if (distance > stableDistance) {
			distance = stableDistance;
		}		
		float modifiedDistance = distance / stableDistance;
		if (modifiedDistance >= 0) {
			ArrowFull.transform.localScale = new Vector3 ((.05f + (.15f * modifiedDistance)),(1),(.1f + (.3f * modifiedDistance)));
			ArrowFull.transform.localPosition = new Vector3 (0,0,(-10 * (ArrowFull.transform.localScale.z / 2)));
		
			ArrowDull.transform.localScale = ArrowFull.transform.localScale;
			ArrowDull.transform.localPosition = ArrowFull.transform.localPosition;
		
			ArrowEdge.transform.localScale = ArrowFull.transform.localScale;
			ArrowEdge.transform.localPosition = ArrowFull.transform.localPosition;
		
			//Predict
			shiftX = Mathf.Cos(angle) * 10;
			shiftY = Mathf.Sin(angle) * 10;

			HeroArrowDirector.transform.position = new Vector3((Self.transform.position.x + shiftX), Self.transform.position.y, (Self.transform.position.z + shiftY));
			HeroArrow.transform.LookAt(HeroArrowDirector.transform.position);
	
				
			//Render
			if (canShift) {
				ArrowDull.SetActive(false);
				ArrowEdge.SetActive(true);
				ArrowFull.SetActive(true);
			}
			else if (!canShift) {
				ArrowFull.SetActive(false);
				ArrowEdge.SetActive(true);
				ArrowDull.SetActive(true);
			}
		}
	}

	void ShiftVelocity () {
		float shiftX;
		float shiftY;

		//ANGLE
		
		shiftX = -(startSpot.x - endSpot.x);
		shiftY = -(startSpot.y - endSpot.y);
		float angle = Mathf.Atan2(shiftY,shiftX);
		
		
		//DISTANCE
		float distance = Vector2.Distance (startSpot, endSpot);
		distance = (distance - stableDistance);
		if (distance > stableDistance) {
			distance = stableDistance;
		}
		
		float modifiedDistance = distance / stableDistance;

		//LAUNCH
		shiftX = Mathf.Cos(angle) * modifiedDistance * speed;
		shiftY = Mathf.Sin(angle) * modifiedDistance * speed;
		Self.rigidbody.velocity = new Vector3 (-shiftX, 0, -shiftY);
		//print ("Distance: " + distance + " Angle: " + angle);
	}

	void OnTriggerEnter (Collider collision) {
		//Key Colliding
		if (collision.gameObject.tag == "KeyBlue") {
			keyBlue = true;
			heroScore += 50;
		}
		else if (collision.gameObject.tag == "KeyRed") {
			keyRed = true;
			heroScore += 50;
		}
		//Key Collided

		else {
			heroScore += 5;
		}

	}

	void OnCollisionEnter (Collision collision) {
		//MainCamera.GetComponent<CameraScript>().MicroShake();
		//Increase Score
		if (collision.gameObject.tag == "Wall") {
			MainCamera.GetComponent<CameraScript>().MicroShake();
			heroScore += 5;
		}
		else if (collision.gameObject.tag == "Pin") {
			MainCamera.GetComponent<CameraScript>().LargeShake();
			heroScore += 25;
		}
		else if (collision.gameObject.tag == "Treasure") {
			heroScore += 100;
		}
		else {
			//heroScore += 5;
		}
	}
}