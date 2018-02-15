using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    public Scene Scene1;
    public Scene Scene2;
    public Scene Scene3;
    public Scene Scene4;
    public Scene Scene5;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LVL1()
    {
        SceneManager.LoadScene("scene1");
    }
    public void LVL2()
    {
        SceneManager.LoadScene("scene2");
    }
    public void LVL3()
    {
        SceneManager.LoadScene("scene3");
    }
    public void LVL4()
    {
        SceneManager.LoadScene("scene4");
    }
    public void LVL5()
    {
        SceneManager.LoadScene("scene5");
    }
}
