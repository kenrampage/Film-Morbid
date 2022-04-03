using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPuzzle : MonoBehaviour
{
    public bool checking, beenRight;
    GameObject trebClef;

    float timer;

    [SerializeField] MusicDot[] musicDots;

    bool won;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!won)
        {
            if (checking)
            {
                timer += Time.deltaTime;
                //Basically animations
                if (!beenRight)
                {
                    if (timer < 0.6f)
                    {
                        trebClef.transform.Rotate(transform.forward * Time.deltaTime * 50f);
                    }
                    else
                    {
                        beenRight = true;
                    }
                }
                else if (beenRight)
                {
                    if (timer < 1.2f)
                    {
                        trebClef.transform.Rotate(-transform.forward * Time.deltaTime * 50f);
                    }
                    else
                    {
                        if (musicDots[0].currentPt == 5 && musicDots[1].currentPt == 5 && musicDots[2].currentPt == 6 && musicDots[3].currentPt == 4 && musicDots[4].currentPt == 5 && musicDots[5].currentPt == 6)
                        {
                            trebClef.gameObject.AddComponent<Rigidbody>();
                            trebClef.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 2, ForceMode.Impulse);
                            trebClef.gameObject.tag = "holdable";
                            for (int i = 0; i < 6; i++)
                            {
                                Rigidbody rb = musicDots[i].gameObject.AddComponent<Rigidbody>();
                                rb.AddForce(transform.forward * 2, ForceMode.Impulse);
                                musicDots[i].gameObject.tag = "holdable";
                            }
                            Debug.Log("Won Music");
                        }
                        won = true;
                        beenRight = false;
                        timer = 0f;
                        checking = false;
                    }
                }
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
}
