using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangementCamera : MonoBehaviour {

	public bool seizeneuvieme;
	public GameObject MainCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void TelRotation () {

		if (seizeneuvieme == true) {
			MainCamera.GetComponent<Camera> ().orthographicSize = 4.5f;
			seizeneuvieme = false;
			Camera.main.ResetAspect ();
			//Camera.main.aspect = 9 / 16;
			Debug.Log ("changement");
		} else if (seizeneuvieme == false) {
			MainCamera.GetComponent	<Camera> ().orthographicSize = 8f;
			Camera.main.ResetAspect ();
			//Camera.main.aspect = 16 / 9;
			seizeneuvieme = true;
		}
		

	}
}
