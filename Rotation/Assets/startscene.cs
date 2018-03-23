using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class startscene : MonoBehaviour {
    public string SceneToLoad;
	// Use this for initialization
	void Start () {
        SceneManager.LoadScene(SceneToLoad);
        Destroy(gameObject);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
