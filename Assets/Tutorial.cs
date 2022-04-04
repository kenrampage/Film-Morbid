using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Tutorial : MonoBehaviour
{
    string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    [SerializeField] TextMeshProUGUI number, code;
    [SerializeField] VideoPlayer[] screens;
    [SerializeField] Material[] colors;
    [SerializeField] Image blinder;
    bool canControlScreens;
    int currentScreen;
    int currentNumber;
    int currentLetter;
    bool won;
    float a;
    void Start()
    {
        blinder.color = new Color(0, 0, 0, a);
        canControlScreens = false;
        currentLetter = 0;
        currentScreen = 1;
        currentNumber = 0;
        number.text = "0";
        won = false;
        a = 0;
        foreach (VideoPlayer s in screens)
        {
            s.Play();
        }
    }
    void Update()
    {
        RaycastHit hit;
        if (!won)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4f))
            {
                if (hit.collider.gameObject != null)
                {
                    if (Input.GetKeyDown("e"))
                    {
                        //Toggles screen of pc 1
                        if (hit.collider.gameObject.name == "Pc1Button")
                        {
                            if (currentScreen == 0)
                            {
                                hit.collider.gameObject.transform.parent.GetComponent<MeshRenderer>().materials[1].color = colors[currentScreen].color;
                                currentScreen = 1;
                            }
                            else if (currentScreen == 1)
                            {
                                hit.collider.gameObject.transform.parent.GetComponent<MeshRenderer>().materials[1].color = colors[currentScreen].color;
                                currentScreen = 0;
                            }
                        }
                        else if (hit.collider.gameObject.name == "Remote")
                        {
                            canControlScreens = true;
                            Destroy(hit.collider.gameObject);
                        }
                    }
                    //Cycles through values of pc 2
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
                    //Increments or decrements the value of the code by 1
                    else if (hit.collider.gameObject.name == "DoorButton")
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            currentLetter--;
                            if (currentLetter < 0)
                            {
                                currentLetter = 25;
                            }
                        }
                        if (Input.GetMouseButtonDown(1))
                        {
                            currentLetter++;
                            if (currentLetter > 25)
                            {
                                currentLetter = 0;
                            }
                        }
                        ChangeLetter();
                    }
                    //Checks if code on door is correct
                    else if (hit.collider.gameObject.name == "DoorOpener")
                    {
                        if (Input.GetKeyDown("e"))
                        {
                            if (currentLetter == 23)
                            {
                                won = true;
                                Debug.Log("Door Opened");
                            }
                        }
                    }
                }
            }
            //Makes pausing possible
            if (canControlScreens)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ToggleVideo();
                }
            }
        }
        if (won)
        {
            a += Time.deltaTime;
            blinder.color = new Color(0, 0, 0, a);
            if(a > 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    private void ToggleVideo()
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
    private void ChangeLetter()
    {
        code.text = alphabet[currentLetter].ToUpper();
    }
}
