using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class TelephoneSFXEvents
{
    public UnityEvent onDial0;
    public UnityEvent onDial1;
    public UnityEvent onDial2;
    public UnityEvent onDial3;
    public UnityEvent onDial4;
    public UnityEvent onDial5;
    public UnityEvent onDial6;
    public UnityEvent onDial7;
    public UnityEvent onDial8;
    public UnityEvent onDial9;
    public UnityEvent onDialStar;
    public UnityEvent onDialPound;
    public UnityEvent onReceiverUp;
    public UnityEvent onReceiverDown;
    public UnityEvent onDialingStarted;
    public UnityEvent onCorrectNumberDialed;
    public UnityEvent onInCorrectNumberDialed;
}

public class telephone : MonoBehaviour
{
    public telephoneLight[] telephonelights;
    public playerInteractionState playerInteractionStateScript;
    public GameObject phoneInEar;
    public GameObject phoneInTelephone;
    public TMP_Text[] phoneNumberSlots;
    public int slotIndex;
    public int combinationIndex;
    public string enteredCombination;
    public string correctCombination;

    private bool playerCanUseTelephone;
    public bool playerIsInCall;
    public bool failsafe;
    public bool fs;

    public int numberOfTimesCalled;

    // [Header("TELEPHONE AUDIO")]
    // public AudioClip sound_button;
    // public AudioClip sound_pickup;
    // public AudioClip sound_putdown;
    // public AudioClip sound_noanswer;
    // public AudioClip sound_extendedwarranty;

    // [Header("AUDIO EVENTS")]

    [SerializeField] private TelephoneSFXEvents telephoneSFXEvents;

    private void Start()
    {
        numberOfTimesCalled = 0;
        failsafe = false;
        playerIsInCall = false;
        playerInteractionStateScript = GameObject.Find("PLAYER").GetComponent<playerInteractionState>();
        playerCanUseTelephone = true;
        clearSlots();
        combinationIndex = 0;
        enteredCombination = "";
    }

    private void Update()
    {
        print($"entered: {enteredCombination}");
        checkPlayerDistance();
        phoneButtonPress();
        telephoneLights();
        callAutomatically();
        enteredCombination = phoneNumberSlots[0].text + phoneNumberSlots[1].text + phoneNumberSlots[2].text + phoneNumberSlots[3].text + phoneNumberSlots[4].text;
    }

    private void callAutomatically()
    {
        if (slotIndex == 5)
        {
            if (fs == false)
            {
                StartCoroutine(callNumber());
                fs = true;
            }
        }
    }
    private void telephoneLights()
    {
        for (int i = 0; i < 5; i++)
        {
            if (phoneNumberSlots[i].text == "")
            {
                telephonelights[i].lit = false;
            }
            else
            {
                telephonelights[i].lit = true;
            }
        }
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
                if (phoneInEar.activeSelf)
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
                        CheckButtonPressed(hit.collider.gameObject.GetComponent<telephoneButton>().buttonNumber);
                        telephoneSFXEvents.onDialingStarted?.Invoke();

                        slotIndex++;
                        //speaker_telephone.PlayOneShot(sound_button);
                    }
                    else
                    if (Input.GetMouseButtonDown(0)
                    && hit.collider.gameObject.layer == 6
                    && playerInteractionStateScript.playerIsAllowedToInteract)
                    {

                        CheckButtonPressed(hit.collider.gameObject.GetComponent<telephoneButton>().buttonNumber);

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
                else if (!phoneInEar.activeSelf)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.collider.gameObject.name == "phone")
                        {
                            //r�alisme
                            //reaalne
                            phoneInEar.SetActive(true);
                            phoneInTelephone.SetActive(false);
                            if (slotIndex == 0)
                            {
                                telephoneSFXEvents.onReceiverUp?.Invoke();
                            }
                        }
                    }
                }
            }
        }
    }

    public IEnumerator callNumber()
    {
        playerIsInCall = true;
        playerCanUseTelephone = false;
        //speaker_telephone.PlayOneShot(sound_pickup);
        yield return new WaitForSeconds(.5f);
        clearSlots();
        if (enteredCombination == correctCombination)
        {
            print("YES ");
            yield return new WaitForSeconds(1);
            //speaker_phoneInEar.PlayOneShot(sound_extendedwarranty);
            telephoneSFXEvents.onCorrectNumberDialed?.Invoke();
            if (numberOfTimesCalled == 0)
            {
                yield return new WaitForSeconds(24);
            }
            else
            {
                yield return new WaitForSeconds(17);
            }
            putPhoneDown();
            numberOfTimesCalled++;
        }
        else
        {
            print("YES WRONG OCMB");
            yield return new WaitForSeconds(1);
            //speaker_phoneInEar.PlayOneShot(sound_noanswer);
            telephoneSFXEvents.onInCorrectNumberDialed?.Invoke();
            yield return new WaitForSeconds(3);
            putPhoneDown();
        }
    }

    private void putPhoneDown()
    {
        print("YEYE");
        //speaker_telephone.PlayOneShot(sound_putdown);
        telephoneSFXEvents.onReceiverDown?.Invoke();
        phoneInEar.SetActive(false);
        phoneInTelephone.SetActive(true);
        //speaker_phoneInEar.Stop();
        playerCanUseTelephone = true;
        playerIsInCall = false;
        clearSlots();
        fs = false;
    }

    private void clearSlots()
    {
        foreach (TMP_Text phoneSlot in phoneNumberSlots)
        {
            phoneSlot.text = "";
        }
        slotIndex = 0;
    }
    
    private void checkPlayerDistance()
    {
        float distance = Vector3.Distance(this.gameObject.transform.position, GameObject.Find("PLAYER").transform.position);
        if (distance > 5)
        {
            if (phoneInEar.activeSelf && playerIsInCall == false)
            {
                print("outside distance, put down phone");
                clearSlots();
                putPhoneDown();
                failsafe = true;
            }
        }
    }

    //Checks which button was pressed and invokes the associated event triggering sound effect playback
    private void CheckButtonPressed(string button)
    {
        switch (button)
        {
            case "0":
                telephoneSFXEvents.onDial0?.Invoke();
                break;

            case "1":
                telephoneSFXEvents.onDial1?.Invoke();
                break;

            case "2":
                telephoneSFXEvents.onDial2?.Invoke();
                break;

            case "3":
                telephoneSFXEvents.onDial3?.Invoke();
                break;

            case "4":
                telephoneSFXEvents.onDial4?.Invoke();
                break;

            case "5":
                telephoneSFXEvents.onDial5?.Invoke();
                break;

            case "6":
                telephoneSFXEvents.onDial6?.Invoke();
                break;

            case "7":
                telephoneSFXEvents.onDial7?.Invoke();
                break;

            case "8":
                telephoneSFXEvents.onDial8?.Invoke();
                break;

            case "9":
                telephoneSFXEvents.onDial9?.Invoke();
                break;

            case "*":
                telephoneSFXEvents.onDialStar?.Invoke();
                break;

            case "#":
                telephoneSFXEvents.onDialPound?.Invoke();
                break;


            default:
                break;
        }
    }
}
