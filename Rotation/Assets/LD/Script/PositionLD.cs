using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLD : MonoBehaviour {

	public GameObject Avatar;
	bool touchelecube = false;


	// Use this for initialization
	void Start () {

		Avatar = GameObject.Find ("Avatar");
		
	}
	
	// Update is called once per frame
	void Update () {

		if (touchelecube == false) {
			SamePlan ();
		}
		
	}

	void OnCollisionEnter  (Collision col) {
		if (col.gameObject.name == "Avatar") {
			touchelecube = true;
		}
	}
	void OnCollisionExit (Collision col) {
		if (col.gameObject.gameObject.gameObject.name == "Avatar") {
			touchelecube = false;
		}
	}



	void SamePlan () {
		transform.position = new Vector3 (transform.transform.position.x, transform.position.y, Avatar.transform.position.z);
	}
}
