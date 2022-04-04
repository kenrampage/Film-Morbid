using UnityEngine;
using UnityEngine.UI;
public class crosshair : MonoBehaviour
{
    public playerInteractionState playerInteractionStateScript;
    public float interactionDistance = 4;
    public Color32 crosshairColor;
    public Image crosshairPanel;
    public Camera playerCamera;
    void Start()
    {
        playerInteractionStateScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerInteractionState>();
    }
    void Update()
    {
        crosshairPanel.color = crosshairColor;
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance)
            && playerInteractionStateScript.playerIsAllowedToInteract)
        {
            if (hit.collider.tag == "interactable")
            {
                crosshairColor.a = 255;
            }
            else if(hit.collider.tag== "holdable")
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
