using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteractionState : MonoBehaviour
{
    //This script exists so that the player can't interact with objects while holding/viewing an object.
    //Whenever you add a new script that has an interaction (for example an input.getkeydown("e"), make sure you check if interaction is allowed here.
    //example: if (Input.GetKeyDown("e") && playerInteractionState.playerIsAllowedToInteract) 
    public bool playerIsAllowedToInteract;

    private void Start()
    {
        playerIsAllowedToInteract = true;
    }
}
