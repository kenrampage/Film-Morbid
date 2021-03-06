using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapBoard : MonoBehaviour
{
    [SerializeField] MapPiece[] blackOnes, whiteOnes;
    public float timer;

    [SerializeField] private UnityEvent onPuzzleSolved;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Check())
        {
            timer += Time.deltaTime;
            if (timer < 2f)
            {
                transform.parent.position += new Vector3(0, Time.deltaTime * -2.47f, 0);
            }

        }
    }
    public void CheckTiles()
    {
        if (Check())
        {
            //blinder.SetActive(false);
            for (int i = 0; i < 12; i++)
            {
                blackOnes[i].gameObject.transform.parent = null;
                blackOnes[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
                blackOnes[i].gameObject.tag = "holdable";
                blackOnes[i].GetComponent<MapPiece>().puzzleSolved = true;
                blackOnes[i].gameObject.GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Impulse);
            }
            for(int i = 0; i < 14; i++)
            {
                whiteOnes[i].gameObject.transform.parent = null;
                whiteOnes[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
                whiteOnes[i].gameObject.tag = "holdable";
                whiteOnes[i].GetComponent<MapPiece>().puzzleSolved = true;
                whiteOnes[i].gameObject.GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Impulse);
            }

            print("Puzzle is solved");
            onPuzzleSolved?.Invoke();
            GameObject.Find("telephone light").GetComponent<Light>().enabled = true;
            
        }
    }
    private bool Check()
    {
        for (int i = 0; i < 12; i++)
        {
            if (!blackOnes[i].isBlack)
            {
                return false;
            }
        }
        for (int j = 0; j < 14; j++)
        {
            if (whiteOnes[j].isBlack)
            {
                return false;
            }
        }
        return true;
    }
}
