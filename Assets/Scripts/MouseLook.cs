using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{ 
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    public float xRotation = 0f;

    public bool playerCanLookAround = true;

    // Start is called before the first frame update
    void Start()
    {
        //mouseSensitivity = 30 + 50f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (playerCanLookAround)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);

            Camera.main.GetComponent<MouseLook>().mouseSensitivity = 100;
        }
    }
}
