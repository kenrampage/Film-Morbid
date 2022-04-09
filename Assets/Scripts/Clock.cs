using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Clock : MonoBehaviour
{
    public playerInteractionState playerInteractionStateScript;
    [SerializeField] GameObject minuteStick, hourStick;
    private float holdTimer;
    public int minute, hour;
    private bool checkedTime;
    GameObject playerCamera;
    public GameObject ClockKeyTurned;
    public GameObject ClockKeyNormal;

    private bool puzzleSolved;


    [SerializeField] private UnityEvent onHandsInstalled;
    [SerializeField] private UnityEvent onTimeChanged;
    [SerializeField] private UnityEvent onButtonPressed;
    [SerializeField] private UnityEvent onPuzzleSolved;
    [SerializeField] private UnityEvent onMusicPickup;

    // Start is called before the first frame update
    void Start()
    {
        holdTimer = 0;

        ClockKeyTurned.SetActive(false);
        puzzleSolved = false;
        playerInteractionStateScript = GameObject.Find("PLAYER").GetComponent<playerInteractionState>();
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        checkedTime = false;
        System.Random r = new System.Random();
        minute = r.Next(0, 12) * 5;
        hour = r.Next(0, 11);
        AddTime(5);
        minuteStick.transform.localRotation = Quaternion.Euler(minuteStick.transform.localRotation.eulerAngles.x, minute * -6 - 180, minuteStick.transform.localRotation.eulerAngles.z);
        hourStick.transform.localRotation = Quaternion.Euler(hourStick.transform.localRotation.eulerAngles.x, hour * -30 - 180, hourStick.transform.localRotation.eulerAngles.z);
        GetComponent<interactionType>().cycle = false;

        minuteStick.SetActive(false);
        hourStick.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // print(puzzleSolved);
        if (checkedTime)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, -230)), 75 * Time.deltaTime);
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
                        holdTimer = 0;
                        onTimeChanged?.Invoke();
                        AddTime(-5);
                    }
                    else if (Input.GetMouseButtonDown(1) && playerInteractionStateScript.playerIsAllowedToInteract)
                    {
                        holdTimer = 0;
                        onTimeChanged?.Invoke();
                        AddTime(5);
                    }

                    //This is for holding the button and the clock going at a higher speed;
                    if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
                    {
                        holdTimer += Time.deltaTime;
                        if (Input.GetMouseButton(0))
                        {
                            if (holdTimer > 0.6f)
                            {
                                if (holdTimer > 0.65f)
                                {
                                    onTimeChanged?.Invoke();
                                    AddTime(-5);
                                    holdTimer = .55f;
                                }
                            }
                        }
                        if (Input.GetMouseButton(1))
                        {
                            if (holdTimer > 0.6f)
                            {
                                if (holdTimer > 0.65f)
                                {
                                    onTimeChanged?.Invoke();
                                    AddTime(5);
                                    holdTimer = .55f;
                                }
                            }
                        }
                    }


                }
                else if (!minuteStick.activeSelf)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (playerCamera.transform.parent.GetComponent<inventoryManager>().playerHolding_clockhands)
                        {
                            GetComponent<interactionType>().cycle = true;
                            playerCamera.transform.parent.GetComponent<inventoryManager>().dropObjects();
                            minuteStick.SetActive(true);
                            hourStick.SetActive(true);

                            onHandsInstalled?.Invoke();
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
                        onButtonPressed?.Invoke();
                        //pretend this garbage code doesn't exist:
                    }
                }
            }
            //Sheet music
            if (hit.collider.gameObject.name == "Music")
            {
                if (Input.GetMouseButtonDown(0) && playerInteractionStateScript.playerIsAllowedToInteract)
                {
                    Destroy(hit.collider.gameObject);
                    onMusicPickup?.Invoke();
                    playerCamera.transform.parent.GetComponent<inventoryManager>().playerHolding_sheetmusic = true;
                }
            }
        }
    }
    public void AddTime(int tm)
    {
        //pls work
        minute += tm;
        if (tm > 0)
        {
            if (minute == 60)
            {
                minute = 0;
                hour++;
                hourStick.transform.localRotation = Quaternion.Euler(hourStick.transform.localRotation.eulerAngles.x, hour * -30 - 180, hourStick.transform.localRotation.eulerAngles.z);
            }
            minuteStick.transform.localRotation = Quaternion.Euler(minuteStick.transform.localRotation.eulerAngles.x, minute * -6 - 180, minuteStick.transform.localRotation.eulerAngles.z);
        }
        else if (tm < 0)
        {
            if (minute == -5)
            {
                minute = 55;
                hour--;
                hourStick.transform.localRotation = Quaternion.Euler(hourStick.transform.localRotation.eulerAngles.x, hour * -30 - 180, hourStick.transform.localRotation.eulerAngles.z);
            }
            minuteStick.transform.localRotation = Quaternion.Euler(minuteStick.transform.localRotation.eulerAngles.x, minute * -6 - 180, minuteStick.transform.localRotation.eulerAngles.z);
        }
        if (hour == 12)
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
        if (minute == 0 && hour == 5 && !puzzleSolved)
        {
            gameObject.tag = "Untagged";
            checkedTime = true;
            puzzleSolved = true;
            onPuzzleSolved?.Invoke();
            OnWin();
        }
    }
    void OnWin()
    {
        GetComponent<BoxCollider>().enabled = false;
        ClockKeyNormal.SetActive(false);
        ClockKeyTurned.SetActive(true);
    }
}