using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class Tutorial : MonoBehaviour
{
    //Tutorial 1
    int currentScreen;
    [SerializeField] Material[] colors;

    //Tutorial 2
    [SerializeField] TextMeshProUGUI number;
    int currentNumber;

    //Tutorial 4
    bool canControlScreens;
    [SerializeField] VideoPlayer[] screens;
    // Start is called before the first frame update
    void Start()
    {
        canControlScreens = false;
        currentScreen = 1;
        currentNumber = 0;
        number.text = "0";
        foreach(VideoPlayer s in screens)
        {
            s.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4f))
        {
            if (Input.GetKeyDown("e"))
            {
                if (hit.collider.gameObject != null)
                {
                    if (hit.collider.gameObject.name == "Pc1Button")
                    {
                        if (currentScreen == 0) {
                            hit.collider.gameObject.transform.parent.GetComponent<MeshRenderer>().materials[1].color = colors[currentScreen].color;
                            currentScreen = 1;
                        }
                        else if (currentScreen == 1)
                        {
                            hit.collider.gameObject.transform.parent.GetComponent<MeshRenderer>().materials[1].color = colors[currentScreen].color;
                            currentScreen = 0;
                        }
                    }
                    else if(hit.collider.gameObject.name == "Remote")
                    {
                        canControlScreens = true;
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
            if (hit.collider.gameObject.name == "Pc2Button")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    currentNumber--;
                    if (currentNumber < 0)
                    {
                        currentNumber = 9;
                    }
                    number.text = currentNumber.ToString();
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    currentNumber++;
                    if (currentNumber > 9)
                    {
                        currentNumber = 0;
                    }
                    number.text = currentNumber.ToString();
                }
            }
        }
        if (canControlScreens)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ToggleVideo();
            }
        }
    }
    void ToggleVideo()
    {
        foreach (VideoPlayer s in screens)
        {
            if (!s.isPaused)
            {
                s.Pause();
            }
            else if (s.isPaused)
            {
                s.Play();
            }
        }
    }
}
