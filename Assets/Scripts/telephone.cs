using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class telephone : MonoBehaviour
{
    public playerInteractionState playerInteractionStateScript;
    public GameObject phoneInEar;
    public GameObject phoneInTelephone;
    public TMP_Text[] phoneNumberSlots;
    public int slotIndex;
    public int combinationIndex;
    public string enteredCombination;
    public string correctCombination;

    private bool playerCanUseTelephone;

    [Header("TELEPHONE AUDIO")]
    public AudioSource speaker_telephone;
    public AudioSource speaker_phoneInEar;
    public AudioClip sound_button;
    public AudioClip sound_pickup;
    public AudioClip sound_putdown;
    public AudioClip sound_noanswer;
    public AudioClip sound_extendedwarranty;

    private void Start()
    {
        playerInteractionStateScript = GameObject.Find("PLAYER").GetComponent<playerInteractionState>();
        playerCanUseTelephone = true;
        clearSlots();
        combinationIndex = 0;
        enteredCombination = "";
    }

    private void Update()
    {
        phoneButtonPress();
        if (Input.GetKeyDown("x"))
        {
            clearSlots();
        }
        enteredCombination = phoneNumberSlots[0].text + phoneNumberSlots[1].text + phoneNumberSlots[2].text + phoneNumberSlots[3].text + phoneNumberSlots[4].text;
    }

    private void phoneButtonPress()
    {
        if (playerCanUseTelephone)
        {
            RaycastHit hit;
            Camera playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            float interactionDistance = 4f;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
            {
                //PLAYER PRESSES NUMBER BUTTON (dial number)
                if (Input.GetMouseButtonDown(0)
                && hit.collider.gameObject.layer == 6
                && slotIndex < phoneNumberSlots.Length
                && hit.collider.gameObject.GetComponent<telephoneButton>().buttonNumber != "*"
                && hit.collider.gameObject.GetComponent<telephoneButton>().buttonNumber != "#"
                && playerInteractionStateScript.playerIsAllowedToInteract)
                {
                    phoneNumberSlots[slotIndex].text = hit.collider.gameObject.GetComponent<telephoneButton>().buttonNumber;
                    slotIndex++;
                    //speaker_telephone.PlayOneShot(sound_button);
                }
                else
                if (Input.GetMouseButtonDown(0)
                && hit.collider.gameObject.layer == 6
                && playerInteractionStateScript.playerIsAllowedToInteract)
                {
                    //PLAYER PRESSES * (clear all numbers)
                    if (hit.collider.gameObject.GetComponent<telephoneButton>().buttonNumber == "*")
                    {
                        clearSlots();
                    }
                    //PLAYER PRESSES # (call number)
                    else if (hit.collider.gameObject.GetComponent<telephoneButton>().buttonNumber == "#")
                    {
                        StartCoroutine(callNumber());
                        clearSlots();
                    }
                    //speaker_telephone.PlayOneShot(sound_button);
                }
            }
        }
    }

    public IEnumerator callNumber()
    {
        playerCanUseTelephone = false;
        //speaker_telephone.PlayOneShot(sound_pickup);
        phoneInTelephone.SetActive(false);
        phoneInEar.SetActive(true);
        if (enteredCombination == correctCombination)
        {
            yield return new WaitForSeconds(1);
            //speaker_phoneInEar.PlayOneShot(sound_extendedwarranty);
            yield return new WaitForSeconds(4);
            putPhoneDown();
        }
        else
        {
            yield return new WaitForSeconds(1);
            //speaker_phoneInEar.PlayOneShot(sound_noanswer);
            yield return new WaitForSeconds(3);
            putPhoneDown();
        }
    }

    private void putPhoneDown()
    {
        speaker_telephone.PlayOneShot(sound_putdown);
        phoneInEar.SetActive(false);
        phoneInTelephone.SetActive(true);
        speaker_phoneInEar.Stop();
        playerCanUseTelephone = true;
    }

    private void clearSlots()
    {
        foreach (TMP_Text phoneSlot in phoneNumberSlots)
        {
            phoneSlot.text = "";
        }
        slotIndex = 0;
    }
}
