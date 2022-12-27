using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotationReverse : MonoBehaviour
{
    private Vector3 startRotation;

    void Start() { startRotation = transform.rotation.eulerAngles; }
    void LateUpdate()
    {
        Vector3 newRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(
            newRotation.x,
            newRotation.y,
            newRotation.z
        );
    }
    
}
