using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionnementLD : MonoBehaviour {

	GameObject PositionAvatar;
	bool contact = false;
	public GameObject[] enfant;
	public int contactAvatar =0;

	// Use this for initialization
	void Start () {
		PositionAvatar = GameObject.Find ("Avatar");
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < enfant.Length; i++) {
			contactAvatar += enfant [i].GetComponent<ContactAvatar> ().value;
		}

		if (contactAvatar == 0 ) {
			StartCoroutine ("Compteur");
		} else {
			contact = true;
			contactAvatar = 0;
		}
	
		if (contact == false) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, PositionAvatar.transform.position.z);
		}
	}






	IEnumerator Compteur() {
		contactAvatar = 0;
		for (float i = 0; i < 3; i++) {
			yield return new WaitForSeconds (0.2f);
		}
		contact = false;
	}

}
