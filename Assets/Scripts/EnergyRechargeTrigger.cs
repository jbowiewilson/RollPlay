﻿using UnityEngine;
using System.Collections;

public class EnergyRechargeTrigger : MonoBehaviour {
	public GameObject Self;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {

		}
		//Hero Collided
	}
	/*
	void OnTriggerExit(Collider collision) {
		//Hero Colliding
		if (collision.gameObject.tag == "Hero") {
			collision.gameObject.GetComponent<Hero>().ExclamationMarkRender.SetActive(false);
		}
		//Hero Collided
	}
	*/
}
