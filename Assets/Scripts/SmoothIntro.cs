using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;

public class SmoothIntro : MonoBehaviour
{
    public float timer, timer2;
    [SerializeField] GameObject blinder;
    [SerializeField] GameObject[] tutorial;
    [SerializeField] GameObject footage;

    public bool started;
    bool paused;

    [SerializeField] private UnityEvent onFilmPlay;
    [SerializeField] private UnityEvent onFilmPause;

    [SerializeField] GameObject piano;

    void Start()
    {
        blinder.SetActive(true);
        paused = false;
        footage.SetActive(false);
        started = false;
        timer = 15f;
        for(int i = 0; i < tutorial.Length; i++)
        {
            tutorial[i].SetActive(false);
        }
    }
    void Update()
    {
        //pain-o :')
        if (timer2 < 15)
        {
            if (timer >= 0)
            {
                Camera.main.transform.parent.GetComponent<PlayerMovement>().playerCanMove = false;
                Camera.main.GetComponent<MouseLook>().playerCanLookAround = false;
                timer2 += Time.deltaTime;
                timer -= Time.deltaTime;
                blinder.GetComponent<Image>().color = new Color(0, 0, 0, timer / 10f);
                if (timer2 > 10)
                {
                    for (int i = 0; i < tutorial.Length; i++)
                    {
                        tutorial[i].SetActive(false);
                    }
                }
                if (timer2 < 10)
                {
                    if (timer2 > 1)
                    {
                        tutorial[0].SetActive(true);
                    }
                    if (timer2 > 2.5f)
                    {
                        tutorial[1].SetActive(true);
                    }
                    if (timer2 > 4f)
                    {
                        tutorial[2].SetActive(true);
                    }
                }
            }
            if (timer < 0)
            {
                blinder.SetActive(false);
                Camera.main.transform.parent.GetComponent<PlayerMovement>().playerCanMove = true;
                Camera.main.GetComponent<MouseLook>().playerCanLookAround = true;
            }
        }
        if (!started) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if(hit.collider.gameObject.name == "projector")
                    {
                        hit.collider.gameObject.tag = "Untagged";
                        Started_Projector();
                    }
                }
            }
        } 
        else if (started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                paused = !paused;
                if (paused)
                {
                    footage.GetComponent<VideoPlayer>().Pause();
                    onFilmPause?.Invoke();
                }
                else if (!paused)
                {
                    footage.GetComponent<VideoPlayer>().Play();
                    onFilmPlay?.Invoke();
                }
            }
        }
    }
    public void Started_Projector()
    {
        started = true;
        footage.SetActive(true);
        footage.GetComponent<VideoPlayer>().Play();
        onFilmPlay?.Invoke();
    }
}
