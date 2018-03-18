using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneStreamer : MonoBehaviour {
    public List<string> SceneToLoad;
    public List<string> AllTheScenes;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public void UnloadLoad()
    {
        int IndScenes = SceneManager.sceneCount;

        for (int i = IndScenes-1; i >=0 ; i--)
        {
            AllTheScenes.Add(SceneManager.GetSceneAt(i).ToString());
        }
        IEnumerable ListSceneToUnload = AllTheScenes.Intersect(SceneToLoad);
        foreach (string item2 in ListSceneToUnload)
        {
            StartCoroutine(UnloadAllTheScenes(item2));
        }
        foreach (string item in SceneToLoad)
        {
            StartCoroutine(LoadYourAsyncScene(item));
        }

        
    }
    IEnumerator LoadYourAsyncScene(string scen)
    {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scen, LoadSceneMode.Additive);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator UnloadAllTheScenes(string scen)
    {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(scen);
        
        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
