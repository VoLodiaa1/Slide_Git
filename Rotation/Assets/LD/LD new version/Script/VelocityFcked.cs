using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VelocityFcked : MonoBehaviour {
	Rigidbody rb;
	public bool troprapide = false;
	float timer =0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		if (timer > 1f) {

			if (rb.velocity.y < -13f) {
				troprapide = true;
			}
		}

		print (rb.velocity);
		rb.AddForce (new Vector3 (0,0.0000000000000001f,0));
	}


	void OnCollisionEnter (Collision col) {
		if (troprapide == true) {
			print ("pute");
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
		if (col.transform.tag == "feuille") {
			transform.parent = col.transform;
			rb.drag = 10;
		}
		if (col.transform.name == "Death") {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
		troprapide = false;
	}
	void OnCollisionExit (Collision col){
		if (col.transform.tag == "feuille") {
			transform.parent = null;
			rb.drag = 0;
		}
	}
		
}
