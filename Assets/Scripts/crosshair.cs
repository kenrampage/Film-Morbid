using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crosshair : MonoBehaviour
{
    public interactionType interactionTypeScript;
    public playerInteractionState playerInteractionStateScript;
    public float interactionDistance = 4;
    public Camera playerCamera;

    public GameObject crosshairParent;

    public Image crosshairPanel;
    public GameObject crosshairPanel_horizontal;
    public GameObject crosshairPanel_vertical;
    public Color32 crosshairColor;

    void Start()
    {
        playerInteractionStateScript = GameObject.Find("PLAYER").GetComponent<playerInteractionState>();
    }

    void Update()
    {
        crosshairPanel.color = crosshairColor;
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance)
            && playerInteractionStateScript.playerIsAllowedToInteract)
        {
            interactionTypeScript = hit.collider.gameObject.GetComponent<interactionType>();
            if (interactionTypeScript != null)
            {
                crosshairPanel_horizontal.SetActive(interactionTypeScript.cycle);
                crosshairPanel_vertical.SetActive(interactionTypeScript.up_down);
            }
            else
            {
                crosshairPanel_horizontal.SetActive(false);
                crosshairPanel_vertical.SetActive(false);
            }
            //CROSSHAIR OPACITY
            if (hit.collider.tag == "interactable")
            {
                crosshairColor.a = 255;
            }
            else if (hit.collider.tag == "holdable")
            {
                crosshairColor.a = 255;
            }
            else
            {
                crosshairColor.a = 80;
                crosshairPanel_horizontal.SetActive(false);
                crosshairPanel_vertical.SetActive(false);

            }
        }
        else
        {
            crosshairColor.a = 80;
            crosshairPanel_horizontal.SetActive(false);
            crosshairPanel_vertical.SetActive(false);
        }
    }
}
