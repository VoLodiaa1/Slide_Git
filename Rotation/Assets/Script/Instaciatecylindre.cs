using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instaciatecylindre : MonoBehaviour {

	public GameObject cylindre;
	public GameObject cube;
	public GameObject sphere;
	public GameObject parentg;
	GameObject[] yolo = new GameObject[3];
	// Use this for initialization
	void Start () {
		
		yolo [0] = cylindre;
		yolo [1] = cube;
		yolo [2] = sphere;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col) {

		if (col.tag == "Objet") {
			GameObject nouveau = Instantiate (yolo[Random.Range (0,2)]);
			nouveau.transform.position = parentg.transform.position;

		}
	}
}
