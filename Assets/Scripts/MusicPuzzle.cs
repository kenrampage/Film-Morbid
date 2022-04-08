using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicPuzzle : MonoBehaviour
{
    public bool checking, beenRight;
    GameObject trebClef;

    float timer, alfa;

    [SerializeField] GameObject placedSheet;
    [SerializeField] MusicDot[] musicDots;

    public bool won;
    [SerializeField] GameObject winHelp;

    [SerializeField] VideoPlayer projector;
    [SerializeField] VideoClip credits;

    [SerializeField] private UnityEvent onMusicPlaced;
    [SerializeField] private UnityEvent onPuzzleSolved;

    [SerializeField] GameObject EXIT_DOOR;
    [SerializeField] GameObject END_TRIGGER;
    Vector3 END_POS_DOOR;
    [SerializeField] Image blinder;

    // Start is called before the first frame update
    void Start()
    {
        END_POS_DOOR = new Vector3(-90, 0, 70);
        placedSheet.SetActive(false);
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        checking = true;
        if (won)
        {
            timer += Time.deltaTime;
            //Camera.main.transform.parent.localRotation = Quaternion.RotateTowards(Camera.main.transform.parent.localRotation, winHelp.transform.localRotation, (timer*1.5f)/1);
            //Camera.main.transform.parent.GetComponent<PlayerMovement>().playerCanMove = false;
            //Camera.main.transform.localRotation = Quaternion.RotateTowards(Camera.main.transform.localRotation, Quaternion.Euler(new Vector3(0, 0, 0)), timer/3 / 1);
            //Camera.main.GetComponent<MouseLook>().playerCanLookAround = false;
            EXIT_DOOR.transform.localRotation = Quaternion.RotateTowards(EXIT_DOOR.transform.localRotation, Quaternion.Euler(END_POS_DOOR), Mathf.Sqrt(timer));
            RaycastHit hit_;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit_, 1.3f))
                if(hit_.collider.gameObject.name == "END")
                {
                    Camera.main.transform.parent.GetComponent<PlayerMovement>().playerCanMove = false;
                    Camera.main.GetComponent<MouseLook>().playerCanLookAround = false;
                    blinder.gameObject.SetActive(true);
                    alfa += Time.deltaTime/5;
                    blinder.color = new Color(0, 0, 0, alfa);

                    if(alfa > 1)
                    {
                        SceneManager.LoadScene(0);
                    }
                }

        }
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.name == "piano" && Camera.main.transform.parent.GetComponent<inventoryManager>().playerHolding_sheetmusic)
                {
                    print(this.gameObject.name);
                    onMusicPlaced?.Invoke();
                    placedSheet.gameObject.SetActive(true);
                    Camera.main.transform.parent.GetComponent<inventoryManager>().dropObjects();
                }
            }
        }
        if (!won)
        {
            if (checking)
            {
                if (musicDots[0].currentPt == 5 && musicDots[1].currentPt == 5 && musicDots[2].currentPt == 6 && musicDots[3].currentPt == 4 && musicDots[4].currentPt == 5 && musicDots[5].currentPt == 6)
                {
                    OnWinGame();
                    won = true;
                    Debug.Log("Won Music");
                }
                beenRight = false;
                timer = 0f;
                //checking = false;
            }
        }
    }
    public void CheckIfCorrect(GameObject objAsKey)
    {
        if (!checking)
        {
            trebClef = objAsKey;
            checking = true;
        }
    }

    public void OnWinGame()
    {
        onPuzzleSolved?.Invoke();
        projector.Stop();
        projector.clip = credits;
        projector.Play();
    }
}
