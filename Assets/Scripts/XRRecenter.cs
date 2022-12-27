using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRRecenter : MonoBehaviour
{
    [SerializeField] Transform resetTransform;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerCamera;
    // Start is called before the first frame update

    public float rotationYOffset;
    bool first = true;

    void Start()
    {
        // float rotation = resetTransform.rotation.eulerAngles.y - playerCamera.transform.rotation.eulerAngles.y;
        // player.transform.Rotate(0, rotationYOffset, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Debug.Log("playerCamera.transform.rotation.eulerAngles.y"+ playerCamera.transform.rotation.eulerAngles.ToString("F4"));
        if (playerCamera.transform.rotation.eulerAngles.y != 0 && first) {
            rotationYOffset = resetTransform.rotation.eulerAngles.y - playerCamera.transform.rotation.eulerAngles.y;
            first = false;
            player.transform.Rotate(0, rotationYOffset, 0);
        }
    }

    [ContextMenu("Reset Rotation")]
    public void resetRotation()
    {
        var rotationAngleY = resetTransform.rotation.eulerAngles.y - playerCamera.transform.rotation.eulerAngles.y;
        playerCamera.transform.Rotate(0, rotationAngleY, 0);
    }

    void addRotation()
    {
        player.transform.Rotate(0, rotationYOffset, 0);
    }
}
