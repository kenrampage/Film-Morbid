using UnityEngine;

public class MusicPuzzle : MonoBehaviour
{
    [SerializeField] MusicDot[] musicDots;
    public bool checking, beenRight;
    GameObject trebClef;
    float timer = 0;
    bool won;
    void Update()
    {
        if (!won)
        {
            if (checking)
            {
                timer += Time.deltaTime;
                //Basically animations - 1
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
                //Basically animations - 2
                else if (beenRight)
                {
                    if (timer < 1.2f)
                    {
                        trebClef.transform.Rotate(-transform.forward * Time.deltaTime * 50f);
                    }
                    else
                    {
                        //Checks if all dots are in the correct position.
                        if (musicDots[0].currentPt == 5 && musicDots[1].currentPt == 5 && musicDots[2].currentPt == 6 && musicDots[3].currentPt == 4 && musicDots[4].currentPt == 5 && musicDots[5].currentPt == 6)
                        {
                            Win();
                        }
                        beenRight = false;
                        timer = 0f;
                        checking = false;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Checks if the notes are ordered correctly.
    /// </summary>
    /// <param name="Treble Clef GameObject"></param>
    public void CheckIfCorrect(GameObject objAsKey)
    {
        if (!checking)
        {
            trebClef = objAsKey;
            checking = true;
        }
    }
    private void Win()
    {
        //Adding a Rigidbody to the treble clef
        trebClef.gameObject.AddComponent<Rigidbody>();
        trebClef.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 2, ForceMode.Impulse);
        //Making it an item you can inspect:
        trebClef.gameObject.tag = "holdable";
        
        for (int i = 0; i < 6; i++)
        {
            //Parent gets nullified to avoid any weird rotations
            musicDots[i].transform.parent = null;
            //Adding a Rigidbody to each music dot
            Rigidbody rb = musicDots[i].gameObject.AddComponent<Rigidbody>();
            rb.AddForce(transform.forward * 2, ForceMode.Impulse);
            musicDots[i].gameObject.tag = "holdable";
        }
        won = true;
    }
}
