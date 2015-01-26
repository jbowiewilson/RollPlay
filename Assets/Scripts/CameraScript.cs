using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public GameObject MainCameraObject;
	public GameObject Hero;
	public bool heroDying;


	//public float timeBlur = (.5f);
	private float xMod, yMod, zMod;
	private float xMod0 = 0, yMod0 = 8, zMod0 = -5;
	private float xShiftMax = 3, zShiftMax = 3;

	//Lerp Blend
	private float shiftSmooth = 5;

	//Shake Variables
	public bool shaking = false;
	public float				shake_intensity;
	public float				shake_decay;
	private float				smallShakeIntensity = .75f; // amount of shake
	private float				smallShakeDuration = 0.2f; // how long shake lasts
	private float				largeShakeIntensity = 1.5f;
	private float				largeShakeDuration = 0.3f;
	

	// Use this for initialization
	void Start () {
		CenterCamera();
	
	}
	
	// Update is called once per frame
	void Update () {

		//Mark Camera Goal
		xMod = Mathf.Lerp (xMod, (xMod0 + ((-Hero.rigidbody.velocity.x / 14) * xShiftMax)), Time.fixedDeltaTime * shiftSmooth);
		zMod = Mathf.Lerp (zMod, (zMod0 + ((-Hero.rigidbody.velocity.z / 14) * zShiftMax)), Time.fixedDeltaTime * shiftSmooth);
		//Camera Goal Marked
		//Shake
		if (shaking) {
			xMod += Random.insideUnitSphere.x * shake_intensity;
			zMod += (Random.insideUnitSphere.y/2) * shake_intensity;
			shake_intensity -= shake_decay*Time.fixedDeltaTime;
		}
		if (shake_intensity <= 0 && shaking){
			shaking = false;
		}
		//Shaken

		//MainCameraObject.transform.position = Vector3.Lerp((MainCameraObject.transform.position), new Vector3 (Hero.transform.position.x + xMod, Hero.transform.position.y + yMod, Hero.transform.position.z + zMod), (Time.fixedDeltaTime * timeBlur));
		/*
		if (Mathf.Abs(Hero.rigidbody.velocity.x) > 0 || Mathf.Abs(Hero.rigidbody.velocity.z) > 0) {

			xMod = Mathf.Lerp (xMod, (xMod0 + ((-Hero.rigidbody.velocity.x / 14) * xShiftMax)), Time.fixedDeltaTime * shiftSmooth);
			zMod = Mathf.Lerp (zMod, (zMod0 + ((-Hero.rigidbody.velocity.z / 14) * zShiftMax)), Time.fixedDeltaTime * shiftSmooth);

		}
		else {
			CenterCamera();
		}
		*/

		if (!heroDying) {
			MainCameraObject.transform.position = new Vector3 (Hero.transform.position.x + xMod, Hero.transform.position.y + yMod, Hero.transform.position.z + zMod);
			MainCameraObject.transform.LookAt (Hero.transform);
		}
	}

	void CenterCamera () {
		xMod = xMod0;
		yMod = yMod0;
		zMod = zMod0;
	}
	//micro shake
	public void MicroShake(){
		shake_intensity = smallShakeIntensity/2;
		shake_decay = smallShakeIntensity/(2*smallShakeDuration);
		shaking = true;
	}
	
	// small shake
	public void SmallShake(){
		shake_intensity = smallShakeIntensity;
		shake_decay = smallShakeIntensity/smallShakeDuration;
		shaking = true;
	}
	
	// large shake
	public void LargeShake(){
		shake_intensity = largeShakeIntensity;
		shake_decay = largeShakeIntensity/largeShakeDuration;
		shaking = true;
	}
}
