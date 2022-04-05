using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    public playerInteractionState playerInteractionStateScript;
    [SerializeField] GameObject minuteStick, hourStick;
    public int minute, hour;
    private bool checkedTime;
    GameObject playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerInteractionStateScript = GameObject.Find("PLAYER").GetComponent<playerInteractionState>();
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        checkedTime = false;
        System.Random r = new System.Random();
        minute = r.Next(0, 12) * 5;
        hour = r.Next(0, 11);
        AddTime(5);
        minuteStick.transform.localRotation = Quaternion.Euler(-minute * 6, minuteStick.transform.localRotation.eulerAngles.y, minuteStick.transform.localRotation.eulerAngles.z);
        hourStick.transform.localRotation = Quaternion.Euler(-hour * 30, hourStick.transform.localRotation.eulerAngles.y, hourStick.transform.localRotation.eulerAngles.z);

        minuteStick.SetActive(false);
        hourStick.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (checkedTime)
        {
            transform.Find("clock hinge parent").localRotation = Quaternion.RotateTowards(transform.Find("clock hinge parent").localRotation, Quaternion.Euler(new Vector3(transform.Find("clock hinge parent").localRotation.eulerAngles.x, transform.Find("clock hinge parent").localRotation.eulerAngles.y, -150)), 75 * Time.deltaTime);
        }
        //Changing the clock's arms.
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 4))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (minuteStick.activeSelf)
                {
                    if (Input.GetMouseButtonDown(0) && playerInteractionStateScript.playerIsAllowedToInteract)
                    {
                        AddTime(-5);
                    }
                    else if (Input.GetMouseButtonDown(1) && playerInteractionStateScript.playerIsAllowedToInteract)
                    {
                        AddTime(5);
                    }
                }
                else if (!minuteStick.activeSelf)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (playerCamera.transform.parent.GetComponent<inventoryManager>().playerHolding_clockhands)
                        {
                            playerCamera.transform.parent.GetComponent<inventoryManager>().dropObjects();
                            minuteStick.SetActive(true);
                            hourStick.SetActive(true);
                        }
                    }
                }
            }
            //Button for opening safe
            if (hit.collider.gameObject.name == "ClockKey")
            {
                if (minuteStick.activeSelf)
                {
                    if (Input.GetMouseButtonDown(0) && playerInteractionStateScript.playerIsAllowedToInteract)
                    {
                        CheckTime();
                    }
                }
            }
            //Sheet music
            if(hit.collider.gameObject.name == "Music")
            {
                if(Input.GetMouseButtonDown(0) && playerInteractionStateScript.playerIsAllowedToInteract)
                {
                    Destroy(hit.collider.gameObject);
                    playerCamera.transform.parent.GetComponent<inventoryManager>().playerHolding_sheetmusic = true;
                }
            }
        }
    }
    public void AddTime(int tm)
    {
        minute += tm;
        if (tm > 0)
        {
            minuteStick.transform.Rotate(transform.parent.parent.parent.right * -30);
            if (minute == 60)
            {
                hourStick.transform.Rotate(transform.parent.parent.parent.right * -30);
                minute = 0;
                hour++;
            }
        }
        else if(tm < 0)
        {
            minuteStick.transform.Rotate(transform.parent.parent.parent.right * 30);
            if (minute == -5)
            {
                hourStick.transform.Rotate(transform.parent.parent.parent.right * 30);
                minute = 55;
                hour--;
            }
        }
        if(hour == 12)
        {
            hour = 0;
        }
        if (hour == -1)
        {
            hour = 11;
        }

    }

    public void CheckTime()
    {
        if(minute == 0 && hour == 5)
        {
            gameObject.tag = "Untagged";
            checkedTime = true;
            OnWin();
        }
    }
    void OnWin()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
