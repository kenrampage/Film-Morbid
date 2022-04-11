using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameStarter : MonoBehaviour
{
    //this shit is in update cause it literally refused to cooperate in start function
    private void Update()
    {
        SceneManager.LoadScene(2);
    }
}
