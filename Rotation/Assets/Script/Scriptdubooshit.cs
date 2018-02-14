using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scriptdubooshit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionEnter (Collision col) {
		this.transform.parent = col.transform;
	}

	void OnCollisionExit (Collision col) {
		this.transform.parent = null;
	}
}
