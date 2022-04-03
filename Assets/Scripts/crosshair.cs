using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crosshair : MonoBehaviour
{
    public float interactionDistance = 4;
    public Camera playerCamera;

    public Image crosshairPanel;
    public Color32 crosshairColor;

    void Start()
    {
    }

    void Update()
    {
        crosshairPanel.color = crosshairColor;
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.tag == "interactable")
            {
                crosshairColor.a = 255;
            }
            else
            {
                crosshairColor.a = 80;
            }
        }
        else
        {
            crosshairColor.a = 80;
        }
    }
}
