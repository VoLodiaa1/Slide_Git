using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityGrabber : MonoBehaviour {
    public float VelocityOfObj;
    public Text Txt2Debug;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        VelocityOfObj = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        Txt2Debug.text = "velocity is " + VelocityOfObj;
	}
}
