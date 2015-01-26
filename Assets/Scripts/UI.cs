using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {
	public GameObject Hero;
	
	//Restart
	private Rect restartRect;
	private float restartW, restartH, restartX, restartY;

	//End
	private Rect exitRect;
	private float exitW, exitH, exitX, exitY;

	//Cooldown
	private bool Cooldown = true;
	private float cooldownW, cooldownH, cooldownX, cooldownY;
	private Rect cooldownRect;

	// Use this for initialization
	void Start () {
		//Cooldown
		cooldownW = (Screen.width/10);
		cooldownH = cooldownW;
		cooldownX = (Screen.width/2);
		cooldownY = 0;
		cooldownRect = new Rect (cooldownX, cooldownY, cooldownW, cooldownH);

		//End
		exitW = (Screen.width/9);
		exitH = (Screen.height/9);
		exitX = (Screen.width - exitW);
		exitY = 0;
		exitRect = new Rect (exitX, exitY, exitW, exitH);

		//Restart
		restartW = (Screen.width/9);
		restartH = (Screen.height/9);
		restartX = (Screen.width - restartW);
		restartY = (restartH);
		restartRect = new Rect (restartX, restartY, restartW, restartH);
	}
	
	// Update is called once per frame
	void Update () {
		Cooldown = !(Hero.GetComponent<Hero>().staminaRecovering);
	}

	void OnGUI () {
		Cooldown = GUI.Toggle(cooldownRect, Cooldown, "Cooldown");

		if (GUI.Button (exitRect, "Exit")) {
			Application.Quit();
		}
		if (GUI.Button (restartRect, "Restart")) {
			Application.LoadLevel (Application.loadedLevel);
		}

	}
}
