using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class LetterSafe : MonoBehaviour
{
    string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    [SerializeField] GameObject[] letterIndicator;
    int[] keyValues = new int[3];

    [SerializeField] GameObject door;
    bool won;
    bool checkTime;

    float timer;

    [SerializeField] private UnityEvent onValueChanged;
    [SerializeField] private UnityEvent onButtonPressed;
    [SerializeField] private UnityEvent onPuzzleSolved;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        checkTime = false;
        System.Random r = new System.Random();
        for (int i = 0; i < 3; i++)
        {
            keyValues[i] = r.Next(0, 26);
        }
        ChangeLetter();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkTime)
        {
            if (timer < 1.7f)
            {
                timer += Time.deltaTime;
                door.transform.localRotation = Quaternion.RotateTowards(door.transform.localRotation, Quaternion.Euler(new Vector3(door.transform.localRotation.eulerAngles.x, door.transform.localRotation.eulerAngles.y, 200)), -75 * Time.deltaTime);
            }
        }
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4f))
        {
            if (letterIndicator[0] != null)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (hit.collider.gameObject.name == "LeftKey")
                    {
                        onValueChanged?.Invoke();
                        keyValues[0]++;
                        if (keyValues[0] > 25)
                        {
                            keyValues[0] = 0;
                        }
                        CheckForSuccess();
                    }
                    else if (hit.collider.gameObject.name == "MidKey")
                    {
                        onValueChanged?.Invoke();
                        keyValues[1]++;
                        if (keyValues[1] > 25)
                        {
                            keyValues[1] = 0;
                        }
                        CheckForSuccess();
                    }
                    else if (hit.collider.gameObject.name == "RightKey")
                    {
                        onValueChanged?.Invoke();
                        keyValues[2]++;
                        if (keyValues[2] > 25)
                        {
                            keyValues[2] = 0;
                        }
                        CheckForSuccess();
                    }


                    ChangeLetter();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.gameObject.name == "LeftKey")
                    {
                        onValueChanged?.Invoke();
                        keyValues[0]--;
                        if (keyValues[0] < 0)
                        {
                            keyValues[0] = 25;
                        }
                        CheckForSuccess();
                    }
                    else if (hit.collider.gameObject.name == "MidKey")
                    {
                        onValueChanged?.Invoke();
                        keyValues[1]--;
                        if (keyValues[1] < 0)
                        {
                            keyValues[1] = 25;
                        }
                        CheckForSuccess();
                    }
                    else if (hit.collider.gameObject.name == "RightKey")
                    {
                        onValueChanged?.Invoke();
                        keyValues[2]--;
                        if (keyValues[2] < 0)
                        {
                            keyValues[2] = 25;
                        }
                        CheckForSuccess();
                    }

                    ChangeLetter();
                }
            }
        }
        if (won)
        {
            checkTime = true;
            Destroy(letterIndicator[0].transform.parent.gameObject);
            won = false;
        }

    }
    void ChangeLetter()
    {

        for (int i = 0; i < 3; i++)
        {
            letterIndicator[i].GetComponent<TextMeshProUGUI>().text = alphabet[keyValues[i]].ToUpper();
        }
    }
    void CheckForSuccess()
    {
        

        if (keyValues[0] == 17 && keyValues[1] == 2 && keyValues[2] == 0)
        {
            Debug.Log("Safe has been opened.");
            won = true;
            onPuzzleSolved?.Invoke();
        }
    }
}
