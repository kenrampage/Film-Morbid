using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SmoothIntro : MonoBehaviour
{
    float timer;
    [SerializeField] GameObject blinder;
    [SerializeField] GameObject footage;

    public bool started;
    bool paused;
    void Start()
    {
        paused = false;
        footage.SetActive(false);
        started = false;
        timer = 10f;
    }
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            blinder.SetActive(true);
            blinder.GetComponent<Image>().color = new Color(0, 0, 0, timer / 10f);
        }
        if (timer < 0)
        {
            blinder.SetActive(false);
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
                }
                else if (!paused)
                {
                    footage.GetComponent<VideoPlayer>().Play();
                }
            }
        }
    }
    public void Started_Projector()
    {
        started = true;
        footage.SetActive(true);
        footage.GetComponent<VideoPlayer>().Play();
    }
}
