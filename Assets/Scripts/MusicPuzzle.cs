using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPuzzle : MonoBehaviour
{
    public bool checking, beenRight;
    GameObject trebClef;

    float timer;

    [SerializeField] GameObject placedSheet;
    [SerializeField] MusicDot[] musicDots;

    public bool won;

    // Start is called before the first frame update
    void Start()
    {
        placedSheet.SetActive(false);
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.name == "piano" && Camera.main.transform.parent.GetComponent<inventoryManager>().playerHolding_sheetmusic)
                {
                    Camera.main.transform.parent.GetComponent<inventoryManager>().sheetmusic.SetActive(false);
                    placedSheet.SetActive(true);
                }
            }
        }
        if (!won)
        {
            if (checking)
            {
                timer += Time.deltaTime;
                if (musicDots[0].currentPt == 5 && musicDots[1].currentPt == 5 && musicDots[2].currentPt == 6 && musicDots[3].currentPt == 4 && musicDots[4].currentPt == 5 && musicDots[5].currentPt == 6)
                {
                    won = true;
                    Debug.Log("Won Music");
                }
                beenRight = false;
                timer = 0f;
                checking = false;
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
