using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notDestory : MonoBehaviour
{
    public GameObject xrOrigin;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(xrOrigin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
