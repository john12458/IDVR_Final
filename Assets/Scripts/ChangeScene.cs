using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeScene : MonoBehaviour
{
    Scene scene;
    int maxScene;
    // Start is called before the first frame update
    void Start()
    {
        maxScene = SceneManager.sceneCountInBuildSettings;
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change(){
        int currScene = scene.buildIndex;
        if(currScene+1 < maxScene)
            SceneManager.LoadScene(currScene + 1);
    }
}
