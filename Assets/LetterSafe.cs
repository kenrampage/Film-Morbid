using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterSafe : MonoBehaviour
{
    string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    [SerializeField] GameObject[] letterIndicator;
    int[] keyValues = new int[3];

    [SerializeField] GameObject door;
    bool won;
    // Start is called before the first frame update
    void Start()
    {
        System.Random r = new System.Random();
        for(int i = 0; i < 3; i++)
        {
            keyValues[i] = r.Next(0, 26);
        }
        ChangeLetter();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4f))
        {
            if (letterIndicator[0] != null)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (hit.collider.gameObject.name == "LeftKey")
                    {
                        keyValues[0]++;
                        if (keyValues[0] > 25)
                        {
                            keyValues[0] = 0;
                        }
                    }
                    else if (hit.collider.gameObject.name == "MidKey")
                    {
                        keyValues[1]++;
                        if (keyValues[1] > 25)
                        {
                            keyValues[1] = 0;
                        }
                    }
                    else if (hit.collider.gameObject.name == "RightKey")
                    {
                        keyValues[2]++;
                        if (keyValues[2] > 25)
                        {
                            keyValues[2] = 0;
                        }
                    }
                    

                    ChangeLetter();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.gameObject.name == "LeftKey")
                    {
                        keyValues[0]--;
                        if (keyValues[0] < 0)
                        {
                            keyValues[0] = 25;
                        }
                    }
                    else if (hit.collider.gameObject.name == "MidKey")
                    {
                        keyValues[1]--;
                        if (keyValues[1] < 0)
                        {
                            keyValues[1] = 25;
                        }
                    }
                    else if (hit.collider.gameObject.name == "RightKey")
                    {
                        keyValues[2]--;
                        if (keyValues[2] < 0)
                        {
                            keyValues[2] = 25;
                        }
                    }
                    
                    ChangeLetter();
                }
                if (Input.GetKeyDown("e"))
                {
                    if (hit.collider.gameObject.name == "SubmitKey")
                    {
                        CheckForSuccess();
                    }
                }
            }
        }
        if (won)
        {
            Rigidbody rb = door.AddComponent<Rigidbody>();
            rb.AddForce(-Camera.main.transform.forward * 8, ForceMode.Impulse);
            door.gameObject.tag = "holdable";
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
        if(keyValues[0] == 5 && keyValues[1] == 14 && keyValues[2] == 1)
        {
            Debug.Log("Safe has been opened.");
            won = true;
        }
    }
}
