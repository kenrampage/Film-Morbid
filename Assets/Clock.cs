using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    [SerializeField] GameObject minuteStick, hourStick;
    public int minute, hour;
    private bool checkedTime;
    // Start is called before the first frame update
    void Start()
    {
        checkedTime = false;
        System.Random r = new System.Random();
        minute = r.Next(0, 12) * 5;
        hour = r.Next(0, 11);
        AddTime(5);
    }

    // Update is called once per frame
    void Update()
    {
        if (checkedTime)
        {
            transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, Quaternion.Euler(new Vector3(transform.parent.rotation.eulerAngles.x, 150, transform.parent.rotation.eulerAngles.z)), 75 * Time.deltaTime);
        }
    }
    public void AddTime(int tm)
    {
        minute += tm;
        if (tm > 0)
        {
            if (minute == 60)
            {
                minute = 0;
                hour++;
            }
        }
        else if(tm < 0)
        {
            if (minute == -5)
            {
                minute = 55;
                hour--;
            }
        }
        if(hour == 12)
        {
            hour = 0;
        }
        minuteStick.transform.rotation = Quaternion.Euler(-minute * 6 -90, 0, 0);
        hourStick.transform.rotation = Quaternion.Euler(-hour * 30 -90, 0, 0);

    }

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
