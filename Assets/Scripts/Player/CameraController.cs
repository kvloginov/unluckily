using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform body;
      
    public float sensitivity;

    // Update is called once per frame
    void Update()
    {
		body.rotation = Quaternion.Euler(0, body.localRotation.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivity, 0);

		if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


}
