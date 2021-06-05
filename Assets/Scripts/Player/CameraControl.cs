using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform playerCamera;
    private Quaternion mainCoords;
    void Start()
    {
        playerCamera = transform.Find("Main Camera");
        if (playerCamera != null)
            mainCoords = playerCamera.localRotation;
    }

    // Update is called once per frame
    public void SetBackCamera()
    {
        if (playerCamera != null)
        {
            playerCamera.localPosition = new Vector3(playerCamera.localPosition.x,playerCamera.localPosition.y,Math.Abs(playerCamera.localPosition.z));
            playerCamera.localEulerAngles = new Vector3(180, playerCamera.localRotation.y,180);
        }
    }
    public void SetNormalCamera()
    {
        if (playerCamera != null)
        {
            playerCamera.localPosition = new Vector3(playerCamera.localPosition.x, playerCamera.localPosition.y, -Math.Abs(playerCamera.localPosition.z));
            playerCamera.localRotation = mainCoords;
        }
    }
}
