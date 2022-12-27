using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SceneLoader());
    }

    // Update is called once per frame
    IEnumerator SceneLoader()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("360Video");
    }
}
