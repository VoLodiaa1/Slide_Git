using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementNv : MonoBehaviour {

	public int NvSuivant;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "Avatar") {
			SceneManager.LoadScene ("niveau " + NvSuivant);
		}
	}
}
