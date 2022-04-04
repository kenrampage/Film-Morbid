using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    public playerInteractionState playerInteractionStateScript;
    [SerializeField] GameObject minuteStick, hourStick;
    private bool checkedTime;
    public int minute, hour;
    GameObject playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerInteractionStateScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerInteractionState>();
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        checkedTime = false;
        System.Random r = new System.Random();
        minute = r.Next(0, 12) * 5;
        hour = r.Next(0, 11);
        AddTime(5);
        minuteStick.transform.rotation = Quaternion.Euler(-minute * 6 - 90, minuteStick.transform.rotation.eulerAngles.y, minuteStick.transform.rotation.eulerAngles.z);
        hourStick.transform.rotation = Quaternion.Euler(-hour * 30 - 90, hourStick.transform.rotation.eulerAngles.y, hourStick.transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (checkedTime)
        {
            transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, Quaternion.Euler(new Vector3(transform.parent.rotation.eulerAngles.x, 150, transform.parent.rotation.eulerAngles.z)), 75 * Time.deltaTime);
        }
        //Changing the clock's arms.
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 4))
        {
            if (hit.collider.gameObject == gameObject)
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
            //Button for opening safe
            if (hit.collider.gameObject.name == "ClockButton")
            {
                if (Input.GetKeyDown("e") && playerInteractionStateScript.playerIsAllowedToInteract)
                {
                    CheckTime();
                }
            }
        }
    }
    /// <summary>
    /// Add time to clock on safe; add time in multiples of 5 to make this function good.
    /// </summary>
    /// <param name="tm"></param>
    public void AddTime(int tm)
    {
        minute += tm;
        if (tm > 0)
        {
            //Maths to change the rotation of the hour stick and to prevent any overflowing.
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
            //Maths to change the rotation of the minute stick and to prevent any overflowing.
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
    /// <summary>
    /// Check if the sticks equal 5:00 
    /// </summary>
    public void CheckTime()
    {
        if(minute == 0 && hour == 5)
        {
            GetComponent<AudioSource>().Play();
            gameObject.tag = "Untagged";
            checkedTime = true;
        }
    }
}
