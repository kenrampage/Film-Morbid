using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothIntro : MonoBehaviour
{
    float timer;
    [SerializeField] GameObject blinder;
    void Start()
    {
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
    }
}
