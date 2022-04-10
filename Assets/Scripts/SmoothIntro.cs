using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;

public class SmoothIntro : MonoBehaviour
{
    public Animator projectorAnim;
    public Animator projectorAnim2;
    public Light light_footage;
    public Light light_projector;
    public float timer, timer2;
    [SerializeField] GameObject blinder;
    [SerializeField] GameObject[] tutorial;
    [SerializeField] GameObject footage;

    public bool started;
    bool paused;

    [SerializeField] private UnityEvent onFilmPlay;
    [SerializeField] private UnityEvent onFilmPause;

    [SerializeField] GameObject piano;
    [SerializeField] AudioSource videoAudio;

    void Start()
    {
        light_footage.enabled = false;
        light_projector.enabled = false;
        Camera.main.transform.parent.GetComponent<PlayerMovement>().playerCanMove = true;
        Camera.main.GetComponent<MouseLook>().playerCanLookAround = true;
        GameObject.Find("PLAYER").GetComponent<footstep>().enableed = false;
        blinder.SetActive(true);
        paused = false;
        footage.SetActive(false);
        started = false;
        timer = 10f;
        for(int i = 0; i < tutorial.Length; i++)
        {
            tutorial[i].SetActive(false);
        }
    }
    void Update()
    {
        //pain-o :')
        if (!piano.GetComponent<MusicPuzzle>().won)
        {
            if (timer2 < 10)
            {
                if (timer >= 0)
                {
                    timer2 += Time.deltaTime;
                    timer -= Time.deltaTime;
                    blinder.GetComponent<Image>().color = new Color(0, 0, 0, timer + 5 / 15f);
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
                        if(timer2 > 5.5f)
                        {
                            tutorial[3].SetActive(true);
                        }
                    }
                }
                if (timer < 0)
                {
                    //blinder.SetActive(false);
                    GameObject.Find("PLAYER").GetComponent<footstep>().enableed = true;
                }
            }
            if (!started)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.collider.gameObject.name == "projector")
                        {
                            GameObject.Find("projector").tag = "Untagged";
                            Started_Projector();
                        }
                    }
                }
            }
            else if (started)
            {
                if (!piano.GetComponent<MusicPuzzle>().won)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Animator reel1_anim = GameObject.Find("reel1").GetComponent<Animator>();
                        Animator reel2_anim = GameObject.Find("reel2").GetComponent<Animator>();
                        paused = !paused;
                        if (paused)
                        {
                            onFilmPause?.Invoke();
                            reel1_anim.speed = 0;
                            reel2_anim.speed = 0;
                            videoAudio.Pause();
                            footage.GetComponent<Animator>().speed = 0;

                        }
                        else if (!paused)
                        {
                            onFilmPlay?.Invoke();
                            reel1_anim.speed = 1;
                            reel2_anim.speed = 1;
                            videoAudio.Play();
                            footage.GetComponent<Animator>().speed = 1;
                            
                        }
                    }
                }
                else if (piano.GetComponent<MusicPuzzle>().won)
                {
                    videoAudio.Pause();
                    footage.GetComponent<Animator>().speed = 1;
                }
            }
        }
        else
        {
            print("f      u          c          k ");
        }
    }
    public void Started_Projector()
    {
        GameObject.Find("ambient light").GetComponent<Light>().enabled = false;
        projectorAnim.SetBool("turnedOn", true);
        projectorAnim2.SetBool("turnedOn", true);
        light_footage.enabled = true;
        light_projector.enabled = true;
        started = true;
        footage.SetActive(true);
        footage.GetComponent<Animator>().Play(0);
        onFilmPlay?.Invoke();
        videoAudio.Play();
        Debug.Log(videoAudio.isPlaying.ToString());
    }
}
