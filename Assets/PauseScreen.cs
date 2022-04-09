using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject player;
    [SerializeField] GameObject postProcessing;
    [SerializeField] Image skulla;

    public float sensitivity;

    [SerializeField] TextMeshProUGUI sensDisp;
    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
        sensitivity = 180;
        skulla.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseScreen.SetActive(!pauseScreen.activeSelf);
            player.GetComponent<PlayerMovement>().playerCanMove = !pauseScreen.activeSelf;
            Camera.main.GetComponent<MouseLook>().playerCanLookAround = !pauseScreen.activeSelf;
            sensDisp.text = sensitivity.ToString();
        }
        if (pauseScreen.activeSelf)
        {
            if (Input.GetKeyDown("x"))
            {
                AblePostProcessing();
            }
            if(Input.mouseScrollDelta.y != 0)
            {
                sensitivity += Input.mouseScrollDelta.y;
                sensitivity = Mathf.Min(sensitivity, 300);
                sensitivity = Mathf.Max(sensitivity, 10);
                Camera.main.GetComponent<MouseLook>().mouseSensitivity = sensitivity;
                sensDisp.text = sensitivity.ToString();
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                BackToMenu();
            }
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void AblePostProcessing()
    {
        postProcessing.SetActive(!postProcessing.activeSelf);
        skulla.gameObject.SetActive(postProcessing.activeSelf);
    }
}
