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
                if (!GetComponent<ObjViewer>().isViewing)
                {
                    if (Input.GetKeyDown("e"))
                    {
                        GetComponent<ObjViewer>().ViewObject(hit.collider.gameObject);
                        hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }

            }
            if(hit.collider.tag == "clock")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<Clock>().AddTime(-5);
                }
                if (Input.GetMouseButtonDown(1))
                {
                    hit.collider.gameObject.GetComponent<Clock>().AddTime(5);
                }
            }
            if(hit.collider.gameObject.name == "ClockButton")
            {
                if (Input.GetKeyDown("e"))
                {
                    hit.collider.gameObject.transform.parent.GetComponent<Clock>().CheckTime();
                }
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
