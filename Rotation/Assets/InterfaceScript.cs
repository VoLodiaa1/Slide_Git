using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceScript : MonoBehaviour {
    public List<GameObject> ObjectToRollBack;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void RollBackButton()
    {
        foreach (var item in ObjectToRollBack)
        {
            
            item.GetComponent<Rotation.PropertiesObj>().timer = 5000;
        }
    }
    void RollBackButtonExit()
    {
        foreach (var item in ObjectToRollBack)
        {

            item.GetComponent<Rotation.PropertiesObj>().timer = 0;
        }
    }
}
