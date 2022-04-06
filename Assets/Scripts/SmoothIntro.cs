using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothIntro : MonoBehaviour
{
    float timer;
    [SerializeField] GameObject blinder;

    public bool started;
    void Start()
    {
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
    }
    public void Started_Projector()
    {
        started = true;
    }
}
