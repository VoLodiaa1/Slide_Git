﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAvatar : MonoBehaviour {

	public int value =0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "Avatar") {
			value = 1;
		}
	}

	void OnCollisionExit (Collision col) {
		if (col.gameObject.name == "Avatar") {
			value = 0;
		}
	}
}
